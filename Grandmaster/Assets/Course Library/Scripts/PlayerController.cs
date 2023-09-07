using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float blastVelocity;
    float speedInitial;
    public float hori;
    public float forward;
    public GameObject visual;
    public GameObject proj;
    public GameObject projAimer;
    public GameObject slamHitbox;
    public bool grounded = false;
    public bool hasDash = false;
    public bool hasKaboom = false;
    public bool hasSlam = false;
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
        if (hasKaboom)
        {
            projAimer.SetActive(true);
        }
        else
        {
            projAimer.SetActive(false);
        }
        slamHitbox.SetActive(hasSlam);
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
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

        if (collision.gameObject.CompareTag("Slam"))
        {
            hasSlam = true;
            Destroy(collision.gameObject);
            StartCoroutine(SlamCheck());
        }
    }
    IEnumerator SlamCheck()
    {
        yield return new WaitForSeconds(0.5f);
        hasSlam = false;
        slamHitbox.SetActive(false);
        StopCoroutine(SlamCheck());
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
            GameObject projectile = Instantiate(proj, projAimer.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddRelativeForce((projAimer.transform.position-transform.position).normalized * blastVelocity, ForceMode.Impulse);
            StartCoroutine(Blastin());
        }
    }
}
