using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera particleCamera;

    void Awake()
    {
        particleCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SwitchToParticleCamera(bool switchToParticle)
    {
        if (switchToParticle)
        {
            particleCamera.Priority = 15;
            // particleCamera.m_Priority ??
        }
        else
        {
            particleCamera.Priority = 5;
        }
    }

}