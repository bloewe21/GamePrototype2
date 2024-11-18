using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBob : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private GameObject bobSlider;
    [SerializeField] private GameObject fishingRod;
    [SerializeField] private GameObject reel;

    //MovementFunction1 variables
    [SerializeField] public float maxBobWidth;
    [SerializeField] public float minBobWidth;
    [SerializeField] private float maxBobHeight;
    [SerializeField] private float minBobHeight;
    [SerializeField] private float defaultBobSpeed = 200f;
    private float prevMousePosY;
    private float fishWeight = 0f;

    //MovementFunction2 variables
    public bool isReeled = true;
    private bool sliderUp = true;
    private bool inWater = false;
    public bool hitWater = false;
    public bool hasFish = false;
    public bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevMousePosY = Input.mousePosition.y;
    }

    private void OnEnable()
    {
        //reset bobSlider to minValue
        bobSlider.GetComponent<Slider>().value = bobSlider.GetComponent<Slider>().minValue;
    }

    // Update is called once per frame
    void Update()
    {
        //reel rotation
        reel.transform.eulerAngles = new Vector3(0f, 0f, transform.position.x * 40.0f);

        MovementFunction1();
        MovementFunction2();
    }

    private void OnTriggerEnter(Collider other)
    {
        //bob down when collide with fish/trash
        if (other.gameObject.tag == "Fish")
        {
            rb.AddForce(new Vector2(0f, -200.0f));
        }
        if (other.gameObject.tag == "Trash")
        {
            rb.AddForce(new Vector2(0f, -200.0f));
        }

        //set bools for enter water/surface
        if (other.gameObject.tag == "Water")
        {
            inWater = true;
            hitWater = true;
        }
        if (other.gameObject.tag == "Surface")
        {
            isReeled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //set bool for exit water
        if (other.gameObject.tag == "Water")
        {
            inWater = false;
        }
    }

    //called in MoveFish3D SurfaceFunction(); MoveTrash3D SurfaceFunction()
    public void UpdateWeight(int newWeight)
    {
        fishWeight = newWeight;
    }

    //called in custom UnityEvent shopButton
    public void UpdateMenu()
    {
        inMenu = !inMenu;
    }

    //called in multiple scripts... resets bob's weight, position, bools...
    public void ResetBob()
    {
        UpdateWeight(0);
        transform.position = fishingRod.transform.position;
        isReeled = true;
        inWater = false;  
    }

    //function for controlling bob
    private void MovementFunction1()
    {
        //can't control if:
        if (isReeled || !hitWater)
        {
            return;
        }

        //min/max distance bob can be pulled horizontally (obsolete due to bobSlider max strength?)
        //max bob width
        if (transform.position.x < maxBobWidth)
        {
            transform.position = new Vector3(maxBobWidth, transform.position.y, 0f);
        }
        //min bob width
        if (transform.position.x > minBobWidth)
        {
            transform.position = new Vector3(minBobWidth, transform.position.y, 0f);
        }

        //get current mouse position y
        float mousePosY = Input.mousePosition.y;
        //subtract previous mouse position y from current
        float mouseDiff = mousePosY - prevMousePosY;
        //normalize distance to move bob
        float bobDiff = mouseDiff / (defaultBobSpeed + fishWeight);
        //manual amount to move bob up when reeling
        float bobDiffY = bobDiff / 2.0f;
        
        //set bobs new position if holding left click
        if (Input.GetMouseButton(0) && (transform.position.x < (transform.position.x - bobDiff)))
        {
            transform.position = new Vector3(transform.position.x - bobDiff, transform.position.y, 0f);
        }

        //store current mouse position y as previous
        prevMousePosY = mousePosY;
    }

    //function for how bob moves
    private void MovementFunction2()
    {
        //float up in water
        if (inWater)
        {
            rb.AddForce(new Vector2(0f, 1.0f));
        }

        //bob is reeled fully in
        if (isReeled)
        {
            //set position + bools
            transform.position = fishingRod.transform.position;
            rb.useGravity = false;
            hitWater = false;

            //slider hits max value, time to move down
            if (bobSlider.GetComponent<Slider>().value == bobSlider.GetComponent<Slider>().maxValue)
            {
                sliderUp = false;
            }
            //slider hits min value, time to move up
            if (bobSlider.GetComponent<Slider>().value == bobSlider.GetComponent<Slider>().minValue)
            {
                sliderUp = true;
            }

            //move slider
            if (sliderUp)
            {
                //slider moves up
                bobSlider.GetComponent<Slider>().value += Time.deltaTime;
            }
            else
            {
                //slider moves down
                bobSlider.GetComponent<Slider>().value -= Time.deltaTime;
            }

            //launch bob at bobSlider strength
            if (Input.GetMouseButtonDown(0) && !inMenu)
            {
                isReeled = false;
                rb.AddForce(new Vector2(-1800f * bobSlider.GetComponent<Slider>().value - 150f, 0f));
                rb.useGravity = true;
            }
        }
    }
}
