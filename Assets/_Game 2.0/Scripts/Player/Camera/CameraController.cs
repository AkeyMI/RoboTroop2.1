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
        distnce = +i;

        if (distnce > 5)
            distnce = 5;

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
        if (camOffset.m_FollowOffset.y < 17 + distnce)
        {
            camOffset.m_FollowOffset += new Vector3(0, Time.fixedDeltaTime, -1 * Time.fixedDeltaTime);
        }

        if (camOffset.m_FollowOffset.y > 17 + distnce)
        {
            camOffset.m_FollowOffset -= new Vector3(0, Time.fixedDeltaTime, -1 * Time.fixedDeltaTime);
        }

    }

}
