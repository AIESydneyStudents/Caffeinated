using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragControl : MonoBehaviour
{
    public RB_PlayerController rbC;
    private Rigidbody rb;
    public float groundFriction;
    public float airFriction;

    private float curfriction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rbC.grounded)
        {
            curfriction = groundFriction;
        }
        else
        {
            curfriction = airFriction;
        }
        //Debug.Log("rb velocity: " + rb.velocity.x + "move dir: " + rbC.moveDir.x);
        if (rb.velocity.x * rbC.moveDir.x <= 0)
        {
            float slowdown = rb.velocity.x / ((curfriction * 0.01f)+1);
            Vector3 newVecolicty = new Vector3(slowdown, 0, 0);
            rb.velocity = rbC.VelocityOverride(newVecolicty, rb.velocity);
        }
        if (rb.velocity.z * rbC.moveDir.z <= 0)
        {
            float slowdown = rb.velocity.z / ((curfriction * 0.01f) + 1);
            Vector3 newVecolicty = new Vector3(0, 0, slowdown);
            rb.velocity = rbC.VelocityOverride(newVecolicty, rb.velocity);
        }
    }
}
