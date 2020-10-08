using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_FollowPlayer : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject player;
    private Vector3 cameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = cameraObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position + cameraPosition;
    }
}
