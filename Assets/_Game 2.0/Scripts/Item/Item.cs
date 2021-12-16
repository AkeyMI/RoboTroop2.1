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
    [SerializeField] int particlesWhenTaked;
    [SerializeField] int particlesOnMinion;
    bool isClose;
    bool taked = false;
    private void Update()
    {
        CheckIfThereIsPlayer();

        if((Input.GetKeyDown(KeyCode.E) || autoCollect) && isClose && !taked)
            StartCoroutine(Use());
    }

    private void CheckIfThereIsPlayer()
    {
        bool isPLayerColse = false;
        Collider[] players = Physics.OverlapSphere(this.transform.position, distanceToActivate);
        foreach (var player in players)
        {
            if (player.CompareTag("AtkMinion"))
                isPLayerColse = true;
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

    IEnumerator Use()
    {
        taked = true;
        FindObjectOfType<SpawnerPool>().GetParticle(particlesWhenTaked, transform.position);
      
        for (int i = 0; i <effects.Length; i++)
            effects[i].Apply();

        float n = 0.25f;
        while (n > 0)
        {
            n -= Time.deltaTime;
            transform.localScale -= new Vector3(n * 0.5f, n * 0.5f, n * 0.5f); 
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToActivate);
    }

}
