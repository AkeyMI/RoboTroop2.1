using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Effect_Name", menuName = "RoboTroop/Effects/PassScene", order = 1)]
public class PassNextLevel : Effect
{
    [SerializeField] string sceneName;

    public override void Apply()
    {
        SceneManager.LoadScene(sceneName);
        FindObjectOfType<DataCollector>().SaveInformation();
    }
}
