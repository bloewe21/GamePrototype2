using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAnimation : MonoBehaviour
{
    [SerializeField] private GameObject catchText;
    [SerializeField] private GameObject fish1Display;
    [SerializeField] private GameObject fish2Display;
    [SerializeField] private GameObject fish3Display;
    [SerializeField] private GameObject fish4Display;
    [SerializeField] private GameObject fish5Display;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called in MoveFish3D SurfaceFunction()
    public void PlayAnimation(int fishNum)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationRoutine(fishNum));
    }

    private IEnumerator AnimationRoutine(int fishNum)
    {
        //activate correct fishDisplay
        GameObject currentDisplay = null;
        if (fishNum == 1)
        {
            currentDisplay = fish1Display;
        }
        else if (fishNum == 2)
        {
            currentDisplay = fish2Display;
        }
        else if (fishNum == 3)
        {
            currentDisplay = fish3Display;
        }
        else if (fishNum == 4)
        {
            currentDisplay = fish4Display;
        }
        else if (fishNum == 5)
        {
            currentDisplay = fish5Display;
        }

        //animation active for 3 seconds
        currentDisplay.SetActive(true);
        catchText.SetActive(true);

        yield return new WaitForSeconds(3f);

        currentDisplay.SetActive(false);
        catchText.SetActive(false);
    }
}
