using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject itemToBeSpawned;
    public Vector3 offset = new Vector3(0.4f, 0.1f, 0.3f);
    public WindowQuestPointer windowQuestPointer;
    public int numberOfItemsToBeSpawned;

    private int index = 0;
    private int emptySpawners = 0;
    private DestroyCollectable destroyCollectable;
    private int itemsInScene;
    private List<int> usedSpawners = new List<int>();

    private bool teaSpawner;
    private bool powerUpSpawner;
    private bool customerSpawner;

    private void Start()
    {
        destroyCollectable = GameObject.Find("Player").GetComponent<DestroyCollectable>();

        if (itemToBeSpawned.CompareTag("Collectable"))
        {
            teaSpawner = true;
        }
        else if (itemToBeSpawned.CompareTag("PowerUp"))
        {
            powerUpSpawner = true;
        }
        else if (itemToBeSpawned.CompareTag("Customer"))
        {
            customerSpawner = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (teaSpawner)
        {
            SpawnTea();
        }
        else if (powerUpSpawner)
        {
            SpawnPowerUp();
        }
        else if (customerSpawner)
        {
            SpawnCustomer();
        }

        if (windowQuestPointer != null)
        {
            if (GameObject.Find("TeaBag(Clone)"))
            {
                windowQuestPointer.Show(GameObject.FindGameObjectWithTag("Collectable"));
            }
            else if (GameObject.Find("Customer(Clone)"))
            {
                windowQuestPointer.Show(GameObject.FindGameObjectWithTag("Customer"));
            }
        }
    }

    private void SpawnTea()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (GameObject.Find(itemToBeSpawned.name + "(Clone)") || destroyCollectable.teaBags > 0)
            {
                break;
            }
            else
            {
                emptySpawners++;
            }
        }

        if (emptySpawners == spawners.Length)
        {
            index = Random.Range(0, spawners.Length);
            Instantiate(itemToBeSpawned, spawners[index].transform.position + offset, Quaternion.identity);
            emptySpawners = 0;
        }
    }

    private void SpawnPowerUp()
    {
        // Spawn the items based on how many items that need to be spawned
        List<int> availableSpawners = new List<int>();
        itemsInScene = 0;

        // Check if the number of items has been spawned
        for (int i = 0; i < spawners.Length; i++)
        {
            if (GameObject.Find(itemToBeSpawned.name + i))
            {
                itemsInScene++;
            }
        }

        if (itemsInScene != numberOfItemsToBeSpawned)
        {
            // Add the spawners that are available
            for (int i = 0; i < spawners.Length; i++)
            {
                availableSpawners.Add(i);
            }

            for (int l = 0; l < usedSpawners.Count; l++)
            {
                GameObject powerUp = GameObject.Find(itemToBeSpawned.name + l);

                if (powerUp == null)
                {
                    // Check if the spawner already exists in the available spawner list
                    availableSpawners.Add(l);
                    usedSpawners.Remove(l);
                }
            }

            // Remove the spawners that are being used
            for (int j = 0; j < availableSpawners.Count; j++)
            {
                for (int k = 0; k < usedSpawners.Count; k++)
                {
                    if (availableSpawners[j] == usedSpawners[k])
                    {
                        availableSpawners.RemoveAt(j);
                    }
                }
            }

            // If not, then choose a random available spawner
            while (itemsInScene < numberOfItemsToBeSpawned)
            {
                int availableSpawnersIndex = Random.Range(0, availableSpawners.Count);
                index = availableSpawners[availableSpawnersIndex];
                GameObject powerUp = Instantiate(itemToBeSpawned, spawners[index].transform.position + offset, Quaternion.identity);
                // Naming convention: PU_Speed0
                powerUp.name = itemToBeSpawned.name + index;
                availableSpawners.RemoveAt(availableSpawnersIndex);
                itemsInScene++;
                usedSpawners.Add(index);
            }
        }
    }

    private void SpawnCustomer()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (GameObject.Find(itemToBeSpawned.name + "(Clone)") || GameObject.FindGameObjectWithTag("Collectable"))
            {
                break;
            }
            else
            {
                emptySpawners++;
            }
        }

        if (emptySpawners == spawners.Length)
        {
            index = Random.Range(0, spawners.Length);
            Instantiate(itemToBeSpawned, spawners[index].transform.position + offset, Quaternion.Euler(0, 180, 0));
            emptySpawners = 0;
        }
    }
}
