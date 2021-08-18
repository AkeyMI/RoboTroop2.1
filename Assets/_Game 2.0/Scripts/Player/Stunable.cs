using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stunable : MonoBehaviour
{
    [SerializeField] Image stunBarImage = default;

    public bool IsStunned { get; private set; }

    public event Action<float> onStunStarted;
    public event Action onStunFinished;

    public void Stun(float time)
    {
        if (IsStunned) return;

        StartCoroutine(StunTimeCoroutine(time));
    }

    private IEnumerator StunTimeCoroutine(float time)
    {
        float currentTime = time;
        stunBarImage.enabled = true;

        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            stunBarImage.fillAmount = (currentTime / time);
            yield return null;
        }

        stunBarImage.enabled = false;
        stunBarImage.fillAmount = 1;

        //IsStunned = true;
        //onStunStarted?.Invoke(time);

        //yield return new WaitForSeconds(time);

        //IsStunned = false;
        //onStunFinished?.Invoke();
    }
}
