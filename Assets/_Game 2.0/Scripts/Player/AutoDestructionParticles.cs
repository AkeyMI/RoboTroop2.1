using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructionParticles : MonoBehaviour
{
    float currentTime;
   private void FixedUpdate()
    {
        if(currentTime <= 6)
        {
            currentTime += Time.fixedDeltaTime * 5;
            transform.localScale = new Vector3( currentTime, 0, currentTime);
        }
    }
}
