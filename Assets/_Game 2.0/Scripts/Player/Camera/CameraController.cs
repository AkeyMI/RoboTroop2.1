using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    float distnce;
    
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
    public void Offset(float i)
    {
        distnce += i;

        if (distnce > 4)
            distnce = 4;

        if (distnce < 0)
            distnce = 0;
    }

    private void FixedUpdate()
    {
        CinemachineTransposer camOffset = cam.GetCinemachineComponent<CinemachineTransposer>();

        if (shaketimer > 0)
        {
            shaketimer -= Time.fixedDeltaTime;
            if(shaketimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin camNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                camNoise.m_AmplitudeGain = 0f;
                camNoise.m_FrequencyGain = 0f;
            }
        }
        if (camOffset.m_FollowOffset.y < 18 + distnce)
        {
            camOffset.m_FollowOffset += new Vector3(0, Time.fixedDeltaTime * 2,Time.fixedDeltaTime * -2f);
        }

        if (camOffset.m_FollowOffset.y > 18 + distnce)
        {
            camOffset.m_FollowOffset -= new Vector3(0, Time.fixedDeltaTime * 2, Time.fixedDeltaTime * -2f);
        }

    }

}
