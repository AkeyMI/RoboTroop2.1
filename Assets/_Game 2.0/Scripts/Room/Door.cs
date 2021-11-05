using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IRoomActivables
{
    bool locked = true;
    Animator animator;
    bool waveTime;
    bool open;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public void Activate()
    {
        if(!locked)
            animator.SetTrigger("Close");
        waveTime = true;
        locked = true;
    }

    public void Deactivate()
    {
        locked = false;
        waveTime = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !waveTime)
        {
            if (locked && other.GetComponent<CharacterController>().key)
            {
                other.GetComponent<CharacterController>().key = false;
                locked = false;
                animator.SetTrigger("Open");
                open = true;
            }
            if (!locked && open)
            {
                animator.SetTrigger("Open");
                open = true;
            }
        }           
    }
}
