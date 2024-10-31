using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAnimation : MonoBehaviour
{
    [SerializeField] private GameObject catchText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        //activate X whenever fish is reeled in
        catchText.SetActive(true);
        yield return new WaitForSeconds(3f);
        catchText.SetActive(false);
    }
}
