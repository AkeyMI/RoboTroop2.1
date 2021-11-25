using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] int walkPoints;
    [SerializeField] int shotPoints;
    
    public void WalkPoint()
    {
        walkPoints--;

        if (walkPoints <= 0)
            Next();

    }

    public void ShotPoint()
    {
        shotPoints--;

        if (shotPoints <= 0)
            Next();

    }

    void Next()
    {

    }
}
