using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera vcam1;
    [SerializeField] public CinemachineVirtualCamera vcam2;
    [SerializeField] public bool _switchNow;

    public float time = 1;

    void Update()
    {
        if(_switchNow)
        {
            var brain = GetComponent<CinemachineBrain>();
            brain.m_DefaultBlend.m_Time = time;

            if (brain.IsLive(vcam1))
            {
                vcam2.gameObject.SetActive(true);
                vcam1.gameObject.SetActive(false);
            }
            else
            {
                vcam2.gameObject.SetActive(false);
                vcam1.gameObject.SetActive(true);
            }
            _switchNow = false;
        }
    }
}
