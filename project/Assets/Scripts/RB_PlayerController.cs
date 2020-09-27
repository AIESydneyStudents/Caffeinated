using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RB_PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public float ForceBoost = 0.8f;
    public float MassBoost = 0.1f;
    public float JumpForce = 5;
    public GameObject ScoreBoard;

    private float distToGround;
    private Rigidbody rb;
    private bool pickup = true;
    private TextMeshProUGUI sb;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        sb = ScoreBoard.GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * Speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int CC = transform.childCount;
            for (int i = 0; i < CC; i++)
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.transform.parent = null;
                Destroy(child);
                Speed -= ForceBoost;
                rb.mass -= MassBoost;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            pickup = !pickup;
        }
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    void UpdateScoreBoard()
    {
        sb.text = "Score: " + score;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer" && transform.childCount > 0)
        {
            int CC = transform.childCount;
            for (int i = 0; i < CC; i++)
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.transform.parent = null;
                Destroy(child);
                Speed -= ForceBoost;
                rb.mass -= MassBoost;
                score++;
            }
        }
        UpdateScoreBoard();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Collectable" && pickup == true)
        {
            collision.transform.parent = transform;
            Speed += ForceBoost;
            rb.mass += MassBoost;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red, distToGround + 0.1f);
    }
}
