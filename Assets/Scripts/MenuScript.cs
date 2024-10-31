using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuScript : MonoBehaviour
{
    [SerializeField] UnityEvent buttonPressEvent;

    private GameObject bob;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressButton()
    {
        //custom unity event triggered on button press
        buttonPressEvent.Invoke();

        //find bob, reset to 0 0 0
        bob = GameObject.Find("Bob");
        if (bob)
        {
            bob.transform.position = new Vector3(0f, 0f, 0f);
        }

        //destroy all fish, trash, bucketitems
        GameObject[] allFish = GameObject.FindGameObjectsWithTag("Fish");
        GameObject[] allTrash = GameObject.FindGameObjectsWithTag("Trash");
        GameObject[] allBuckets = GameObject.FindGameObjectsWithTag("BucketItem");

        foreach (GameObject fish in allFish)
        {
            Destroy(fish);
        }
        foreach (GameObject trash in allTrash)
        {
            Destroy(trash);
        }
        foreach (GameObject bucketItem in allBuckets)
        {
            Destroy(bucketItem);
        }
    }


}
