using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOnAndSlam : MonoBehaviour
{
    public float pushForce;
    public float distanceStaling;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(((other.gameObject.transform.position - transform.position).normalized * pushForce) / ((other.gameObject.transform.position - transform.position).magnitude * distanceStaling), ForceMode.Impulse);
        }
    }
}
