using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject itemToBeSpawned;
    public Vector3 offset = new Vector3(0.4f, 0.1f, 0.3f);
    public WindowQuestPointer windowQuestPointer;

    private int index = 0;
    private int emptySpawners = 0;
    private DestroyCollectable destroyCollectable;

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
        else if (itemToBeSpawned.name == "PowerUp")
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

        if (GameObject.Find("TeaBag(Clone)"))
        {
            windowQuestPointer.Show(GameObject.FindGameObjectWithTag("Collectable"));
        }
        else if (GameObject.Find("Customer(Clone)"))
        {
            windowQuestPointer.Show(GameObject.FindGameObjectWithTag("Customer"));
        }

        if (emptySpawners == spawners.Length)
        {
            // Generate a random number between 0 and the length of the spawners array
            index = Random.Range(0, spawners.Length);
            Instantiate(itemToBeSpawned, spawners[index].transform.position + offset, Quaternion.identity);
            emptySpawners = 0;
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
                Debug.Log("Tea not here");
                emptySpawners++;
            }
        }
    }

    private void SpawnPowerUp()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (GameObject.Find(itemToBeSpawned.name + "(Clone)"))
            {
                break;
            }
            else
            {
                emptySpawners++;
            }
        }
    }

    private void SpawnCustomer()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (GameObject.Find(itemToBeSpawned.name + "(Clone)"))
            {
                break;
            }
            else
            {
                emptySpawners++;
            }
        }
    }
}
