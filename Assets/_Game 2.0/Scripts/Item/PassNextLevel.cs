using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/PassScene", order = 1)]
public class PassNextLevel : Effect
{
    [SerializeField] int sceneNumber = 1;

    public override void Apply()
    {
        SceneManager.LoadScene(sceneNumber);
        FindObjectOfType<DataCollector>().SaveInformation();
    }
}
