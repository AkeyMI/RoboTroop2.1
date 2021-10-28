using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] Stunable stunable;
    [SerializeField] Image stunBarImage;
    [SerializeField] GameObject minion1Ui;
    [SerializeField] GameObject minion2Ui;
    [SerializeField] GameObject minion3Ui;
    [SerializeField] GameObject minionIconParent;
    [SerializeField] GameObject minionItemIconParent;
    [SerializeField] GameObject pauseMenu = default;
    [SerializeField] Image naveLifeImage = default;
    [SerializeField] Image minionLifeImage = default;
    [SerializeField] TMP_Text minionLifetext = default;
    [SerializeField] GameObject minionIconLifeParent = default;
    [SerializeField] Image naveShieldLifeImage = default;

    private GameObject minionAtkImage;
    private GameObject minionShieldImage;
    private GameObject minionItemImage;
    [SerializeField] Image minionAtkLifeImage;

    private Image minionItemFillAmount;

    private MinionController minionController;

    private NaveController naveController;

    private List<GameObject> uiMinionsList = new List<GameObject>();

    private bool isOnComputer;

    private void Awake()
    {
        minionController = FindObjectOfType<MinionController>().GetComponent<MinionController>();

        naveController = FindObjectOfType<NaveController>().GetComponent<NaveController>();
    }

    private void Start()
    {
        //minionController = FindObjectOfType<MinionController>().GetComponent<MinionController>();
        minionAtkImage = minion1Ui;
        minionShieldImage = minion2Ui;

        minionItemImage = minion3Ui;
        minionItemFillAmount = minionItemImage.GetComponentInChildren<MinionItemUi>().GetFillAmount();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isOnComputer)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        //stunable.onStunStarted += StartStunBar;
        //stunable.onStunFinished += StopStun;
        minionController.onLifeChange += MinionLifeDamage;

        minionController.onResetMinionStatus += ResetMinionStatus;

        minionController.onChangeMinion += ChangeUiMinion;
        minionController.onChangeMinionAtkUi += ChangeMinionAtkUi;
        minionController.onChangeMinionShieldUi += ChangeMinionShieldUi;
        minionController.onChangeMinionItemUi += ChangeMinionItemUi;

        minionController.onAddMinionsAtkUi += AddMinionAtkUi;
        minionController.onClearList += ClearList;

        minionController.onChangeFillAmountMinionItem += FillAmountMinionItem;

        naveController.onLifeChange += NaveLifeDamage;
        naveController.onShieldChange += NaveShieldDamage;

        minionController.onMinionItemNoUsable += MinionItemNotUsable;
    }

    private void OnDisable()
    {
        //stunable.onStunStarted -= StartStunBar;
        //stunable.onStunFinished -= StopStun;
        minionController.onLifeChange -= MinionLifeDamage;

        minionController.onResetMinionStatus -= ResetMinionStatus;

        minionController.onChangeMinion -= ChangeUiMinion;
        minionController.onChangeMinionAtkUi -= ChangeMinionAtkUi;
        minionController.onChangeMinionShieldUi -= ChangeMinionShieldUi;
        minionController.onChangeMinionItemUi -= ChangeMinionItemUi;

        minionController.onAddMinionsAtkUi -= AddMinionAtkUi;
        minionController.onClearList -= ClearList;

        minionController.onChangeFillAmountMinionItem -= FillAmountMinionItem;

        naveController.onLifeChange -= NaveLifeDamage;
        naveController.onShieldChange -= NaveShieldDamage;

        minionController.onMinionItemNoUsable -= MinionItemNotUsable;
    }

    public void CheckIfIsOnComputer(bool value)
    {
        isOnComputer = value;
    }

    private void MinionItemNotUsable()
    {
        StartCoroutine(MinionItemRed());
    }

    private IEnumerator MinionItemRed()
    {
        Image item = minionItemImage.GetComponent<Image>();
        item.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        item.color = Color.white;
    }

    private void ChangeUiMinion(bool minionToChange)
    {
        minionAtkImage.SetActive(minionToChange);
        minionShieldImage.SetActive(!minionToChange);
    }

    private void ClearList()
    {
        for(int i = 0; i < uiMinionsList.Count; i++)
        {
            Destroy(uiMinionsList[i].gameObject);
        }

        uiMinionsList.Clear();
        uiMinionsList = new List<GameObject>();
    }

    private void ChangeMinionAtkUi()
    {
        //if(minionAtkImage.activeSelf)
        //{
        //    Destroy(minionAtkImage.gameObject);
        //    minionAtkImage = Instantiate(icon, minionIconParent.transform);
        //}
        //else
        //{
        //    Destroy(minionAtkImage.gameObject);
        //    minionAtkImage = Instantiate(icon, minionIconParent.transform);
        //    minionAtkImage.SetActive(false);
        //}

        Destroy(uiMinionsList[0].gameObject);
        uiMinionsList.Remove(uiMinionsList[0]);
    }

    private void AddMinionAtkUi(GameObject icon)
    {
        minionAtkImage = Instantiate(icon, minionIconParent.transform);
        uiMinionsList.Add(minionAtkImage);
    }

    private void ChangeMinionShieldUi(GameObject icon)
    {
        if (minionShieldImage.activeSelf)
        {
            Destroy(minionShieldImage.gameObject);
            minionShieldImage = Instantiate(icon, minionIconParent.transform);
        }
        else
        {
            Destroy(minionShieldImage.gameObject);
            minionShieldImage = Instantiate(icon, minionIconParent.transform);
            minionShieldImage.SetActive(false);
        }
    }

    private void ChangeMinionItemUi(GameObject icon)
    {
        Destroy(minionItemImage.gameObject);
        minionItemImage = Instantiate(icon, minionItemIconParent.transform);
        minionItemFillAmount = minionItemImage.GetComponentInChildren<MinionItemUi>().GetFillAmount();
    }

    private void FillAmountMinionItem(int amount, int minionReloadUlti)
    {
        float currentItemFillAmount = (float)amount / (float)minionReloadUlti;

        minionItemFillAmount.fillAmount = currentItemFillAmount;
    }

    private void ResetMinionStatus(Sprite icon, int life)
    {
        minionLifeImage.fillAmount = 1;
        minionAtkLifeImage.sprite = icon;

        minionLifetext.text = life + "/" + life;
    }

    private void MinionLifeDamage(int amount, int data)
    {
        float currentLife = (float)amount / (float)data;

        minionLifeImage.fillAmount = currentLife;
        minionLifetext.text = amount + "/" + data; 
    }

    private void NaveLifeDamage(int amount, int data)
    {
        float currentlife = (float)amount / (float)data;

        naveLifeImage.fillAmount = currentlife;
    }

    private void NaveShieldDamage(int amount, int data)
    {
        float currentlife = (float)amount / (float)data;

        naveShieldLifeImage.fillAmount = currentlife;
    }

    private void StartStunBar(float time)
    {
        StartCoroutine(StunBarCoroutine(time));
    }

    private IEnumerator StunBarCoroutine(float time)
    {
        stunBarImage.enabled = true;
        float currentTime = time;

        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            stunBarImage.fillAmount = 1 - (currentTime / time);
            yield return null;
        }

        stunBarImage.fillAmount = 1;
    }

    private void StopStun()
    {
        stunBarImage.enabled = false;
    }
}
