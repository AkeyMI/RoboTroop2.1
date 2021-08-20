using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{
    [SerializeField] float timeForCure = 1f;
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

        yield return new WaitForSeconds(timeForCure);

        data.effect.Apply();
        Debug.Log("Hizo el efecto de curacion");

        this.gameObject.SetActive(false);

        //Destroy(this.gameObject);
    }
}
