using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScript : MonoBehaviour
{
    [SerializeField] private int fishNum = 0;
    [SerializeField] private GameObject myFishDisplay;
    [SerializeField] private float rotateSpeed = -50f;
    [SerializeField] private GameObject myCaughtText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate display fish constantly
        myFishDisplay.transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);

        //grab + print amount of X fish caught
        string caughtKey = "Fish" + fishNum.ToString() + "Caught";
        if (PlayerPrefs.HasKey(caughtKey))
        {
            myCaughtText.GetComponent<TextMeshProUGUI>().text = "Times caught: " + PlayerPrefs.GetInt(caughtKey).ToString();
        }
        else
        {
            myCaughtText.GetComponent<TextMeshProUGUI>().text = "Times caught: 0"; 
        }
    }
}
