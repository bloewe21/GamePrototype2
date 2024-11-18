using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrash3D : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private int trashNum = 1;
    [SerializeField] private float speedX = 2f;
    [SerializeField] private int trashWeight = 50;
    [SerializeField] private int timeValue = 3;
    [SerializeField] private bool isCaught = false;

    private GameObject bob;
    private GameObject score;
    private GameObject time;
    private GameObject bucket;
    private GameObject list;

    // Start is called before the first frame update
    void Start()
    {
        //set initial speed of trash
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speedX, 0f, 0f);

        //set material transparency to 0
        Color color = GetComponent<MeshRenderer>().materials[0].color;
        color.a = 0f;
        GetComponent<MeshRenderer>().materials[0].color = color;

        bob = GameObject.Find("Bob");
        score = GameObject.Find("Score");
        time = GameObject.Find("Time");
        bucket = GameObject.Find("Buckets");
        list = GameObject.Find("List");

        StartCoroutine(AppearRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (isCaught)
        {
            //attach fish to bob (positioned manually) if caught
            transform.position = new Vector3(bob.transform.position.x - .35f, bob.transform.position.y, bob.transform.position.z);
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
            BobFunction();
        }

        if (other.gameObject.tag == "Surface")
        {
            SurfaceFunction();
        }
    }

    private IEnumerator AppearRoutine()
    {
        //get color of MeshRenderer
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color color = meshRenderer.materials[0].color;

        //while color's alpha is below 1
        while (color.a < 1f)
        {
            //increase color's alpha value, apply to MeshRenderer
            color.a += 0.001f;
            meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }

        //start DisappearRoutine() when alpha = 1
        StartCoroutine(DisappearRoutine());
    }

    private IEnumerator DisappearRoutine()
    {
        //get color of MeshRenderer
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color color = meshRenderer.materials[0].color;

        //while color's alpha is above 0
        while (color.a > 0f)
        {
            //reduce color's alpha value. apply to MeshRenderer
            color.a -= 0.001f;
            meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }

        //destroy trash when alpha = 0
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
        color.a = 1f;
        GetComponent<MeshRenderer>().materials[0].color = color;

        //update bob weight, stop fish speed
        bob.GetComponent<MoveBob>().UpdateWeight(trashWeight);
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    private void SurfaceFunction()
    {
        //update bob collider/weight
        bob.GetComponent<MoveBob>().ResetBob();
        //add time to timer (currently obsolete)
        time.GetComponent<TimerScript>().AddTime(timeValue);
        //spawn bucket prefab
        bucket.GetComponent<BucketScript>().SpawnBucketTrash(trashNum);
        //update check list
        list.GetComponent<CheckList>().UpdateListTrash();
        //destroy trash
        Destroy(gameObject);
    }
}

