using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] bool canDebug;
    [SerializeField] CameraChannelSO cameraChannel;

    void Update()
    {
        if (canDebug)
        {
            CheckForDebugCommands();
        }
    }

    private void CheckForDebugCommands()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(cameraChannel.CompareRoomNameAction());
        }
    }
}
