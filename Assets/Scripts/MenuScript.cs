using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private int buttonID;
    [SerializeField] private int myPrice;
    [SerializeField] private GameObject shopManager;
    [SerializeField] UnityEvent buttonPressEvent;

    private GameObject score;
    private GameObject bob;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //start game button, back button
    public void PressButton1()
    {
        //custom unity event triggered on button press
        buttonPressEvent.Invoke();

        //find bob, call bob's ResetBob()
        bob = GameObject.Find("Bob");
        if (bob)
        {
            bob.GetComponent<MoveBob>().ResetBob();
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

    //shop button
    public void PressButton2()
    {
        //custom unity event triggered on button press
        buttonPressEvent.Invoke();
    }

    //buy button
    public void BuyButton()
    {
        //call shopManager's BuyFunction()
        shopManager.GetComponent<ShopManager>().BuyFunction(buttonID, myPrice);
    }
}
