using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int budget;


    public void ManageBudget(int i)
    {
        budget += i;
    }
}
