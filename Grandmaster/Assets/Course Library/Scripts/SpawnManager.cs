using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemyPrefab;
    public GameObject indicator;
    public GameObject[] powerup;
    public float countdown = 10f;
    bool spawningProcess = false;
    int enemiesToSpawn = 1;
    int powerupCheck;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning(1));
    }

    // Update is called once per frame
    void Update()
    {
        powerupCheck = enemiesToSpawn % 2;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0 && spawningProcess == false)
        {
            enemiesToSpawn++;
            StartCoroutine(Spawning(enemiesToSpawn));
        }
    }

    IEnumerator Spawning(int amount)
    {
        for (int i = 0; i<amount; i++)
        {
            spawningProcess = true;
            float random = Random.Range(-6.0f, 6.0f);
            float random2 = Random.Range(-6.0f, 6.0f);
            GameObject instantiatedIndicator = Instantiate(indicator, new Vector3(random, -2.15f, random2), Quaternion.identity);
            yield return new WaitForSeconds(0.75f);
            int randomNumber = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomNumber], new Vector3(random, 0, random2), Quaternion.identity);
            Destroy(instantiatedIndicator);
            spawningProcess = false;
        }
        if (powerupCheck <= 0)
        {
            float random3 = Random.Range(-9.0f, 9.0f);
            float random4 = Random.Range(-9.0f, 9.0f);
            int randomnumber = Random.Range(0, powerup.Length);
            Instantiate(powerup[randomnumber], new Vector3(random3, 0f, random4), Quaternion.identity);
        }
        StopAllCoroutines();
    }
}
