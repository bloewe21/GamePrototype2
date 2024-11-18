using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpawnScriptNew : MonoBehaviour
{
    //how many attempts spawner has tried to spawn
    private int spawnChecks = 0;
    //max attempts at spawning before forcing a spawn
    [SerializeField] private int maxChecks = 5;
    //time between spawn attempts
    [SerializeField] private float spawnTime = 2f;
    //x ranges for each fish group...
    [SerializeField] private float[] groupRanges;

    //spawnables = possible prefabs to spawn; probabilties = chance of each spawnable spawning
    [SerializeField] private GameObject[] spawnablesGroupOne;
    public int[] probabilitiesGroupOne;
    [SerializeField] private GameObject[] spawnablesGroupTwo;
    public int[] probabilitiesGroupTwo;
    [SerializeField] private GameObject[] spawnablesGroupThree;
    public int[] probabilitiesGroupThree;

    //save default probabilties; for shop purposes
    private int[] savedProbabilitiesOne;
    private int[] savedProbabilitiesTwo;
    private int[] savedProbabilitiesThree;

    //distance between bob and surface
    private float bobDistanceFromSurface;
    //whether spawner should be attempting spawns or not
    private bool canSearch = false;

    private GameObject bob;

    // Start is called before the first frame update
    void Start()
    {
        bob = GameObject.Find("Bob");

        //save default probabilties
        savedProbabilitiesOne = (int[])probabilitiesGroupOne.Clone();
        savedProbabilitiesTwo = (int[])probabilitiesGroupTwo.Clone();
        savedProbabilitiesThree = (int[])probabilitiesGroupThree.Clone();
    }

    private void OnEnable()
    {
        StartCoroutine(ResetProbabilitiesRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        bobDistanceFromSurface = Mathf.Abs(bob.transform.position.x);

        //start routine once bob hits water initially
        if (bob.GetComponent<MoveBob>().hitWater && canSearch)
        {
            StartCoroutine(PossibleSpawn());
            bob.GetComponent<MoveBob>().hitWater = false;
            canSearch = false;
        }

        //reset canSearch if bob is fully reeled in
        if (bob.GetComponent<MoveBob>().isReeled)
        {
            canSearch = true;
        }
    }

    //reset probabilties back to original (savedProbabilities)
    public IEnumerator ResetProbabilitiesRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i=0; i < savedProbabilitiesOne.Length; i++)
        {
            probabilitiesGroupOne[i] = savedProbabilitiesOne[i];
            probabilitiesGroupTwo[i] = savedProbabilitiesTwo[i];
            probabilitiesGroupThree[i] = savedProbabilitiesThree[i];
        }
    }

    private IEnumerator PossibleSpawn()
    {
        yield return new WaitForSeconds(spawnTime);

        if (bob.GetComponent<MoveBob>().isReeled)
        {
            spawnChecks = 0;
            yield break;
        }

        spawnChecks += 1;
        //randomize chance of spawning a prefab
        int spawnChance = Random.Range(0, maxChecks);

        //default to group one spawns
        GameObject[] currentSpawnables = spawnablesGroupOne;
        int[] currentProbabilities = probabilitiesGroupOne;

        //if bob past range 2, group three spawns
        if (bob.transform.position.x < groupRanges[1])
        {
            currentSpawnables = spawnablesGroupThree;
            currentProbabilities = probabilitiesGroupThree;
        }
        //if bob past range 1 (but not past range 2). group two spawns
        else if (bob.transform.position.x < groupRanges[0])
        {
            currentSpawnables = spawnablesGroupTwo;
            currentProbabilities = probabilitiesGroupTwo;
        }

        //spawn if spawnChance lands on 1 randomly, or if spawnChecks reaches maxChecks limit
        if (spawnChance == 1 || spawnChecks == maxChecks)
        {
            spawnChecks = 0;
            GameObject newSpawn = null;
            int total = 0;
            int roll = Random.Range(0, currentProbabilities.Sum());
            for (int i = 0; i < currentProbabilities.Length; i++)
            {
                total += currentProbabilities[i];
                if (roll < total)
                {
                    newSpawn = currentSpawnables[i];
                    break;
                }
            }
            Instantiate(newSpawn, new Vector3(bob.transform.position.x - 3.0f, 0f, bob.transform.position.z), newSpawn.transform.rotation);
        }

        //fish isn't spawned; restart routine
        else
        {
            StartCoroutine(PossibleSpawn());
        }
    }
}
