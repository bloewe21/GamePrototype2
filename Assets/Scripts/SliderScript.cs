using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public GameObject currentFish;
    private GameObject bob;
    // Start is called before the first frame update
    void Start()
    {
        bob = GameObject.Find("Bob");
    }

    void OnEnable()
    {
        //when slider is activated, set max value to current fish's designated sliderStrength
        GetComponent<Slider>().value = GetComponent<Slider>().maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //if a fish is connected to bob
        if (currentFish)
        {
            //gameObject.transform.position = new Vector3(currentFish.transform.position.x, currentFish.transform.position.y - .5f, currentFish.transform.position.z);
            GetComponent<Slider>().value -= Time.deltaTime;
        }

        //if slider reaches 0
        if (GetComponent<Slider>().value <= 0f)
        {
            RemoveFish();
        }
    }

    public void RemoveFish()
    {
        //if fish on the line, remove it
        if (currentFish)
        {
           Destroy(currentFish); 
        }

        //reset value back to maxValue, disable slider
        GetComponent<Slider>().value = GetComponent<Slider>().maxValue;
        this.gameObject.SetActive(false);
    }
}
