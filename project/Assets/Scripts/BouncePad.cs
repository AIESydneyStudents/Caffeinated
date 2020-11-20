using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float BounceForce;
    public string[] JumpTagBlacklist;
    public AudioClip bouncePadSoundEffect;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && !BlackListCheck(other.tag))
        {
            anim.SetTrigger("playerColided");
            //other.GetComponent<Rigidbody>().velocity = transform.up * BounceForce;
            AudioSource.PlayClipAtPoint(bouncePadSoundEffect, Camera.main.transform.position, 1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && !BlackListCheck(other.tag))
        {
            //anim.SetTrigger("playerColided");
            other.GetComponent<Rigidbody>().velocity = transform.up * BounceForce;
        }
    }
    private bool BlackListCheck(string tag)
    {
        foreach (string tags in JumpTagBlacklist)
        {
            if (tag == tags)
            {
                return true;
            }
        }
        return false;
    }
}
