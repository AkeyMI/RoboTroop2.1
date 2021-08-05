using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerController : MonoBehaviour
{
    [SerializeField] float timeForCure = 1f;

    private MinionItemData data;

    public void Init(MinionItemData itemData)
    {
        data = itemData;
    }

    private void Start()
    {
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
