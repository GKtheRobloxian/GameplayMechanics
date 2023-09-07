using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().grounded)
        {
            rb.AddForce((player.transform.position - transform.position).normalized * speed);
        }
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
}
