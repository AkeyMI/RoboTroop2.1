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
    [SerializeField] GameObject minionIconParent;

    private GameObject minionAtkImage;
    private GameObject minionShieldImage;
    private GameObject minionItemImage;

    private MinionController minionController;

    private void Awake()
    {
        minionController = FindObjectOfType<MinionController>().GetComponent<MinionController>();
    }

    private void Start()
    {
        //minionController = FindObjectOfType<MinionController>().GetComponent<MinionController>();
        minionAtkImage = minion1Ui;
        minionShieldImage = minion2Ui;
    }

    private void OnEnable()
    {
        //stunable.onStunStarted += StartStunBar;
        //stunable.onStunFinished += StopStun;
        minionController.onChangeMinion += ChangeUiMinion;
        minionController.onChangeMinionAtkUi += ChangeMinionAtkUi;
        minionController.onChangeMinionShieldUi += ChangeMinionShieldUi;
        minionController.onChangeMinionItemUi += ChangeMinionItemUi;
    }

    private void OnDisable()
    {
        //stunable.onStunStarted -= StartStunBar;
        //stunable.onStunFinished -= StopStun;
        minionController.onChangeMinion -= ChangeUiMinion;
        minionController.onChangeMinionAtkUi -= ChangeMinionAtkUi;
        minionController.onChangeMinionShieldUi -= ChangeMinionShieldUi;
        minionController.onChangeMinionItemUi -= ChangeMinionItemUi;
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
        minionItemImage = icon;
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
