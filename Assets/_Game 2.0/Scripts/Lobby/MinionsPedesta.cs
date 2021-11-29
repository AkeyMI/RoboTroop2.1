using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsPedesta : MonoBehaviour
{
    [SerializeField] GameObject canyonGuy;
    [SerializeField] GameObject havyMachineGuy;
    [SerializeField] GameObject machineGuy;
    [SerializeField] GameObject semigunGuy;
    [SerializeField] GameObject shotGuy;
    [SerializeField] GameObject triplegunGuy;

    private DataCollector dataCollector;
    private SaveData save;

    private void Start()
    {
        dataCollector = FindObjectOfType<DataCollector>();

        save = dataCollector.GetPedestalData();

        Pedestals();
    }

    private void Pedestals()
    {
        if (save.canyonGuy)
        {
            canyonGuy.SetActive(true);
        }
        else
        {
            canyonGuy.SetActive(false);
        }

        if (save.havyMachineGuy)
        {
            havyMachineGuy.SetActive(true);
        }
        else
        {
            havyMachineGuy.SetActive(false);
        }

        if (save.machineGuy)
        {
            machineGuy.SetActive(true);
        }
        else
        {
            machineGuy.SetActive(false);
        }

        if (save.semigunGuy)
        {
            semigunGuy.SetActive(true);
        }
        else
        {
            semigunGuy.SetActive(false);
        }

        if (save.shotGuy)
        {
            shotGuy.SetActive(true);
        }
        else
        {
            shotGuy.SetActive(false);
        }

        if (save.triplegunGuy)
        {
            triplegunGuy.SetActive(true);
        }
        else
        {
            triplegunGuy.SetActive(false);
        }
    }

}
