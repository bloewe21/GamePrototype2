using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBob : MonoBehaviour
{
    private Rigidbody rb;
    private float fishWeight = 0f;

    //for 3D scene
    [SerializeField] private float maxBobWidth;
    [SerializeField] private float minBobWidth;
    [SerializeField] private float maxBobHeight;
    [SerializeField] private float minBobHeight;

    //MovementFunction1 variables
    private float prevMousePosY;
    [SerializeField] private float defaultBobSpeed = 200f;
    [SerializeField] private float bobSinkSpeed = 10f;

    //MovementFunction2 variables
    [SerializeField] private float bobInputSpeed = 30.0f;
    [SerializeField] private float bobInputDistance = 3.0f;
    private bool isMoving = false;
    private Vector3 currentVector = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevMousePosY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        //debug, reset bob position
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }

        //MovementFunction1();
        //MovementFunction2();
        MovementFunction3();
    }

    private void OnTriggerEnter(Collider other)
    {
        //disable collider when touch fish, trash
        if (other.gameObject.tag == "Fish")
        {
            GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.tag == "Trash")
        {
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    public void UpdateWeight(int newWeight)
    {
        fishWeight = newWeight;
    }

    public void ResetBob()
    {
        GetComponent<SphereCollider>().enabled = true;
        UpdateWeight(0);
    }

    private void MovementFunction1()
    {
        //get current mouse position y
        float mousePosY = Input.mousePosition.y;
        //subtract previous mouse position y from current
        float mouseDiff = mousePosY - prevMousePosY;
        //normalize distance to move bob
        float bobDiff = mouseDiff / (defaultBobSpeed + fishWeight);
        
        //set bobs new position if holding left click
        if (Input.GetMouseButton(0))
        {
            transform.position = new Vector3(0f, transform.position.y + bobDiff, 0f);
        }

        //store current mouse position y as previous
        prevMousePosY = mousePosY;
    }

    private void MovementFunction2()
    {
        var step = bobInputSpeed * Time.deltaTime;

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentVector, step);
            if (transform.position == currentVector)
            {
                isMoving = false;
            }
            return;
        }

        float abovePositionY = transform.position.y + bobInputDistance;
        float belowPositionY = transform.position.y - bobInputDistance;
        Vector3 aboveVector = new Vector3(0f, abovePositionY, 0f);
        Vector3 belowVector = new Vector3(0f, belowPositionY, 0f);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.y == maxBobHeight)
            {
                return;
            }
            isMoving = true;
            currentVector = aboveVector;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.y == minBobHeight)
            {
                return;
            }
            isMoving = true;
            currentVector = belowVector;
        }

    }

    private void MovementFunction3()
    {
        //min/max distance bob can be pulled horizontally
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

        //min/max height the bob can rise/sink
        //max bob height
        if (transform.position.y > maxBobHeight)
        {
            transform.position = new Vector3(transform.position.x, maxBobHeight, 0f);
        }
        //min bob height
        if (transform.position.y < minBobHeight)
        {
            transform.position = new Vector3(transform.position.x, minBobHeight, 0f);
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
        //if (Input.GetMouseButton(0) && bobDiff != 0f)
        if (Input.GetMouseButton(0))
        {
            transform.position = new Vector3(transform.position.x - bobDiff, transform.position.y + Mathf.Abs(bobDiffY), 0f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - bobSinkSpeed, 0f);
        }

        //store current mouse position y as previous
        prevMousePosY = mousePosY;
    }
}
