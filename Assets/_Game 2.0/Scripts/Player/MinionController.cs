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
    [SerializeField] GameObject spawnminionItem = default;
    [SerializeField] int maxMinionsInQueue = 5;

    private Queue<GameObject> atkMinions = new Queue<GameObject>();
    private List<MinionData> atkMinionsList = new List<MinionData>();
    private List<MinionData> atkMinionsBox = new List<MinionData>();

    public event Action<bool> onChangeMinion;
    public event Action<GameObject> onChangeMinionAtkUi;
    public event Action<GameObject> onChangeMinionShieldUi;
    public event Action<GameObject> onChangeMinionItemUi;
    public event Action<int, int> onChangeFillAmountMinionItem;
    public event Action onMinionItemNoUsable;

    private GameObject minionAtk;
    private GameObject minionShield;
    private GameObject minionItem;
    private MinionItemData itemData;

    private int currentMaxMinionsInQueue;

    private GameObject takentAtkMinion;

    private int currentReloadUlti;
    private int reloadUltiUi = 3;

    private bool minionChange = false;

    private bool canUseUlti;

    [SerializeField] KeyCode shieldKey;
    [SerializeField] KeyCode ultiKey;

    //private bool firstItemMinion = true;

    private void Start()
    {
        minionAtk = Instantiate(minion1.minionPrefab, Vector3.zero, Quaternion.identity);
        minionAtk.transform.SetParent(minionArt.transform, false);
        atkMinions.Enqueue(minionAtk);
        atkMinionsList.Add(minion1);
        //onChangeMinionAtkUi?.Invoke(minion1.minionUI);

        minionShield = Instantiate(minion2.minionPrefab, Vector3.zero, Quaternion.identity);
        minionShield.transform.SetParent(minionArt.transform, false);
        minionShield.SetActive(false);
        //onChangeMinionShieldUi?.Invoke(minion2.minionUi);

        itemData = minion3;
        minionItem = Instantiate(minion3.minionPrefab, Vector3.zero, Quaternion.identity);
        minionItem.SetActive(false);

        currentMaxMinionsInQueue = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(shieldKey))
            ChangeMinion();
        
        if(Input.GetKeyUp(shieldKey))
            ChangeMinion();       

        if(Input.GetKeyDown(ultiKey))
            UseMinion();
        
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
        if (!CanUseUlti())
        {
            onMinionItemNoUsable?.Invoke();
            return;
        }

        //canUseUlti = false;
        currentReloadUlti = itemData.reloadUlti;
        reloadUltiUi = 0;
        onChangeFillAmountMinionItem?.Invoke(reloadUltiUi, itemData.reloadUlti);
        //GameObject item = Instantiate(itemData.minionPrefab, transform.position, Quaternion.identity);
        minionItem.transform.position = spawnminionItem.transform.position;
        minionItem.SetActive(true);
        //minionItem.GetComponent<HealerController>().Init(itemData);
        //itemData.effect.Apply();
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
        reloadUltiUi++;

        if (reloadUltiUi >= itemData.reloadUlti)
            reloadUltiUi = itemData.reloadUlti;

        onChangeFillAmountMinionItem?.Invoke(reloadUltiUi, itemData.reloadUlti);
    }

    public List<MinionData> GetMinionBoxDataList()
    {
        //Agregar los minions de la mochila
        return atkMinionsBox;
    }

    public List<MinionData> GetMinionDataList()
    {
        //Agregar los minions de la mochila
        return atkMinionsList;
    }

    public void ChangeAtkMinion(MinionData data)
    {
        //if (minionAtk.activeSelf)
        //{
        //    Destroy(minionAtk);
        //    minionAtk = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
        //    minionAtk.transform.SetParent(minionArt.transform, false);
        //}
        //else
        //{
        //    Destroy(minionAtk);
        //    minionAtk = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
        //    minionAtk.transform.SetParent(minionArt.transform, false);
        //    minionAtk.SetActive(false);
        //}

        if (currentMaxMinionsInQueue < maxMinionsInQueue)
        {
            atkMinionsList.Add(data);
            //takentAtkMinion = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            //takentAtkMinion.SetActive(false);

            //atkMinions.Enqueue(takentAtkMinion);

            currentMaxMinionsInQueue++;
            Debug.Log(currentMaxMinionsInQueue);
        }
        else
        {
            //takentAtkMinion = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            //takentAtkMinion.SetActive(false);

            atkMinionsBox.Add(data);
        }

        //onChangeMinionAtkUi?.Invoke(data.minionUI);
    }

    public void NextMinion()
    {
        atkMinionsList.Remove(minionAtk.GetComponent<ShootController>().Data);
        Destroy(minionAtk);
        //atkMinions.Dequeue();
        minionAtk = Instantiate(atkMinionsList[0].minionPrefab);
        currentMaxMinionsInQueue--;

        //minionAtk = atkMinions.Peek();
        minionAtk.transform.SetParent(minionArt.transform, false);
        //minionAtk.SetActive(true);
        //MinionData data = minionAtk.GetComponent<ShootController>().Data;
        onChangeMinionAtkUi?.Invoke(atkMinionsList[0].minionUI);
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
        minionItem.SetActive(false);
        //minionItem.transform.SetParent(minionArt.transform, false);
        currentReloadUlti = itemData.reloadUlti;
        reloadUltiUi = 0;
        onChangeMinionItemUi?.Invoke(data.minionUi);
        onChangeFillAmountMinionItem?.Invoke(reloadUltiUi, itemData.reloadUlti);
    }
}
