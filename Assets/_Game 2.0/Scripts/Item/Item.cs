using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //[SerializeField] string itemName;
    //[SerializeField] string description;

    [SerializeField] float distanceToActivate = 2f;
    [SerializeField] Effect[] effects;
    [SerializeField] Image eImage = default;

    private bool isClose;

    private void Update()
    {
        CheckIfThereIsPlayer();

        if(Input.GetKeyDown(KeyCode.E) && isClose)
        {
            //CheckIfThereIsPlayer();
            Use();
        }
    }

    private void CheckIfThereIsPlayer()
    {
        Collider[] players = Physics.OverlapSphere(this.transform.position, distanceToActivate);
        foreach (var player in players)
        {
            if (player.CompareTag("Player"))
            {
                //Use();
                eImage.enabled = true;
                isClose = true;
            }
            else
            {
                eImage.enabled = false;
                isClose = false;
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
