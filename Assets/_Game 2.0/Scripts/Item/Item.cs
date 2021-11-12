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
    [SerializeField] bool autoCollect = false;

    private bool isClose;

    private void Update()
    {
        CheckIfThereIsPlayer();

        if((Input.GetKeyDown(KeyCode.E) || autoCollect) && isClose)
        {
            Use();
        }

    }

    private void CheckIfThereIsPlayer()
    {
        bool isPLayerColse = false;
        Collider[] players = Physics.OverlapSphere(this.transform.position, distanceToActivate);
        foreach (var player in players)
        {
            if (player.CompareTag("AtkMinion"))
            {
                isPLayerColse = true;
                
            }
        }

        if(isPLayerColse)
        {
            isClose = true;
            if (eImage != null)
                eImage.enabled = true;
        }
        else
        {
            isClose = false;
            if (eImage != null)
                eImage.enabled = false;
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
