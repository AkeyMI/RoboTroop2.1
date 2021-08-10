using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinImprovisado : MonoBehaviour
{
    [SerializeField] GameObject cosa = default;

    private void Update()
    {
        if(cosa = null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
