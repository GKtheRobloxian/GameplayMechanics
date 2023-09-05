using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float speedInitial;
    public float hori;
    public float forward;
    public GameObject visual;
    public GameObject proj;
    bool grounded;
    public bool hasDash = false;
    public bool hasKaboom = false;
    public float dashMomentum;
    public GameObject particles;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedInitial = speed;
    }

    // Update is called once per frame
    void Update()
    {
        visual.transform.position = transform.position;
        forward = Input.GetAxisRaw("Vertical");
        hori = Input.GetAxisRaw("Horizontal");
        if (hasDash)
        {
            particles.SetActive(true);
            speed = speedInitial * 1.3f;
        }
        else
        {
            particles.SetActive(false);
            speed = speedInitial;
        }
        if (grounded)
        {
            rb.AddRelativeForce(Vector3.forward * forward * speed);
            rb.AddRelativeForce(Vector3.right * hori * speed);

            visual.GetComponent<Rigidbody>().AddRelativeTorque(gameObject.transform.right * forward * speed / 2);
            visual.GetComponent<Rigidbody>().AddRelativeTorque(gameObject.transform.forward * -hori * speed / 2);
            if (hasDash && Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 velocitySave = rb.velocity;
                rb.velocity = Vector3.zero; 
                rb.AddRelativeForce(velocitySave.normalized*dashMomentum, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Dash"))
        {
            hasDash = true;
            Destroy(collision.gameObject);
            StartCoroutine(DashCoroutine());
        }

        if (collision.gameObject.CompareTag("Kaboom"))
        {
            hasKaboom = true;
            Destroy(collision.gameObject);
            StartCoroutine(KaboomCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(5);
        hasDash = false;
    }

    IEnumerator KaboomCoroutine()
    {
        StartCoroutine(Blastin());
        yield return new WaitForSeconds(5);
        hasKaboom = false;
    }

    IEnumerator Blastin()
    {
        if (hasKaboom)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject projectile = Instantiate(proj, transform.position + rb.velocity.normalized, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddRelativeForce(rb.velocity.normalized * 20, ForceMode.Impulse);
            StartCoroutine(Blastin());
        }
    }
}
