using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] KeyCode []  input;
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    bool waiting;
    PlayerData pData;

    private void Awake()
    {
        pData = FindObjectOfType<PlayerData>();
    }
    private void Start()
    {
        StartCoroutine(Wait(2, "Move"));
    }
    private void Update()
    {


        if(!waiting)
        {
            

        }
    }
    void InputPermision()
    {
        if (image.color == Color.red)
            image.color = Color.green;
        else
            image.color = Color.red;
    }

    IEnumerator Wait(int f, string st)
    {
        waiting = true;
        text.text = st;
        InputPermision();
        yield return new WaitForSeconds(f);
        InputPermision();
        waiting = false;
    }

}
