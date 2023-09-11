using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwinsScript : MonoBehaviour
{
    public float healthPoints = 20f;
    public GameObject sliderr;
    public Slider realSlide;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        realSlide.maxValue = healthPoints;
        realSlide.value = healthPoints;
        realSlide.fillRect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        sliderr.transform.position = transform.position + Vector3.up;
        sliderr.transform.LookAt(GameObject.Find("Main Camera").transform.position);
        float random = Random.Range(-3.0f, 3.0f);
        float random2 = Random.Range(-3.0f, 3.0f);
        if (transform.position.y < -2)
        {
            transform.position = new Vector3(random, 0.0f, random2);
        }
        if (healthPoints < 1)
        {
            Destroy(gameObject);
        }
        UpdateValue(healthPoints);

    }

    void UpdateValue(float value)
    {
        realSlide.fillRect.gameObject.SetActive(true);
        realSlide.value = value; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            healthPoints--;
        }
    }
}
