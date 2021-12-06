using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMinions : MonoBehaviour
{
    [SerializeField] float distanceToActivate = 2f;
    [SerializeField] Image eImage = default;
    [SerializeField] GameObject boxMinions = default;
    [SerializeField] GameObject[] desativateThings = default;

    private bool isOpen =  false;

    private bool isClose;

    private void Update()
    {
        CheckIfThereIsPlayer();

        if (Input.GetKeyDown(KeyCode.E) && isClose && !isOpen)
        {
            ActivateComputer();
            isOpen = true;
        }
    }

    private void CheckIfThereIsPlayer()
    {
        bool isPlayerClose = false;
        Collider[] players = Physics.OverlapSphere(this.transform.position, distanceToActivate);
        foreach (var player in players)
        {
            if (player.CompareTag("AtkMinion"))
            {
                isPlayerClose = true;

            }
        }

        if(isPlayerClose)
        {
            isClose = true;
            Debug.Log("Esta cerca");
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

    public void DesactivateBoxMinions()
    {
        boxMinions.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<UiManager>().CheckIfIsOnComputer(false);
        isOpen = false;

        for (int i = 0; i < desativateThings.Length; i++)
        {
            desativateThings[i].SetActive(true);
        }
    }

    public void ActivateComputer()
    {
        for(int i = 0; i < desativateThings.Length; i++)
        {
            desativateThings[i].SetActive(false);
        }

        FindObjectOfType<UiManager>().CheckIfIsOnComputer(true);
        boxMinions.SetActive(true);
        Time.timeScale = 0f;
        MinionController minionController = FindObjectOfType<MinionController>();
        BoxMInion boxMInion = FindObjectOfType<BoxMInion>();
        boxMInion.CreateBoxElements(minionController.GetMinionBoxDataList());
        boxMInion.CreateInElements(minionController.GetMinionDataList());
    }
}
