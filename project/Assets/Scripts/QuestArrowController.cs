using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArrowController : MonoBehaviour
{
    public WindowQuestPointer windowQuestPointer;

    // Start is called before the first frame update
    void Start()
    {
        //windowQuestPointer.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            //windowQuestPointer.Hide();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Collectable"))
        {
           // windowQuestPointer.Show();
        }
    }
}
