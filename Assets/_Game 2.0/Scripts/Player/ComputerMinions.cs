using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMinions : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ActivateComputer();
        }
    }

    public void ActivateComputer()
    {
        MinionController minionController = FindObjectOfType<MinionController>();
        BoxMInion boxMInion = FindObjectOfType<BoxMInion>();
        boxMInion.CreateBoxElements(minionController.GetMinionBoxDataList());
        boxMInion.CreateInElements(minionController.GetMinionDataList());
    }
}
