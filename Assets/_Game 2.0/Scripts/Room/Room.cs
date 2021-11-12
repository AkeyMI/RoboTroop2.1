using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] bool traps;
    IRoomActivables[] roomActivables;

    private void Awake()
    {
        RefreshIRoomActivablesReferences();
    }

    public void ActivateRoom()
    {
        for (int i = 0; i < roomActivables.Length; i++)
        {
            roomActivables[i].Activate();
        }
        if (traps)
        {
            foreach (Trap t in GetComponentsInChildren<Trap>())         
                t.WakeUP();            
        }
        
    }

    private void RefreshIRoomActivablesReferences()
    {
        roomActivables = GetComponentsInChildren<IRoomActivables>();
    }

    public void EndWave()
    {
        for (int i = 0; i < roomActivables.Length; i++)
        {
            roomActivables[i].Deactivate();
        }
        if (traps)
        {
            foreach (Trap t in GetComponentsInChildren<Trap>())
            {
                if (t.GetComponent<TrapEF>() != null)
                    t.GetComponent<TrapEF>().Death();
            }
        }
        FindObjectOfType<CharacterController>().key = true;
    }
}
