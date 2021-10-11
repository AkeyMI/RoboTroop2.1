using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElement : MonoBehaviour
{
    GameObject objToPool;
    [SerializeField] float timeToRecicle;

    private void Awake()
    {
        objToPool = this.gameObject;
    }

    public GameObject Obj()
    {
        return objToPool;
    }
    public float f()
    {
        return timeToRecicle;
    }
}
