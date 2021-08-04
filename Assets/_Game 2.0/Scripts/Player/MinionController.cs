using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [SerializeField] MinionData minion1 = default;
    [SerializeField] MinionDefenceData minion2 = default;
    [SerializeField] MinionItemData minion3 = default;
    [SerializeField] GameObject minionArt = default;

    public event Action<bool> onChangeMinion;
    public event Action<GameObject> onChangeMinionAtkUi;
    public event Action<GameObject> onChangeMinionShieldUi;
    public event Action<GameObject> onChangeMinionItemUi;

    private GameObject minionAtk;
    private GameObject minionShield;
    private GameObject minionItem;
    private MinionItemData itemData;

    private int currentReloadUlti;

    private bool minionChange = false;

    private bool canUseUlti;

    //private bool firstItemMinion = true;

    private void Start()
    {
        minionAtk = Instantiate(minion1.minionPrefab, Vector3.zero, Quaternion.identity);
        minionAtk.transform.SetParent(minionArt.transform, false);
        //onChangeMinionAtkUi?.Invoke(minion1.minionUI);

        minionShield = Instantiate(minion2.minionPrefab, Vector3.zero, Quaternion.identity);
        minionShield.transform.SetParent(minionArt.transform, false);
        minionShield.SetActive(false);
        //onChangeMinionShieldUi?.Invoke(minion2.minionUi);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ChangeMinion();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseMinion();
        }
    }

    private void ChangeMinion()
    {
        minionAtk.SetActive(minionChange);
        onChangeMinion?.Invoke(minionChange);

        minionChange = !minionChange;

        minionShield.SetActive(minionChange);
    }

    private void UseMinion()
    {
        if (!CanUseUlti()) return;

        //canUseUlti = false;
        currentReloadUlti = itemData.reloadUlti;
        itemData.effect.Apply();
    }

    private bool CanUseUlti()
    {
        if(currentReloadUlti > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ReloadUlti()
    {
        currentReloadUlti--;
    }

    public void ChangeAtkMinion(MinionData data)
    {
        if (minionAtk.activeSelf)
        {
            Destroy(minionAtk);
            minionAtk = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            minionAtk.transform.SetParent(minionArt.transform, false);
        }
        else
        {
            Destroy(minionAtk);
            minionAtk = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            minionAtk.transform.SetParent(minionArt.transform, false);
            minionAtk.SetActive(false);
        }

        onChangeMinionAtkUi?.Invoke(data.minionUI);
    }

    public void ChangeShieldMinion(MinionDefenceData data)
    {
        if (minionShield.activeSelf)
        {
            Destroy(minionShield);
            minionShield = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            minionShield.transform.SetParent(minionArt.transform, false);
        }
        else
        {
            Destroy(minionShield);
            minionShield = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            minionShield.transform.SetParent(minionArt.transform, false);
            minionShield.SetActive(false);
        }

        onChangeMinionShieldUi?.Invoke(data.minionUi);
    }

    public void ChangeItemMinion(MinionItemData data)
    {
        Destroy(minionItem);
        itemData = data;
        minionItem = Instantiate(itemData.minionPrefab, Vector3.zero, Quaternion.identity);
        minionItem.transform.SetParent(minionArt.transform, false);
        currentReloadUlti = itemData.reloadUlti;
    }
}
