using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IRoomActivables
{
    [SerializeField] bool locked;
    Animator animator;
    bool waveTime;
    bool open;
    [SerializeField]GameObject shader;
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
        shader.GetComponent<MeshRenderer>().material.SetFloat("_status", 1);
    }

    public void Deactivate()
    {
        if (open)
        {
            animator.SetTrigger("Open");
            shader.GetComponent<MeshRenderer>().material.SetFloat("_status", -1);
        }
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
                shader.GetComponent<MeshRenderer>().material.SetFloat("_status", -1);
                open = true;
            }
            if (!locked && !open)
            {
                animator.SetTrigger("Open");
                shader.GetComponent<MeshRenderer>().material.SetFloat("_status", -1);
                open = true;
            }
        }           
    }
}
