/*-----------------------------------
    File Name: RB_PlayerController.cs
    Purpose: Control the character
    Author: Ruben Antao
    Modified: 24 November 2020
-------------------------------------
    Copyright 2020 Caffeinated.
-----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_PlayerController : MonoBehaviour
{
    [Header("Tollerance Settings")]
    public float giveDist = 0.1f;
    [Tooltip("Gives extra distance to the ground box cast")]
    public float rotateBuffer = 0.1f;
    [Tooltip("Speed required to rotate character")]
    [Header("Refferences")]
    public UI_PauseScript pauseScript;
    private GameController gameController;
    private Rigidbody rb;
    private DragControl dc;
    private PlayerControls Controls;
    [Header("Grounded Settings & Debugging")]
    public GameObject hitGroundObjects;
    private float groundHitDistance;
    public float CoyoteTime = 0.2f;
    // ------Box cast implementaion------
    public Vector3 groundBoxHalfExtents;
    public float groundBoxDistance;
    // ------Sphere cast implementaion------
    //public float sphereRadius;
    //private float sphereDistance;

    [Header("WallJump Settings & Debugging")]
    public GameObject hitWallObjects;
    private float wallHitDistance;
    public Vector3 wallBoxHalfExtents;
    public float wallBoxDistance;

    [Header("Speed Settings")]
    public float GroundSpeed = 1;
    public float AirSpeed = 1;
    public float VelocityCap = 50;
    [Header("Dash Settings")]
    public bool AddForceDashs;
    public float DashForce = 10;
    public int MidAirDashs = 1;
    [Header("Jump Settings")]
    public bool AddForceJumps;
    public bool AddForceWallJumps;
    public string[] JumpTagBlacklist;
    public float JumpForce = 5;
    public int MidAirJumps = 1;
    public float GravMultiplyer = 1;
    public float noDragTime = 0.5f;

    [Header("Pickup Settings")]
    //private float ForceBoost = 0.8f;
    //private float MassBoost = 0.1f;
    public float PickupBonusTime = 5f;
    [Header("Damage Settings")]
    public float HitStunDuration;
    public float HitGracePeriod;

    [Header("Debuging")]
    public bool invulnerable;
    [SerializeField]
    private int jumps;
    private float Speed;
    public float SpeedBoost = 1f;
    public Vector3 moveDir;
    private int dashs;
    private float distToGround;
    private float distToWall;
    private bool stuned = false;
    public bool grounded = true;
    private bool constraintToggle = false;
    private float timer;
    public Vector3 walled;
    //private parts
    private RigidbodyConstraints rbConstraints;
    private bool pickup = true;
    private List<GameObject> Colectables;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Activating Controles
        Controls = new PlayerControls();
        Controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        Controls.Player.Dash.performed += _ => Dash();
        Controls.Player.Drop.performed += _ => Drop();
        Controls.Player.Jump.performed += _ => Jump();
        Controls.Player.Slam.performed += _ => Slam();
        Controls.Player.Pause.performed += _ => Pause();
        Controls.Debug.toggle2D.performed += _ => Toggle2D_performed();
        // Loading private refferences
        Colectables = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        dc = GetComponent<DragControl>();
        distToGround = GetComponent<BoxCollider>().bounds.extents.y;
        distToWall = GetComponent<BoxCollider>().bounds.extents.x;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // Leading variables
        rbConstraints = rb.constraints;
        jumps = MidAirJumps;
        dashs = MidAirDashs;
        // Checking exisiting restraints
        if ((int)(rb.constraints & RigidbodyConstraints.FreezePositionZ) == 8)
        {
            // constrain z axis toggle
            constraintToggle = true;
        }
        // Calculate extents and distance for ground detection
        groundBoxHalfExtents = new Vector3(GetComponent<BoxCollider>().bounds.extents.x*2, 0.1f, GetComponent<BoxCollider>().bounds.extents.z*2);
        groundBoxDistance = distToGround - groundBoxHalfExtents.y + giveDist;
        // Calculate extents and distance for wall detection
        wallBoxHalfExtents = new Vector3(0.1f, (distToGround - distToWall) * 2, GetComponent<BoxCollider>().bounds.extents.z);
        wallBoxDistance = distToWall - wallBoxHalfExtents.x + giveDist;


    }

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        Controls.Enable();
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    private void Pause()
    {
        pauseScript.PauseToggle();
    }

    /// <summary>
    /// Toggle 2D mode
    /// </summary>
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

    /// <summary>
    /// Move character
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector2 direction)
    {
        // save direction to move
        moveDir.x = direction.x;
        moveDir.z = direction.y;
    }

    /// <summary>
    /// Allow the player to dash
    /// </summary>
    private void Dash()
    {
        // Check if the player has a dash
        if (dashs > 0)
        {
            // Get direction to dash
            Vector3 DashDir;
            if (moveDir != new Vector3(0, 0, 0))
            {
                DashDir = new Vector3(moveDir.x, 0, moveDir.z);
            }
            else
            {
                DashDir = transform.forward;
            }
            DashDir = Vector3.Normalize(DashDir);
            if (AddForceDashs)
            {
                // Use AddForce to dash
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
                // Set velocity to dash
                rb.velocity = DashDir * DashForce;
            }
            // Remove a dash
            dashs--;
        }
    }

    /// <summary>
    /// Allow the player to ground slam
    /// </summary>
    private void Slam()
    {
        // Check if the player has a dash
        if (dashs > 0)
        {
            // Set dash direction to Down
            Vector3 DashDir = new Vector3(0,-1,0);

            if (AddForceDashs)
            {
                // Use AddForce to dash
                if (rb.velocity.y * DashDir.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                rb.AddForce(DashDir * DashForce, ForceMode.VelocityChange);
            }
            else
            {
                // Set velocity to dash
                rb.velocity = DashDir * DashForce;
            }
            // Remove a dash
            dashs--;
        }
    }

    /// <summary>
    /// Allow player to jump
    /// </summary>
    private void Jump()
    {
        // Check for wall jump criteria
        if (walled != new Vector3(0, 0, 0) && !grounded)
        {
            // Set Jump direction
            Vector3 Jumpdir = Vector3.up + walled;
            if (AddForceWallJumps)
            {
                // Use AddForce to wall jump
                rb.AddForce(Jumpdir * JumpForce, ForceMode.VelocityChange);
            }
            else
            {
                // Set velocity to wall jump
                rb.velocity = VelocityOverride(Jumpdir * JumpForce, rb.velocity);
            }
            // Bypass Coyotetime
            grounded = false;
        }
        // Check for jump criteria
        else if (jumps > 0 || grounded)
        {
            // Set Jump direction
            Vector3 Jumpdir = Vector3.up;
            if (AddForceJumps)
            {
                // Use AddForce to jump
                rb.AddForce(Jumpdir * JumpForce, ForceMode.VelocityChange);
            }
            else
            {
                // Set velocity to jump
                rb.velocity = VelocityOverride(Jumpdir * JumpForce, rb.velocity);
            }
            // remove a jump if mid-air
            if (!grounded)
            {
                jumps--;
            }
            // Bypass Coyotetime
            grounded = false;
        }
    }

    // Depreciated
    /// <summary>
    /// Remove collectables from inventory
    /// </summary>
    private void Drop()
    {
        // Used to remove colectables from inventory
        foreach (GameObject colectable in Colectables)
        {
            colectable.transform.parent = null;
            colectable.AddComponent<Rigidbody>();
            colectable.AddComponent<MeshCollider>();
        }
        pickup = !pickup;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    private void Update()
    {
        // Coyote time implementation
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
            // reset time and set player grounded when player touches the ground
            if (Controls.Player.Jump.phase != UnityEngine.InputSystem.InputActionPhase.Performed)
            {
                timer = 0;
                grounded = true;
            }           
        }
        // Detect wall
        walled = detectWall();
        // Ensure that extra jumps are lost when jump powerup finishes
        if (jumps > MidAirJumps)
        {
            jumps = MidAirJumps;
        }
        // Reset jumps and dashes when touching the ground
        if (grounded && (jumps < MidAirJumps || dashs < MidAirDashs))
        {
            jumps = MidAirJumps;
            dashs = MidAirDashs;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled
    /// </summary>
    private void FixedUpdate()
    {
        // Setting current speed
        if (isGrounded())
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
        // Velocity Cap: checking every axis speed and reducing speed if current speed exceeds velocity cap
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
        /*
         * Depreciated velocity cap implementaion
        if (Vector3.Distance(rb.velocity, new Vector3(0, 0, 0)) > VelocityCap)
        {
            rb.velocity = Vector3.Normalize(rb.velocity) * VelocityCap;
        }
        */
        // Set speed
        rb.AddForce(moveDir * Speed * SpeedBoost);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        // Disable Controles
        Controls.Player.Move.performed -= ctx => Move(ctx.ReadValue<Vector2>());
        Controls.Player.Dash.performed -= _ => Dash();
        Controls.Player.Drop.performed -= _ => Drop();
        Controls.Player.Jump.performed -= _ =>Jump();
        Controls.Player.Pause.performed -= _ => Pause();
        Controls.Debug.toggle2D.performed -= _ => Toggle2D_performed();
        Controls.Disable();
    }
    // Disable Drag temperarily
    /// <summary>
    /// Disable drag temporarily
    /// </summary>
    /// <returns></returns>
    IEnumerable DragStop()
    {
        float temp = dc.airFriction;
        dc.airFriction = 0;
        yield return new WaitForSeconds(noDragTime);
        dc.airFriction = temp;
    }

    /// <summary>
    /// Check if the player is grounded
    /// </summary>
    /// <returns>True if player is on the ground</returns>
    bool isGrounded()
    {
        // Setup Hit
        RaycastHit hit;
        // ------ Sphere cast implementaion ------
        //if (Physics.SphereCast(transform.position, sphereRadius, -Vector3.up, out hit, sphereDistance) && !BlackListCheck(hit))
        //{
        // Get details
        //    curHitObject = hit.transform.gameObject;
        //    currentHitDistance = hit.distance;
        // return
        //    return true;
        //}
        // Get details
        //groundHitDistance = sphereDistance;
        //hitGroundObjects = null;
        // return
        //return false;
        // ------ Box cast implementaion ---------
        // Check if box colides and if object is ground
        if (Physics.BoxCast(transform.position, groundBoxHalfExtents, -Vector3.up, out hit, Quaternion.identity, groundBoxDistance) && !BlackListCheck(hit))
        {
            // Get details
            hitGroundObjects = hit.transform.gameObject;
            groundHitDistance = hit.distance;
            // return
            return true;
        }
        // Get details
        //currentHitDistance = sphereDistance;
        groundHitDistance = groundBoxDistance;
        hitGroundObjects = null;
        // return
        return false;
        // ------- Ray cast implementaion ------
        //return Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround) && !BlackListCheck(hit);
    }

    /// <summary>
    /// Detect a wall
    /// </summary>
    /// <returns></returns>
    Vector3 detectWall()
    {
        // Setup Hit
        RaycastHit hit;
        // Check if box colides and if object is wall
        if (Physics.BoxCast(transform.position, wallBoxHalfExtents, transform.forward, out hit, Quaternion.identity, wallBoxDistance) && !BlackListCheck(hit))
        {
            // Get details
            hitWallObjects = hit.transform.gameObject;
            wallHitDistance= hit.distance;
            // return
            return -transform.forward;
        }
        // Get details
        wallHitDistance = wallBoxDistance;
        hitWallObjects = null;
        // return
        return new Vector3(0, 0, 0);
    }
    //----- Depreciated-----
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

    /// <summary>
    /// Check if the player can jump on this object
    /// </summary>
    /// <param name="hit">The object the player is touching</param>
    /// <returns>True if the player can't jump on it</returns>
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

    /// <summary>
    /// Override the player's current velocity
    /// </summary>
    /// <param name="dir">New direction for the player</param>
    /// <param name="rbv">Current direction of the player</param>
    /// <returns></returns>
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

    /// <summary>
    /// Lose score and time if the player gets hit by an obstacle 
    /// </summary>
    /// <param name="score">Score to remove</param>
    /// <param name="time">Time to remove</param>
    public void Damaged(int score, float time)
    {
        if (!invulnerable && !stuned)
        {
            gameController.AddTime(-time);
            gameController.UpdateScoreBoard(-score);
            StartCoroutine(Hit());
        }    
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
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

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    /// </summary>
    /// <param name="collision"></param>
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
            gameController.AddTime(PickupBonusTime);
            Colectables.Add(collision.gameObject);
        }
    }

    /// <summary>
    /// Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundHitDistance);
        Gizmos.DrawWireCube(transform.position + Vector3.down * groundHitDistance, groundBoxHalfExtents);
        Debug.DrawLine(transform.position, transform.position + transform.forward * wallHitDistance);
        Gizmos.DrawWireCube(transform.position + transform.forward * wallHitDistance, wallBoxHalfExtents);
    }

    /// <summary>
    /// Disable controls for the player while they are stunned
    /// </summary>
    /// <returns></returns>
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
