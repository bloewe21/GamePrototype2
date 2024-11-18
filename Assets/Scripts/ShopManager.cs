using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject[] buyButtons;
    [SerializeField] private float effectTime = 60f;

    private GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //reset all buttons when game starts
        for (int i=0; i < buyButtons.Length; i++)
        {
            GameObject currentButton = buyButtons[i];
            GameObject textChild = currentButton.transform.GetChild(0).gameObject;

            textChild.GetComponent<TextMeshProUGUI>().text = "Buy";
            currentButton.GetComponent<Button>().interactable = true;
        }
    }

    public void BuyFunction(int buttonID, int myPrice)
    {
        //get current score
        float currentScore = score.GetComponent<ScoreScript>().score;

        //if not enough points in score, do nothing...
        if ((currentScore - myPrice) < 0)
        {
            return;
        }

        //decrease score number by price
        score.GetComponent<ScoreScript>().score -= myPrice;

        //effects depending on buttonID
        if (buttonID == 1)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[0] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[0] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[0] *= 2;
        }
        else if (buttonID == 2)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[1] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[1] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[1] *= 2;
        }
        else if (buttonID == 3)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[2] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[2] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[2] *= 2;
        }
        else if (buttonID == 4)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[3] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[3] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[3] *= 2;
        }
        else if (buttonID == 5)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[4] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[4] *= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[4] *= 2;
        }

        StartCoroutine(PurchaseButtonRoutine(buttonID));
        StartCoroutine(PurchaseRoutine(buttonID));
    }

    //wait, deactivate purchase effect
    private IEnumerator PurchaseRoutine(int buttonID)
    {
        yield return new WaitForSeconds(effectTime);
        if (buttonID == 1)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[0] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[0] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[0] /= 2;
        }
        else if (buttonID == 2)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[1] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[1] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[1] /= 2;
        }
        else if (buttonID == 3)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[2] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[2] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[2] /= 2;
        }
        else if (buttonID == 4)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[3] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[3] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[3] /= 2;
        }
        else if (buttonID == 5)
        {
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupOne[4] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupTwo[4] /= 2;
            spawner.GetComponent<SpawnScriptNew>().probabilitiesGroupThree[4] /= 2;
        }
    }

    //deactivate button, wait, reactivate button
    private IEnumerator PurchaseButtonRoutine(int buttonID)
    {
        GameObject currentButton = buyButtons[buttonID-1];
        GameObject textChild = currentButton.transform.GetChild(0).gameObject;
        textChild.GetComponent<TextMeshProUGUI>().text = "Active";
        currentButton.GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(effectTime);

        textChild.GetComponent<TextMeshProUGUI>().text = "Buy";
        currentButton.GetComponent<Button>().interactable = true;
    }
}
