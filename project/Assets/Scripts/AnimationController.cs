using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            Debug.Log("Playing running animation");
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    private void LateUpdate()
    {
        transform.localPosition += startPosition;
    }
}
