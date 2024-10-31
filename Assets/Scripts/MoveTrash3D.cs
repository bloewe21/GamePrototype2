using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrash3D : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private int trashNum = 1;
    [SerializeField] private float speedZ = 1f;
    [SerializeField] private int trashWeight = 50;
    [SerializeField] private int timeValue = 3;
    [SerializeField] private bool isCaught = false;

    private GameObject bob;
    private GameObject score;
    private GameObject time;
    private GameObject bucket;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(0f, 0f, speedZ);

        bob = GameObject.Find("Bob");
        score = GameObject.Find("Score");
        time = GameObject.Find("Time");
        bucket = GameObject.Find("Buckets");
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaught)
        {
            //attach trash to bob if caught
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

        //update bob's weight, stop trash speed
        bob.GetComponent<MoveBob>().UpdateWeight(trashWeight);
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    private void SurfaceFunction()
    {
        //update bob collider/weight
        bob.GetComponent<MoveBob>().ResetBob();

        //add time to timer
        time.GetComponent<TimerScript>().AddTime(timeValue);

        //spawn bucket prefab
        bucket.GetComponent<BucketScript>().SpawnBucketTrash(trashNum);

        //destroy trash
        Destroy(gameObject);
    }
}

