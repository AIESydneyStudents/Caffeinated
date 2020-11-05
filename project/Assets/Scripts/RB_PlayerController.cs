using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_PlayerController : MonoBehaviour
{
    private float giveDist = 0.1f;
    public float rotateBuffer = 0.1f;
    //Is Grounded check
    public GameObject curHitObject;
    private float currentHitDistance;
    public Vector3 boxHalfExtentsG;
    public float boxDistanceG;
    //public float sphereRadius;
    //private float sphereDistance;

    //Wall Jump check
    public GameObject curHitObjectW;
    private float currentHitDistanceW;
    public Vector3 boxHalfExtents;
    public float boxDistance;

    public float GroundSpeed = 1;
    public float AirSpeed = 1;
    public float GravMultiplyer = 1;
    public float VelocityCap = 50;
    public float PickupBonusTime = 5f;
    //private float ForceBoost = 0.8f;
    //private float MassBoost = 0.1f;
    public float DashForce = 10;
    public float JumpForce = 5;
    public int MidAirJumps = 1;
    public int MidAirDashs = 1;
    public float CoyoteTime = 0.2f;
    public bool AddForceJumps;
    public bool AddForceWallJumps;
    public bool AddForceDashs;
    public string[] JumpTagBlacklist;
    public float HitStunDuration;
    public float HitGracePeriod;
    public float SpeedBoost = 1f;
    public bool invulnerable;

    private PlayerControls Controls;
    private float Speed;
    public Vector3 moveDir;
    private int jumps;
    private int dashs;
    private float distToGround;
    private float distToWall;
    private Rigidbody rb;
    private RigidbodyConstraints rbConstraints;
    private bool pickup = true;
    private GameController gc;
    private bool constraintToggle = false;
    
    private bool stuned = false;
    public bool grounded = true;

    private float timer;
    private List<GameObject> Colectables; 

    // Start is called before the first frame update
    private void Awake()
    {
        Controls = new PlayerControls();
        Controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        Controls.Player.Dash.performed += _ => Dash();
        Controls.Player.Drop.performed += _ => Drop();
        Controls.Player.Jump.performed += _ => Jump();
        Controls.Player.Slam.performed += _ => Slam();
        Controls.Debug.toggle2D.performed += _ => Toggle2D_performed();

        Colectables = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        rbConstraints = rb.constraints;
        distToGround = GetComponent<BoxCollider>().bounds.extents.y;
        distToWall = GetComponent<BoxCollider>().bounds.extents.x;

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        jumps = MidAirJumps;
        dashs = MidAirDashs;

        if ((int)(rb.constraints & RigidbodyConstraints.FreezePositionZ) == 8)
        {
            constraintToggle = true;
        }

        boxHalfExtentsG = new Vector3(GetComponent<BoxCollider>().bounds.extents.x*2, 0.1f, GetComponent<BoxCollider>().bounds.extents.z*2);
        boxDistanceG = distToGround - boxHalfExtentsG.y + giveDist;

        boxHalfExtents = new Vector3(0.1f, (distToGround - distToWall) * 2, GetComponent<BoxCollider>().bounds.extents.z);
        boxDistance = distToWall - boxHalfExtents.x + giveDist;
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
            if (AddForceDashs)
            {
                if (rb.velocity.x * DashDir.x < 0)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                }
                if (rb.velocity.y * DashDir.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                rb.AddForce(DashDir * DashForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.velocity = DashDir * DashForce;
            }
            dashs--;
        }
    }

    private void Slam()
    {
        if (dashs > 0)
        {
            Vector3 DashDir = new Vector3(0,-1,0);
            if (AddForceDashs)
            {
                if (rb.velocity.y * DashDir.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                rb.AddForce(DashDir * DashForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.velocity = DashDir * DashForce;
            }
            dashs--;
        }
    }

    private void Jump()
    {
        if (detectWall() != new Vector3(0, 0, 0) && !grounded)
        {
            Vector3 Jumpdir = Vector3.up + detectWall();
            if (AddForceWallJumps)
            {
                rb.AddForce(Jumpdir * JumpForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.velocity = VelocityOverride(Jumpdir * JumpForce, rb.velocity);
            }
            grounded = false;
        }
        else if (jumps > 0 || grounded)
        {
            Vector3 Jumpdir = Vector3.up;
            if (AddForceJumps)
            {
                rb.AddForce(Jumpdir * JumpForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.velocity = VelocityOverride(Jumpdir * JumpForce, rb.velocity);
            }
            if (!grounded)
            {
                jumps--;
            }
            grounded = false;
        }

    }

    private void Drop()
    {
        foreach (GameObject colectable in Colectables)
        {
            colectable.transform.parent = null;
            colectable.AddComponent<Rigidbody>();
            colectable.AddComponent<MeshCollider>();
        }
        pickup = !pickup;
    }
    private void Update()
    {
        if (!isGrounded())
        {
            timer += Time.deltaTime;
            if (timer >= CoyoteTime)
            {
                grounded = false;
            }
        }
        else
        {
            if (Controls.Player.Jump.phase != UnityEngine.InputSystem.InputActionPhase.Performed)
            {
                timer = 0;
                grounded = true;
            }           
        }
        if (jumps > MidAirJumps)
        {
            jumps = MidAirJumps;
        }
        if (grounded && (jumps < MidAirJumps || dashs < MidAirDashs))
        {
            jumps = MidAirJumps;
            dashs = MidAirDashs;
        }
    }
    private void FixedUpdate()
    {
        if (grounded)
        {
            Speed = GroundSpeed;
        }
        else
        {
            rb.AddForce(Vector3.down * (9.81f * (GravMultiplyer - 1)));
            Speed = AirSpeed;
        }
        // Rotate based on velocity
        if (rb.velocity.x > rotateBuffer)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                90,
                transform.eulerAngles.z
            );
        }
        if (rb.velocity.x < -rotateBuffer)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                -90,
                transform.eulerAngles.z
            );
        }
        // Velocity Cap
        if (rb.velocity.x > VelocityCap)
        {
            Vector3 newVelocity = new Vector3(VelocityCap, 0, 0);
            rb.velocity = VelocityOverride(newVelocity, rb.velocity);
        }
        if (rb.velocity.x < -VelocityCap)
        {
            Vector3 newVelocity = new Vector3(-VelocityCap, 0, 0);
            rb.velocity = VelocityOverride(newVelocity, rb.velocity);
        }
        if (rb.velocity.y > VelocityCap)
        {
            Vector3 newVelocity = new Vector3(0, VelocityCap, 0);
            rb.velocity = VelocityOverride(newVelocity, rb.velocity);
        }
        if (rb.velocity.y < -VelocityCap)
        {
            Vector3 newVelocity = new Vector3(0, -VelocityCap, 0);
            rb.velocity = VelocityOverride(newVelocity, rb.velocity);
        }
        if (rb.velocity.z > VelocityCap)
        {
            Vector3 newVelocity = new Vector3(0, 0, VelocityCap);
            rb.velocity = VelocityOverride(newVelocity, rb.velocity);
        }
        if (rb.velocity.z < -VelocityCap)
        {
            Vector3 newVelocity = new Vector3(0, 0, -VelocityCap);
            rb.velocity = VelocityOverride(newVelocity, rb.velocity);
        }
        //if (Vector3.Distance(rb.velocity, new Vector3(0, 0, 0)) > VelocityCap)
        //{
        //    rb.velocity = Vector3.Normalize(rb.velocity) * VelocityCap;
        //}
        rb.AddForce(moveDir * Speed * SpeedBoost);

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
        RaycastHit hit;
        //if (Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit, sphereDistance) && !BlackListCheck(hit))
        //{
        //    curHitObject = hit.transform.gameObject;
        //    currentHitDistance = hit.distance;
        //    return true;
        //}
        if (Physics.BoxCast(transform.position, boxHalfExtentsG, -Vector3.up, out hit, Quaternion.identity, boxDistanceG) && !BlackListCheck(hit))
        {
            curHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            return true;
        }
        //currentHitDistance = sphereDistance;
        currentHitDistance = boxDistanceG;
        curHitObject = null;
        return false;
        //return Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround) && !BlackListCheck(hit);
    }
    Vector3 detectWall()
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, boxHalfExtents, transform.forward, out hit, Quaternion.identity, boxDistance) && !BlackListCheck(hit))
        {
            curHitObjectW = hit.transform.gameObject;
            currentHitDistanceW= hit.distance;
            return -transform.forward;
        }
        currentHitDistanceW = boxDistance;
        curHitObjectW = null;
        return new Vector3(0, 0, 0);
    }

    //Vector3 detectWall()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.forward, out hit, distToWall+giveDist) && !BlackListCheck(hit))
    //    {
    //        return Vector3.back;
    //    }
    //    if (Physics.Raycast(transform.position, Vector3.back, out hit, distToWall + giveDist) && !BlackListCheck(hit))
    //    {
    //        return Vector3.forward;
    //    }
    //    if (Physics.Raycast(transform.position, Vector3.left, out hit, distToWall + giveDist) && !BlackListCheck(hit))
    //    {
    //        return Vector3.right;
    //    }
    //    if (Physics.Raycast(transform.position, Vector3.right, out hit, distToWall + giveDist) && !BlackListCheck(hit))
    //    {
    //        return Vector3.left;
    //    }
    //    else
    //    {
    //        return new Vector3(0,0,0);
    //    }
    //}

    private bool BlackListCheck(RaycastHit hit)
    {
        foreach (string tags in JumpTagBlacklist)
        {
            if (hit.transform.tag == tags)
            {
                return true;
            }
        }
        return false;
    }
    public Vector3 VelocityOverride(Vector3 dir, Vector3 rbv)
    {
        Vector3 result = new Vector3(0, 0, 0);
        // Set x
        if (dir.x == 0)
        {
            result.x = rbv.x;
        }
        else
        {
            result.x = dir.x;
        }
        // Set y
        if (dir.y == 0)
        {
            result.y = rbv.y;
        }
        else
        {
            result.y = dir.y;
        }
        // Set z
        if (dir.z == 0)
        {
            result.z = rbv.z;
        }
        else
        {
            result.z = dir.z;
        }
        return result;
    }
    public void Damaged(int score, float time)
    {
        if (!invulnerable && !stuned)
        {
            gc.AddTime(-time);
            gc.UpdateScoreBoard(-score);
            StartCoroutine(Hit());
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer" && transform.childCount > 0)
        {
            foreach (GameObject colectable in Colectables)
            {
                colectable.GetComponent<Collectablefix>().DistroyObject();
            }
            Colectables.Clear();
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
                Destroy(collision.transform.GetComponent<MeshCollider>());
            }
            //Speed += ForceBoost;
            //rb.mass += MassBoost;
            gc.AddTime(PickupBonusTime);
            Colectables.Add(collision.gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * currentHitDistance);
        Gizmos.DrawWireCube(transform.position + Vector3.down * currentHitDistance, boxHalfExtentsG);
        Debug.DrawLine(transform.position, transform.position + transform.forward * currentHitDistanceW);
        Gizmos.DrawWireCube(transform.position + transform.forward * currentHitDistanceW, boxHalfExtents);
    }
    IEnumerator Hit()
    {
        //Instantiate(pickupEffect, transform.position, transform.rotation);
        rb.velocity = new Vector3(0, 0, 0);

        invulnerable = true;
        Controls.Disable();
        moveDir = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(HitStunDuration);
        Controls.Enable();

        yield return new WaitForSeconds(HitGracePeriod);
        invulnerable = false;
    }
}
