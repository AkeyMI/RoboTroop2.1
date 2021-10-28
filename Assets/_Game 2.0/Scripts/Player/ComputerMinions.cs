using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMinions : MonoBehaviour
{
    [SerializeField] float distanceToActivate = 2f;
    [SerializeField] Image eImage = default;
    [SerializeField] GameObject boxMinions = default;

    private bool isClose;

    private void Update()
    {
        CheckIfThereIsPlayer();

        if (Input.GetKeyDown(KeyCode.E) && isClose)
        {
            ActivateComputer();
        }
    }

    private void CheckIfThereIsPlayer()
    {
        Collider[] players = Physics.OverlapSphere(this.transform.position, distanceToActivate);
        foreach (var player in players)
        {
            if (player.CompareTag("Player") || player.CompareTag("AtkMinion"))
            {
                isClose = true;
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
    }

    public void DesactivateBoxMinions()
    {
        boxMinions.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<UiManager>().CheckIfIsOnComputer(false);
    }

    public void ActivateComputer()
    {
        FindObjectOfType<UiManager>().CheckIfIsOnComputer(true);
        boxMinions.SetActive(true);
        Time.timeScale = 0f;
        MinionController minionController = FindObjectOfType<MinionController>();
        BoxMInion boxMInion = FindObjectOfType<BoxMInion>();
        boxMInion.CreateBoxElements(minionController.GetMinionBoxDataList());
        boxMInion.CreateInElements(minionController.GetMinionDataList());
    }
}
