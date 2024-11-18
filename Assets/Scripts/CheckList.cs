using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class CheckList : MonoBehaviour
{
    [SerializeField] private GameObject[] requiredFish;
    [SerializeField] private int[] requiredFishNums;
    [SerializeField] private int[] textRequirements;
    private List<int> textCurrents;
    private int trashCurrent;
    [SerializeField] private GameObject[] myTextBoxes;
    [SerializeField] private GameObject myTrashTextBox;
    [SerializeField] private int trashRequirement;
    // Start is called before the first frame update
    void OnEnable()
    {
        textCurrents = new List<int>();
        for (int i=0; i < myTextBoxes.Length; i++)
        {
            textCurrents.Add(0);
        }

        trashCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //always updating all fish requirements
        for (int i = 0; i < myTextBoxes.Length; i++)
        {
            var currNum = i + 1;
            myTextBoxes[i].gameObject.GetComponent<TextMeshProUGUI>().text = currNum.ToString() + ". " + requiredFish[i].GetComponent<MoveFish3D>().fishName + " (" + textCurrents[i].ToString() + "/" + textRequirements[i].ToString() + ")";
        }

        //always updating trash requirement
        myTrashTextBox.gameObject.GetComponent<TextMeshProUGUI>().text = "Trash (" + trashCurrent.ToString() + "/" + trashRequirement.ToString() + ")";
    }

    //called in MoveFish3D SurfaceFunction()
    public void UpdateListFish(int num)
    {
        //if fishID in requiredFishNums
        if (requiredFishNums.Contains(num))
        {
            int index = System.Array.IndexOf(requiredFishNums, num);
            textCurrents[index] += 1;
        }
    }

    //called in MoveTrash3D SurfaceFunction()
    public void UpdateListTrash()
    {
        trashCurrent += 1;
    }
}
