using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float hori;
    public float forward;
    public GameObject visual;
    bool grounded;
    public bool hasDash = false;
    public float dashMomentum;
    public GameObject particles;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }
        else
        {
            particles.SetActive(false);
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
        if(collision.gameObject.CompareTag("Powerup"))
        {
            hasDash = true;
            Destroy(collision.gameObject);
            StartCoroutine(PowerupCoroutine());
        }
    }

    IEnumerator PowerupCoroutine()
    {
        yield return new WaitForSeconds(5);
        hasDash = false;
    }
}
