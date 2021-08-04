using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
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
    }

    private void RefreshIRoomActivablesReferences()
    {
        roomActivables = GetComponentsInChildren<IRoomActivables>();
    }
}
