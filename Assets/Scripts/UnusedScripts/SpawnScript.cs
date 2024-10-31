using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] upperObjects;
    [SerializeField] private float[] upperLocations;
    [SerializeField] private GameObject[] lowerObjects;
    [SerializeField] private float[] lowerLocations;

    [SerializeField] private int maxSpawns;
    [SerializeField] private int maxTrash;
    [SerializeField] private float spawnTime = 5f;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnRoutine()
    {
        bool resetRoutine = false;

        //choose to spawn in upper or lower section of pond
        int sectionChoice = Random.Range(0, 2);

        GameObject newSpawn = null;
        float spawnXLocation = Random.Range(-1, 1);
        float spawnYLocation = 0f;
        
        //grab object from upper or lower list
        if (sectionChoice == 0)
        {
            newSpawn = upperObjects[Random.Range(0, upperObjects.Length)];
            spawnYLocation = upperLocations[Random.Range(0, upperLocations.Length)];
        }
        else
        {
            newSpawn = lowerObjects[Random.Range(0, lowerObjects.Length)];
            spawnYLocation = lowerLocations[Random.Range(0, lowerLocations.Length)];
        }

        //gather all fish/trash into lists
        GameObject[] allFish = GameObject.FindGameObjectsWithTag("Fish");
        GameObject[] allTrash = GameObject.FindGameObjectsWithTag("Trash");

        //if trying to spawn more than maxTrash, redo coroutine
        if (newSpawn.tag == "Trash" && allTrash.Length >= maxTrash)
        {
            resetRoutine = true;
        }

        if (!resetRoutine)
        {    
            //if amount of objects is less than maxSpawns
            if ((allFish.Length + allTrash.Length) < maxSpawns)
            {
                Instantiate(newSpawn, new Vector3(spawnXLocation, spawnYLocation, 0f), newSpawn.transform.rotation);
            }

            //pause, spawn after spawnTime seconds
            yield return new WaitForSeconds(spawnTime);
        }

        StartCoroutine(SpawnRoutine());
    }
}
