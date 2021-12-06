using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinionController : MonoBehaviour
{
    [SerializeField] MinionData minion1 = default;
    [SerializeField] MinionDefenceData minion2 = default;
    [SerializeField] MinionItemData minion3 = default;
    [SerializeField] GameObject minionArt = default;
    [SerializeField] GameObject spawnminionItem = default;
    [SerializeField] int maxMinionsInQueue = 5;
    [SerializeField] bool firtsRound = true;

    private Queue<GameObject> atkMinions = new Queue<GameObject>();
    public List<MinionData> atkMinionsList = new List<MinionData>();
    public List<MinionData> atkMinionsBox = new List<MinionData>();

    public event Action<bool> onChangeMinion;
    public event Action onChangeMinionAtkUi;
    public event Action onClearList;
    public event Action<GameObject> onAddMinionsAtkUi;
    public event Action<GameObject> onChangeMinionShieldUi;
    public event Action<GameObject> onChangeMinionItemUi;
    public event Action<int, int> onChangeFillAmountMinionItem;
    public event Action onMinionItemNoUsable;
    public event Action<Sprite, int> onResetMinionStatus;
    public event Action<int, int> onLifeChange;

    public GameObject MinionAtk => minionAtk;

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
        if (firtsRound)
        {
            minionAtk = Instantiate(minion1.minionPrefab, Vector3.zero, Quaternion.identity);
            minionAtk.transform.SetParent(minionArt.transform, false);
            atkMinions.Enqueue(minionAtk);
            atkMinionsList.Add(minion1);
            onAddMinionsAtkUi?.Invoke(minion1.minionUI);
            onResetMinionStatus?.Invoke(minion1.sprite, minion1.life);
            itemData = minion3;
            minionItem = Instantiate(minion3.minionPrefab, Vector3.zero, Quaternion.identity);
            minionItem.SetActive(false);

            currentMaxMinionsInQueue = 1;
        }
        else
        {
            DataCollector dataCollector = FindObjectOfType<DataCollector>();
            dataCollector.GetMinionsInBagData(this);
            dataCollector.GetMinionsInBoxData(this);
            dataCollector.GetMinionItem(this);
        }

        minionShield = Instantiate(minion2.minionPrefab, Vector3.zero, Quaternion.identity);
        minionShield.transform.SetParent(minionArt.transform, false);
        minionShield.SetActive(false);
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
        //onChangeMinion?.Invoke(minionChange);

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

    public MinionItemData GetMinionItemData()
    {
        return itemData;
    }

    public void SendMinionItem(MinionItemData dataItem)
    {
        itemData = dataItem;
        minionItem = Instantiate(itemData.minionPrefab, Vector3.zero, Quaternion.identity);
        minionItem.SetActive(false);
    }

    public void SendMinionBoxList(List<MinionData> list)
    {
        atkMinionsBox.Clear();
        atkMinionsBox = list;
    }

    public void SendMinionList(List<MinionData> list)
    {
        atkMinionsList.Clear();
        atkMinionsList = list;

        onClearList?.Invoke();

        Destroy(minionAtk);
        minionAtk = Instantiate(atkMinionsList[0].minionPrefab);
        minionAtk.transform.SetParent(minionArt.transform, false);
        onResetMinionStatus?.Invoke(atkMinionsList[0].sprite, atkMinionsList[0].life);
        for (int i = 0; i < atkMinionsList.Count; i++)
        {
            onAddMinionsAtkUi?.Invoke(atkMinionsList[i].minionUI);
        }

        currentMaxMinionsInQueue = atkMinionsList.Count;
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
            onAddMinionsAtkUi?.Invoke(data.minionUI);
            //takentAtkMinion = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            //takentAtkMinion.SetActive(false);

            //atkMinions.Enqueue(takentAtkMinion);

            currentMaxMinionsInQueue++;
            //Debug.Log(currentMaxMinionsInQueue);
        }
        else
        {
            //takentAtkMinion = Instantiate(data.minionPrefab, Vector3.zero, Quaternion.identity);
            //takentAtkMinion.SetActive(false);

            atkMinionsBox.Add(data);
        }
    }

    public void NextMinion()
    {
        currentMaxMinionsInQueue--;

        if(currentMaxMinionsInQueue <= 0)
        {
            GameOver();
        }
        atkMinionsList.Remove(minionAtk.GetComponent<ShootController>().Data);
        Destroy(minionAtk);
        minionAtk = Instantiate(atkMinionsList[0].minionPrefab);

        //minionAtk = atkMinions.Peek();
        minionAtk.transform.SetParent(minionArt.transform, false);
        //minionAtk.SetActive(true);
        //MinionData data = minionAtk.GetComponent<ShootController>().Data;
        onResetMinionStatus?.Invoke(atkMinionsList[0].sprite, atkMinionsList[0].life);
        onChangeMinionAtkUi?.Invoke();
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

    private void GameOver()
    {
        SceneManager.LoadScene(3);
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

    public void ChangeMinionLife(int amount, int life)
    {
        onLifeChange?.Invoke(amount, life);
    }
}
