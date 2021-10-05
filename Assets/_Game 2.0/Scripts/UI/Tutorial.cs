using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] KeyCode []  input;
    bool waiting = true;

    private void Update()
    {


        if(!waiting)
        {
            

        }
    }
    void InputPermision()
    {
        
    }

    IEnumerator Wait(int f)
    {
        waiting = true;
        InputPermision();
        yield return new WaitForSeconds(f);
        InputPermision();
        waiting = false;
    }

}
