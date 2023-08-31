using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject indicator;
    public float countdown = 10f;
    bool spawningProcess = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !spawningProcess)
        {
            StartCoroutine(Spawning());
        }
    }

    IEnumerator Spawning()
    {
        spawningProcess = true;
        float random = Random.Range(-6.0f, 6.0f);
        float random2 = Random.Range(-6.0f, 6.0f);
        GameObject instantiatedIndicator = Instantiate(indicator, new Vector3(random, -2.15f, random2), Quaternion.identity);
        yield return new WaitForSeconds(0.75f);
        Instantiate(enemyPrefab, new Vector3(random, 0, random2), enemyPrefab.transform.rotation);
        Destroy(instantiatedIndicator);
        countdown = 10f;
        spawningProcess = false;
        StopAllCoroutines();
    }
}
