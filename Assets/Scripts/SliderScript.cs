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

    //reset slider to maxValue on enable
    void OnEnable()
    {
        GetComponent<Slider>().value = GetComponent<Slider>().maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //if a fish is connected to bob
        if (currentFish)
        {
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
        print("remove");
        //if fish on the line, remove it
        if (currentFish)
        {
           Destroy(currentFish);
           bob.GetComponent<MoveBob>().hasFish = false;
           bob.GetComponent<MoveBob>().UpdateWeight(0);
        }

        //reset value back to maxValue, disable slider
        GetComponent<Slider>().value = GetComponent<Slider>().maxValue;
        this.gameObject.SetActive(false);
    }
}
