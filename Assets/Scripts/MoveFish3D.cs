using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveFish3D : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public string fishName;
    [SerializeField] public int fishNum = 1;
    [SerializeField] private float speedX = 2f;
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
    private GameObject list;
    private GameObject shadow;
    public CollectionCount collectionCount;
    
    // Start is called before the first frame update
    void Start()
    {
        collectionCount = FindObjectOfType<CollectionCount>();
        
        shadow = gameObject.transform.GetChild(0).gameObject;
        //set initial speed of fish
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speedX, 0f, 0f);

        //set material transparency to 0
        //Color color = GetComponent<MeshRenderer>().materials[0].color;
        Color color = shadow.GetComponent<MeshRenderer>().materials[0].color;
        color.a = 0f;
        //GetComponent<MeshRenderer>().materials[0].color = color;
        shadow.GetComponent<MeshRenderer>().materials[0].color = color;

        bob = GameObject.Find("Bob");
        score = GameObject.Find("Score");
        canvas = GameObject.Find("Canvas");
        slider = canvas.transform.Find("Slider").gameObject;
        bucket = GameObject.Find("Buckets");
        catchanim = GameObject.Find("CatchAnim");
        list = GameObject.Find("List");

        StartCoroutine(AppearRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaught)
        {
            //attach fish to bob (positioned manually) if caught
            transform.position = new Vector3(bob.transform.position.x - .50f, bob.transform.position.y, bob.transform.position.z);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            //currently obsolete
            WallFunction();
        }

        if (other.gameObject.tag == "Bob")
        {
            //only call if bob doesn't have a fish already
            if (!bob.GetComponent<MoveBob>().hasFish)
            {
                bob.GetComponent<MoveBob>().hasFish = true;
                BobFunction();
            }
        }

        if (other.gameObject.tag == "Surface")
        {
            SurfaceFunction();
        }
    }

    private IEnumerator AppearRoutine()
    {
        //get color of MeshRenderer
        // MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshRenderer meshRenderer = shadow.GetComponent<MeshRenderer>();
        Color color = meshRenderer.materials[0].color;

        //while color's alpha is below 1
        //while (color.a < 1f)
        while (color.a < .5f)
        {
            //increase color's alpha value, apply to MeshRenderer
            color.a += 0.001f;
            meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }

        //start DisappearRoutine() when alpha = 1
        yield return new WaitForSeconds(1f);
        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        //get color of MeshRenderer
        // MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshRenderer meshRenderer = shadow.GetComponent<MeshRenderer>();
        Color color = meshRenderer.materials[0].color;

        //while color's alpha is above 0
        while (color.a > 0f)
        {
            //reduce color's alpha value. apply to MeshRenderer
            color.a -= 0.001f;
            meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }

        //destroy fish when alpha = 0
        Destroy(gameObject);
    }

    private void WallFunction()
    {
        //currently obsolete
    }

    private void BobFunction()
    {
        isCaught = true;

        //stop transparency routines, set alpha to 1
        StopAllCoroutines();
        Color color = GetComponent<MeshRenderer>().materials[0].color;
        // color.a = 1f;
        color.a = .5f;
        //GetComponent<MeshRenderer>().materials[0].color = color;
        shadow.GetComponent<MeshRenderer>().materials[0].color = color;

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
        //return if not caught by bob
        if (!isCaught)
        {
            return;
        }

        //reset bob position/weight
        bob.GetComponent<MoveBob>().ResetBob();
        //update score
        score.GetComponent<ScoreScript>().score += scoreValue;
        //call slider function
        slider.GetComponent<SliderScript>().RemoveFish();
        //spawn bucket prefab
        bucket.GetComponent<BucketScript>().SpawnBucketFish(fishNum);
        //call catchanim function
        catchanim.GetComponent<CatchAnimation>().PlayAnimation(fishNum);
        //update check list
        list.GetComponent<CheckList>().UpdateListFish(fishNum);

        //save amount of this fish caught total (PlayerPrefs)
        string keyCheck = "Fish" + fishNum.ToString() + "Caught";
        if (!PlayerPrefs.HasKey(keyCheck))
        {
            PlayerPrefs.SetInt(keyCheck, 1);
        }
        else
        {
            PlayerPrefs.SetInt(keyCheck, PlayerPrefs.GetInt(keyCheck) + 1);
        }

        //Add too fish collection
        collectionCount.AddFish(fishNum);
    }
}
