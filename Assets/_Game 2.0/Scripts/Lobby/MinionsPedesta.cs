using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsPedesta : MonoBehaviour
{
    [SerializeField] GameObject[] pedestals = default;

    private DataCollector dataCollector;
    private SaveData save;

    private void Start()
    {
        dataCollector = FindObjectOfType<DataCollector>();

        save = dataCollector.GetPedestalData();


    }

    private void Pedestals()
    {

    }

}
