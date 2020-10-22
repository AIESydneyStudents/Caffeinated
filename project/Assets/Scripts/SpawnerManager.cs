using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject itemToBeSpawned;
    public Vector3 offset = new Vector3(0.4f, 0.1f, 0.3f);

    private int index = 0;
    private int emptySpawners = 4;
    
    
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < spawners.Length; i++)
        //{
        //    if (!spawners[i].transform.Find(itemToBeSpawned.name + "(Clone)"))
        //    {
        //        emptySpawners++;
        //    }
        //}

        if (emptySpawners == spawners.Length)
        {
            // Generate a random number between 0 and the length of the spawners array
            index = Random.Range(0, spawners.Length);
            Instantiate(itemToBeSpawned, spawners[index].transform.position + offset, Quaternion.identity);
        }
    }
}
