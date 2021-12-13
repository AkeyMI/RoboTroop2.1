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

    [SerializeField] GameObject portalLevel2;
    [SerializeField] GameObject portalLevel3;
    [SerializeField] GameObject portalLevel4;
    [SerializeField] GameObject portalLevel5;

    private DataCollector dataCollector;
    private SaveData save;

    private void Start()
    {
        dataCollector = FindObjectOfType<DataCollector>();

        save = dataCollector.GetPedestalData();

        Pedestals();
        Portals();
    }

    private void Portals()
    {
        if (save.level2)
        {
            portalLevel2.SetActive(true);
        }
        else
        {
            portalLevel2.SetActive(false);
        }

        if (save.level3)
        {
            portalLevel3.SetActive(true);
        }
        else
        {
            portalLevel3.SetActive(false);
        }

        if (save.level4)
        {
            portalLevel4.SetActive(true);
        }
        else
        {
            portalLevel4.SetActive(false);
        }

        if (save.level5)
        {
            portalLevel5.SetActive(true);
        }
        else
        {
            portalLevel5.SetActive(false);
        }
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
