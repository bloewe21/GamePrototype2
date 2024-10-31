using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private int gameMode = 1;
    [SerializeField] private float startTime = 30f;
    private float newTime;
    private float currentTime;

    [SerializeField] UnityEvent timeEndEvent;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //set newTime to given startTime
        newTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if gameMode == 0 (endless), disable text
        if (gameMode == 0)
        {
            GetComponent<TextMeshProUGUI>().enabled = false;
            return;
        }

        //ensure TimerEnd() is only called once
        if (currentTime < 0)
        {
            return;
        }

        //subtract seconds
        newTime -= Time.deltaTime;
        //floor function on time
        currentTime = Mathf.Floor(newTime);

        //when timer reaches 0
        if (currentTime < 0)
        {
            //what happens at end of each given game mode
            if (gameMode == 1)
            {
                TimerEnd();
                timeEndEvent.Invoke();
            }
            return;
        }

        //change text to currentTime
        GetComponent<TextMeshProUGUI>().text = currentTime.ToString();
    }

    private void TimerEnd()
    {
        //reset times
        newTime = startTime;
        currentTime = startTime;

        //destroy all existing fish/trash
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

    //mainly used by trash script
    public void AddTime(int timeToAdd)
    {
        newTime += timeToAdd;
    }
}
