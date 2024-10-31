using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveFish3D : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private int fishNum = 1;
    [SerializeField] private float speedZ = 1f;
    [SerializeField] private int sliderStrength = 1;
    [SerializeField] private int fishWeight = 50;
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private bool isCaught = false;

    private GameObject bob;
    private GameObject score;
    private GameObject canvas;
    private GameObject slider;
    private GameObject bucket;
    private GameObject catchanim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //spawn with random velocity direction
        int speedMod = Random.Range(0, 2);
        if (speedMod == 0)
        {
            speedZ *= -1;
        }
        rb.velocity = new Vector3(0f, 0f, speedZ);

        bob = GameObject.Find("Bob");
        score = GameObject.Find("Score");
        canvas = GameObject.Find("Canvas");
        slider = canvas.transform.Find("Slider").gameObject;
        bucket = GameObject.Find("Buckets");
        catchanim = GameObject.Find("CatchAnim");
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaught)
        {
            //attach fish to bob if caught
            transform.position = new Vector3(bob.transform.position.x - .35f, bob.transform.position.y, bob.transform.position.z);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            WallFunction();
        }

        if (other.gameObject.tag == "Bob")
        {
            BobFunction();
        }

        if (other.gameObject.tag == "Surface")
        {
            SurfaceFunction();
        }
    }

    private void WallFunction()
    {
        //reverse direction if hit wall
        speedZ = speedZ * -1.0f;
        rb.velocity = new Vector3(0f, 0f, speedZ);
    }

    private void BobFunction()
    {
        isCaught = true;

        //update bob weight, stop fish speed
        bob.GetComponent<MoveBob>().UpdateWeight(fishWeight);
        rb.velocity = new Vector3(0f, 0f, 0f);

        //update and activate slider
        slider.GetComponent<Slider>().maxValue = sliderStrength;
        slider.SetActive(true);
        slider.GetComponent<SliderScript>().currentFish = this.gameObject;
    }

    private void SurfaceFunction()
    {
        //update bob collider/weight
        bob.GetComponent<MoveBob>().ResetBob();
        //update score
        score.GetComponent<ScoreScript>().score += scoreValue;
        //call slider function
        slider.GetComponent<SliderScript>().RemoveFish();

        //save amount of this fish caught total
        string keyCheck = "Fish" + fishNum.ToString() + "Caught";
        if (!PlayerPrefs.HasKey(keyCheck))
        {
            PlayerPrefs.SetInt(keyCheck, 1);
        }
        else
        {
            PlayerPrefs.SetInt(keyCheck, PlayerPrefs.GetInt(keyCheck) + 1);
        }

        //spawn bucket prefab
        bucket.GetComponent<BucketScript>().SpawnBucketFish(fishNum);
        //call catchanim function
        catchanim.GetComponent<CatchAnimation>().PlayAnimation();
    }
}
