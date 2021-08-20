using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    //[SerializeField] string itemName;
    //[SerializeField] string description;

    [SerializeField] float distanceToActivate = 2f;
    [SerializeField] Effect[] effects;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckIfThereIsPlayer();
        }
    }

    private void CheckIfThereIsPlayer()
    {
        Collider[] players = Physics.OverlapSphere(this.transform.position, distanceToActivate);
        foreach (var player in players)
        {
            if (player.CompareTag("Player"))
            {
                Use();
            }
        }
    }

    public void Use()
    { 
        for(int i = 0; i <effects.Length; i++)
        {
            effects[i].Apply();
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToActivate);
    }

}
