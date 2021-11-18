using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    [SerializeField] MinionData[] minionDatas = default;
    [SerializeField] SaveJson saveJson = default;

    private string canyonGuy = "Cañon";
    private string havyMachineGuy = "HavyMachineGuy";
    private string machineGuy = "MachineGuy";
    private string semigunGuy = "SemigunGuy";
    private string shotGuy = "ShotGuy";
    private string triplegunGuy = "TriplegunGuy";

    private SaveData saveData;

    private void Start()
    {
        saveData = new SaveData();
    }

    public void GetPedestalData()
    {

    }

    public void GetMinionsInBagData()
    {

    }

    public void GetMinionsInBoxData()
    {

    }

    public void SavePedestal(string minionName)
    {
        if(minionName == canyonGuy)
        {
            saveData.canyonGuy = true;
        }
        else if(minionName == havyMachineGuy)
        {
            saveData.havyMachineGuy = true;
        }
        else if (minionName == machineGuy)
        {
            saveData.machineGuy = true;
        }
        else if (minionName == semigunGuy)
        {
            saveData.semigunGuy = true;
        }
        else if (minionName == shotGuy)
        {
            saveData.shotGuy = true;
        }
        else if (minionName == triplegunGuy)
        {
            saveData.triplegunGuy = true;
        }
    }

    public void SaveMinionsInBag()
    {
        List<MinionData> listMinionInBag = FindObjectOfType<MinionController>().GetMinionDataList();
        saveData.minionsInBag = new string[listMinionInBag.Count];

        for(int i = 0; i < listMinionInBag.Count; i++)
        {
            saveData.minionsInBag[i] = listMinionInBag[i].minionName;
        }
    }

    public void SaveMinionsInBox()
    {
        List<MinionData> listMinionInBox = FindObjectOfType<MinionController>().GetMinionBoxDataList();
        saveData.minionsInBox = new string[listMinionInBox.Count];

        for (int i = 0; i < listMinionInBox.Count; i++)
        {
            saveData.minionsInBox[i] = listMinionInBox[i].minionName;
        }
    }
}
