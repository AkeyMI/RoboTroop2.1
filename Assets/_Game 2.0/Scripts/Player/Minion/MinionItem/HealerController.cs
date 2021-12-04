using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{
    [SerializeField] MinionItemData data = default;

    Animator ac;
    private bool firstTime = true;
    //private MinionItemData data;
    private void Start()
    {
        ac = GetComponent<Animator>();
        ac.SetBool("Heal", true);
        
    }
    public void Init(MinionItemData itemData)
    {
        data = itemData;
    }

    private void OnEnable()
    {
        if(firstTime)
        {
            firstTime = false;
            return;
        }

        StartCoroutine(CureCoroutine());
    }

    private IEnumerator CureCoroutine()
    {
        FindObjectOfType<SpawnerPool>().GetParticle(8, transform.position);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.5f);
            data.effect.Apply();
        }

        this.gameObject.SetActive(false);
    }
}
