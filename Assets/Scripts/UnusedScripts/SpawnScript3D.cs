using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnScript3D : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnables;
    [SerializeField] private int[] probabilities;
    [SerializeField] private float[] locations;

    //[SerializeField] private GameObject[] upperObjects;
    //[SerializeField] private float[] upperLocations;
    //[SerializeField] private GameObject[] lowerObjects;
    //[SerializeField] private float[] lowerLocations;

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

    // private IEnumerator SpawnRoutine()
    // {
    //     bool resetRoutine = false;

    //     GameObject newSpawn = null;
    //     float spawnZLocation = Random.Range(-2, 3);
    //     float spawnYLocation = Random.Range(-2, 1);
    //     float spawnXLocation = 0f;

    //     //choose to spawn in upper or lower section of pond
    //     int sectionChoice = Random.Range(0, 2);
        
    //     //grab object from upper or lower list
    //     if (sectionChoice == 0)
    //     {
    //         newSpawn = upperObjects[Random.Range(0, upperObjects.Length)];
    //         spawnXLocation = upperLocations[Random.Range(0, upperLocations.Length)];
    //     }
    //     else
    //     {
    //         newSpawn = lowerObjects[Random.Range(0, lowerObjects.Length)];
    //         spawnXLocation = lowerLocations[Random.Range(0, lowerLocations.Length)];
    //     }

    //     //gather all fish/trash into lists
    //     GameObject[] allFish = GameObject.FindGameObjectsWithTag("Fish");
    //     GameObject[] allTrash = GameObject.FindGameObjectsWithTag("Trash");

    //     //if trying to spawn more than maxTrash, redo coroutine
    //     if (newSpawn.tag == "Trash" && allTrash.Length >= maxTrash)
    //     {
    //         resetRoutine = true;
    //     }

    //     if (!resetRoutine)
    //     {    
    //         //if amount of objects is less than maxSpawns
    //         if ((allFish.Length + allTrash.Length) < maxSpawns)
    //         {
    //             Instantiate(newSpawn, new Vector3(spawnXLocation, spawnYLocation, spawnZLocation), newSpawn.transform.rotation);
    //         }

    //         //pause, spawn after spawnTime seconds
    //         yield return new WaitForSeconds(spawnTime);
    //     }

    //     StartCoroutine(SpawnRoutine());
    // }

    private IEnumerator SpawnRoutine()
    {
        //deterimine random fish position
        float spawnXLocation = locations[Random.Range(0, locations.Length)];
        float spawnYLocation = Random.Range(0, 1);
        float spawnZLocation = Random.Range(-2, 3);
        
        //determine random spawnable based off probabilities
        GameObject newSpawn = null;
        int total = 0;
        int roll = Random.Range(0, probabilities.Sum());
        for (int i = 0; i < probabilities.Length; i++)
        {
            total += probabilities[i];
            if (roll < total)
            {
                newSpawn = spawnables[i];
                break;
            }
        }

        //gather all fish/trash into lists
        GameObject[] allFish = GameObject.FindGameObjectsWithTag("Fish");
        GameObject[] allTrash = GameObject.FindGameObjectsWithTag("Trash");

        //if trying to spawn more than maxTrash, redo coroutine
        if (newSpawn.tag == "Trash" && allTrash.Length >= maxTrash)
        {
            //do nothing
        }
        else
        { 
            //if amount of objects is less than maxSpawns
            if ((allFish.Length + allTrash.Length) < maxSpawns)
            {
                Instantiate(newSpawn, new Vector3(spawnXLocation, spawnYLocation, spawnZLocation), newSpawn.transform.rotation);
            }

            //pause, spawn after spawnTime seconds
            yield return new WaitForSeconds(spawnTime);
        }

        //restart routine
        StartCoroutine(SpawnRoutine());
    }
}

