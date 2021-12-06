using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    [SerializeField] MinionData[] minionDatas = default;
    [SerializeField] MinionItemData[] minionItemData = default;
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
        saveData = saveJson.Read_Jason();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SaveInformation();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            GetInformation();
            GetMinionsInBagData(FindObjectOfType<MinionController>());

        }
    }

    private void GetInformation()
    {
        saveData = saveJson.Read_Jason();
    }

    public SaveData GetPedestalData()
    {
        GetInformation();

        return saveData;
    }

    public void GetMinionsInBagData(MinionController mC)
    {
        if (saveData == null)
            GetInformation();

        if (saveData.minionsInBag.Length <= 0)
        {
            Debug.Log("No hay nada en la lista");
            return;
        }

        List<MinionData> data = new List<MinionData>();

        for(int i = 0; i < saveData.minionsInBag.Length; i++)
        {
            for(int j = 0; j < minionDatas.Length; j++)
            {
                if (saveData.minionsInBag[i] == minionDatas[j].minionName)
                {
                    data.Add(minionDatas[j]);
                }
            }
        }

        mC.SendMinionList(data);

    }

    public void GetMinionsInBoxData(MinionController mC)
    {
        if (saveData.minionsInBox.Length <= 0) return;

        List<MinionData> data = new List<MinionData>();

        for (int i = 0; i < saveData.minionsInBox.Length; i++)
        {
            for (int j = 0; j < minionDatas.Length; j++)
            {
                if (saveData.minionsInBox[i] == minionDatas[j].minionName)
                {
                    data.Add(minionDatas[j]);
                }
            }
        }

        mC.SendMinionBoxList(data);

    }

    public void GetMinionItem(MinionController mC)
    {
        if (saveData.minionItem == null) return;

        for(int i = 0; i < minionItemData.Length; i++)
        {
            if(saveData.minionItem == minionItemData[i].minionName)
            {
                mC.SendMinionItem(minionItemData[i]);
            }
        }
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

    public void SaveInformation()
    {
        SaveMinionsInBag();
        SaveMinionsInBox();
        SaveMinionItem();
        saveJson.ConvertToJson(saveData);
    }

    private void SaveMinionsInBag()
    {
        List<MinionData> listMinionInBag = FindObjectOfType<MinionController>().GetMinionDataList();
        saveData.minionsInBag = new string[listMinionInBag.Count];

        for(int i = 0; i < listMinionInBag.Count; i++)
        {
            saveData.minionsInBag[i] = listMinionInBag[i].minionName;
        }
    }

    private void SaveMinionsInBox()
    {
        List<MinionData> listMinionInBox = FindObjectOfType<MinionController>().GetMinionBoxDataList();
        saveData.minionsInBox = new string[listMinionInBox.Count];

        for (int i = 0; i < listMinionInBox.Count; i++)
        {
            saveData.minionsInBox[i] = listMinionInBox[i].minionName;
        }
    }

    private void SaveMinionItem()
    {
        saveData.minionItem = FindObjectOfType<MinionController>().GetMinionItemData().minionName;
    }
}
