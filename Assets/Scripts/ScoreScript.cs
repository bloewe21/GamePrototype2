using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        //set score to 0 when game starts
        score = 0;
        GetComponent<TMP_Text>().text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = score.ToString();
    }
}
