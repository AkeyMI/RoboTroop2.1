using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip clip = default;
    [SerializeField] AudioSource sound = default;

    private void Start()
    {
        StartCoroutine(SoundPlay());
    }

    IEnumerator SoundPlay()
    {
        sound.PlayOneShot(clip);

        yield return new WaitForSeconds(clip.length);

        Destroy(this.gameObject);
    }
}
