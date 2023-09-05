using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectsWithTag("Enemy")[Random.Range(0, GameObject.FindGameObjectsWithTag("Enemy").Length)].transform.position);
    }
}
