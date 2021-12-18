using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , IRoomActivables
{
    [SerializeField] bool locked;
    Animator animator;
    bool waveTime;
    bool open;
    [SerializeField] GameObject shader;
    [SerializeField] bool stillCloseOnExit;

    SpawnerPool sp;
    private void Start() => sp = FindObjectOfType<SpawnerPool>();

    private void Awake() => animator = this.GetComponent<Animator>();

    public void Activate()
    {
        waveTime = true;
        locked = true;
        animator.SetBool("Open", false);
        shader.GetComponent<MeshRenderer>().material.SetFloat("_status", 1);
    }

    public void Deactivate()
    {
        if (open)
            Status(true);
        locked = false;
        waveTime = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !waveTime)
        {
            if (locked && other.GetComponent<CharacterController>().key)
            {
                locked = false;
                other.GetComponent<CharacterController>().key = false;
                Status(true);
            }
            if (!locked && !open)
                Status(true);
        }           
    }

    void Status(bool status)
    {
        animator.SetBool("Loked", !status);
        animator.SetBool("Open", status);
        open = status;
        sp.GetSound(21);
        if(shader != null)
            shader.GetComponent<MeshRenderer>().material.SetFloat("_status", -1);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !waveTime && !locked && open && stillCloseOnExit)
            Status(false);
    }
}
