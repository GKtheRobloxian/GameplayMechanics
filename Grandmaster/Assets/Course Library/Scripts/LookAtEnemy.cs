using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public int randomEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(randomEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectsWithTag("Enemy")[randomEnemy].transform.position - Vector3.up*GameObject.FindGameObjectsWithTag("Enemy")[randomEnemy].transform.position.y);
    }

    IEnumerator randomEnemies()
    {
        yield return new WaitForSeconds(1.0f);
        randomEnemy = Random.Range(0, GameObject.FindGameObjectsWithTag("Enemy").Length);
        StartCoroutine(randomEnemies());
    }
}
