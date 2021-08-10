using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Image naveShieldLifeImage = default;

    private GameObject minionAtkImage;
    private GameObject minionShieldImage;
    private GameObject minionItemImage;

    private Image minionItemFillAmount;

    private MinionController minionController;

    private NaveController naveController;

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
        if(Input.GetKeyDown(KeyCode.Escape))
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
        minionController.onChangeMinion += ChangeUiMinion;
        minionController.onChangeMinionAtkUi += ChangeMinionAtkUi;
        minionController.onChangeMinionShieldUi += ChangeMinionShieldUi;
        minionController.onChangeMinionItemUi += ChangeMinionItemUi;

        minionController.onChangeFillAmountMinionItem += FillAmountMinionItem;

        naveController.onLifeChange += NaveLifeDamage;
        naveController.onShieldChange += NaveShieldDamage;
    }

    private void OnDisable()
    {
        //stunable.onStunStarted -= StartStunBar;
        //stunable.onStunFinished -= StopStun;
        minionController.onChangeMinion -= ChangeUiMinion;
        minionController.onChangeMinionAtkUi -= ChangeMinionAtkUi;
        minionController.onChangeMinionShieldUi -= ChangeMinionShieldUi;
        minionController.onChangeMinionItemUi -= ChangeMinionItemUi;

        minionController.onChangeFillAmountMinionItem -= FillAmountMinionItem;

        naveController.onLifeChange -= NaveLifeDamage;
        naveController.onShieldChange -= NaveShieldDamage;
    }

    private void ChangeUiMinion(bool minionToChange)
    {
        minionAtkImage.SetActive(minionToChange);
        minionShieldImage.SetActive(!minionToChange);
    }

    private void ChangeMinionAtkUi(GameObject icon)
    {
        if(minionAtkImage.activeSelf)
        {
            Destroy(minionAtkImage.gameObject);
            minionAtkImage = Instantiate(icon, minionIconParent.transform);
        }
        else
        {
            Destroy(minionAtkImage.gameObject);
            minionAtkImage = Instantiate(icon, minionIconParent.transform);
            minionAtkImage.SetActive(false);
        }
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

    private void FillAmountMinionItem(int amount)
    {
        float currentItemFillAmount = (float)amount / 3f;

        minionItemFillAmount.fillAmount = currentItemFillAmount;
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
