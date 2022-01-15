/// <copyright> (c) Out Of The Box 2021 </copyright>
/// <author> (c) Dominik Dammer </author>
/// <url> http://dominik-dammer.de/ </url>

using Cinemachine;
using UnityEngine;

    #region enum

    #endregion enum

public class CameraScript : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    public int test = 10;

    private void Update()
    {

        //UPDATE TO NEW INPUT
        if (Input.GetKeyDown(KeyCode.E))
        {
             var dolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
             dolly.m_PathPosition = 1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            var dolly = currentCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            dolly.m_PathPosition = 0;
        }
    }
}

