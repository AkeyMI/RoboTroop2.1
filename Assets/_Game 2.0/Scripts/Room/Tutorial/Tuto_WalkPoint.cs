using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto_WalkPoint : MonoBehaviour
{
    Tuto tuto;
    void Start()
    {
        tuto = FindObjectOfType<Tuto>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tuto.WalkPoint();
            GetComponent<MeshRenderer>().material.color = Color.blue;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
