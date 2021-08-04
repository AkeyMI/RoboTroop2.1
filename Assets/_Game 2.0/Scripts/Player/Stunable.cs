using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunable : MonoBehaviour
{
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
        IsStunned = true;
        onStunStarted?.Invoke(time);

        yield return new WaitForSeconds(time);

        IsStunned = false;
        onStunFinished?.Invoke();
    }
}
