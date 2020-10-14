using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public float PickupBonusTime = 5f;
    private float ForceBoost = 0.8f;
    private float MassBoost = 0.1f;
    public float DashForce = 10;
    public float JumpForce = 5;
    public int MidAirJumps = 1;
    public int MidAirDashs = 1;


    private PlayerControls Controls;
    private Vector3 moveDir;
    private int jumps;
    private int dashs;
    private float distToGround;
    private float distToWall;
    private Rigidbody rb;
    private RigidbodyConstraints rbConstraints;
    private bool pickup = true;
    private GameController gc;
    private bool constraintToggle = false;
    

    private Color activeColour;
    private Color inactiveColour;

    // Start is called before the first frame update
    private void Awake()
    {
        Controls = new PlayerControls();
        Controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        Controls.Player.Dash.performed += _ => Dash();
        Controls.Player.Drop.performed += _ => Drop();
        Controls.Player.Jump.performed += _ => Jump();
        Controls.Debug.toggle2D.performed += _ => Toggle2D_performed();

        rb = GetComponent<Rigidbody>();
        rbConstraints = rb.constraints;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        distToWall = GetComponent<Collider>().bounds.extents.x;

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        jumps = MidAirJumps;
        dashs = MidAirDashs;

        activeColour = Color.green;
        inactiveColour = Color.red;
    }
    private void OnEnable()
    {
        Controls.Enable();
    }

    private void Toggle2D_performed()
    {
        //2D input controles
        constraintToggle = !constraintToggle;
        if (constraintToggle)
        {
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            rb.constraints = rbConstraints;
        }
    }
    private void Move(Vector2 direction)
    {
        moveDir.x = direction.x;
        moveDir.z = direction.y;
    }
    private void Dash()
    {
        if (dashs > 0)
        {
            Vector3 DashDir;
            if (moveDir != new Vector3(0, 0, 0))
            {
                DashDir = new Vector3(moveDir.x, 0, moveDir.z);
            }
            else
            {
                DashDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
            DashDir = Vector3.Normalize(DashDir);
            rb.velocity = DashDir * DashForce;
            dashs--;
        }
    }

    private void Jump()
    {
        if (jumps > 0 || detectWall() != new Vector3(0, 0, 0) || isGrounded())
        {
            Vector3 Jumpdir = Vector3.up + detectWall();
            rb.AddForce(Jumpdir * JumpForce, ForceMode.VelocityChange);
            if (detectWall() == new Vector3(0, 0, 0))
            {
                jumps--;
            }
        }
    }

    private void Drop()
    {
        int CC = transform.childCount;
        for (int i = 0; i < CC; i++)
        {
            GameObject child = transform.GetChild(0).gameObject;
            child.transform.parent = null;
            child.AddComponent<Rigidbody>();
            Speed -= ForceBoost;
            rb.mass -= MassBoost;
        }
        pickup = !pickup;
    }

    private void Update()
    {
        rb.AddForce(moveDir * Speed);
        if (isGrounded() && (jumps < MidAirJumps || dashs < MidAirDashs))
        {
            jumps = MidAirJumps;
            dashs = MidAirDashs;
        }
    }

    private void OnDisable()
    {
        Controls.Player.Move.performed -= ctx => Move(ctx.ReadValue<Vector2>());
        Controls.Player.Dash.performed -= _ => Dash();
        Controls.Player.Drop.performed -= _ => Drop();
        Controls.Player.Jump.performed -= _ =>Jump();
        Controls.Debug.toggle2D.performed -= _ => Toggle2D_performed();
        Controls.Disable();
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround+0.1f);
    }
    
    Vector3 detectWall()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, distToGround + 0.1f))
        {
            return Vector3.back;
        }
        else if (Physics.Raycast(transform.position, Vector3.back, distToGround + 0.1f))
        {
            return Vector3.forward;
        }
        else if (Physics.Raycast(transform.position, Vector3.left, distToGround + 0.1f))
        {
            return Vector3.right;
        }
        else if (Physics.Raycast(transform.position, Vector3.right, distToGround + 0.1f))
        {
            return Vector3.left;
        }
        else
        {
            return new Vector3(0,0,0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer" && transform.childCount > 0)
        {
            int CC = transform.childCount;
            for (int i = 0; i < CC; i++)
            {
                Collectablefix child = transform.GetChild(0).gameObject.GetComponent<Collectablefix>();
                //child.transform.parent = null;
                //Destroy(child);
                child.DistroyObject();
                Speed -= ForceBoost;
                rb.mass -= MassBoost;
            }
            //gc.UpdateScoreBoard(CC);
        }
        if (other.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Collectable" && pickup == true)
        {
            collision.transform.parent = transform;
            if (collision.transform.GetComponent<Rigidbody>() != null)
            {
                Destroy(collision.transform.GetComponent<Rigidbody>());
            }
            Speed += ForceBoost;
            rb.mass += MassBoost;
            gc.AddTime(PickupBonusTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (isGrounded())
        {
            Debug.DrawRay(transform.position, Vector3.down, activeColour, distToGround + 0.1f);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down, inactiveColour, distToGround + 0.1f);
        }
        if (detectWall() != new Vector3(0,0,0))
        {
            Debug.DrawRay(transform.position, Vector3.forward, activeColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.back, activeColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.left, activeColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.right, activeColour, distToGround + 0.1f);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.forward, inactiveColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.back, inactiveColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.left, inactiveColour, distToGround + 0.1f);
            Debug.DrawRay(transform.position, Vector3.right, inactiveColour, distToGround + 0.1f);
        }
        
    }
}
