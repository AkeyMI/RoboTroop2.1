using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IRoomActivables
{
    Animator animator;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void Activate()
    {
        animator.SetTrigger("Close");
    }

    public void Deactivate()
    {
        animator.SetTrigger("Open");
    }
}
