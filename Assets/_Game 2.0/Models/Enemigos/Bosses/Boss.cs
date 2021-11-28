using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator ac;
    void Start()
    {
        ac = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ac.SetBool("Awake", true);
            StartCoroutine(wait());

        }
    }

    IEnumerator wait()
    {

        yield return new WaitForSeconds(1);
        ac.SetBool("Idle", true);
        
    }

}
