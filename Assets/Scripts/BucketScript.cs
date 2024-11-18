using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    [SerializeField] private GameObject fish1Bucket;
    [SerializeField] private GameObject fish2Bucket;
    [SerializeField] private GameObject fish3Bucket;
    [SerializeField] private GameObject fish4Bucket;
    [SerializeField] private GameObject fish5Bucket;

    [SerializeField] private GameObject trash1Bucket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called in MoveFish3D SurfaceFunction()
    public void SpawnBucketFish(int fishNum)
    {
        //spawn bucket fish depending on MoveFish3D fishNum
        if (fishNum == 1)
        {
            Instantiate(fish1Bucket, new Vector3(1.3f, 3f, 2f), fish1Bucket.transform.rotation);
        }
        else if (fishNum == 2)
        {
            Instantiate(fish2Bucket, new Vector3(1.3f, 3f, 2f), fish2Bucket.transform.rotation);
        }
        else if (fishNum == 3)
        {
            Instantiate(fish3Bucket, new Vector3(1.3f, 3f, 2f), fish3Bucket.transform.rotation);
        }
        else if (fishNum == 4)
        {
            Instantiate(fish4Bucket, new Vector3(1.3f, 3f, 2f), fish4Bucket.transform.rotation);
        }
        else if (fishNum == 5)
        {
            Instantiate(fish5Bucket, new Vector3(1.3f, 3f, 2f), fish5Bucket.transform.rotation);
        }
    }

    public void SpawnBucketTrash(int trashNum)
    {
        //spawn bucket trash depending on MoveTrash3D trashNum
        if (trashNum == 1)
        {
            Instantiate(trash1Bucket, new Vector3(3f, 3f, 2f), trash1Bucket.transform.rotation);
        }
    }
}
