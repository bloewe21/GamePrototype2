using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrash : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speedX = 1f;
    [SerializeField] private int trashWeight = 50;
    [SerializeField] private int scoreValue = -1;
    [SerializeField] private bool isCaught = false;

    private GameObject bob;
    private GameObject score;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(speedX, 0f, 0f);

        bob = GameObject.Find("Bob");
        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaught)
        {
            transform.position = new Vector3(bob.transform.position.x, bob.transform.position.y - 0.5f, bob.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            //print("wall");
            WallFunction();
        }

        if (other.gameObject.tag == "Bob")
        {
            //print("bob");
            BobFunction();
            bob.GetComponent<MoveBob>().UpdateWeight(trashWeight);
        }

        if (other.gameObject.tag == "Surface")
        {
            //print("surface");
            SurfaceFunction();
        }
    }

    private void WallFunction()
    {
        speedX = speedX * -1.0f;
        rb.velocity = new Vector3(speedX, 0f, 0f);
    }

    private void BobFunction()
    {
        isCaught = true;
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    private void SurfaceFunction()
    {
        score.GetComponent<ScoreScript>().score += scoreValue;
        bob.GetComponent<SphereCollider>().enabled = true;
        bob.GetComponent<MoveBob>().UpdateWeight(0);

        Destroy(gameObject);
    }
}
