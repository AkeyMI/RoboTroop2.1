using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    
    float shaketimer;
    void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    public void Shake (float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin camNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        camNoise.m_AmplitudeGain = intensity;
        camNoise.m_FrequencyGain = intensity;
        shaketimer = time;
    }

    private void FixedUpdate()
    {
        if(shaketimer > 0)
        {
            shaketimer -= Time.fixedDeltaTime;
            if(shaketimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin camNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                camNoise.m_AmplitudeGain = 0f;
                camNoise.m_FrequencyGain = 0f;
            }
        }
    }

}
