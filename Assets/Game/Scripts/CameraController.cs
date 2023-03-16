using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Active Cameras")]
    [SerializeField] List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();

    [Header("Event Channels")]
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] GameStateChannelSO gameStateChannel;

    int cameraIndex = 0;
    bool canInteract = false;

    private void OnEnable()
    {
        cameraChannel.OnInitSceneCameras += InitVirtualCameras;
        cameraChannel.OnNextCamera += NextCamera;
        cameraChannel.OnPreviousCamera += PreviousCamera;
        cameraChannel.OnGetRoomName += GetRoomNameEvent;
        gameStateChannel.OnTriggerPlayerInteraction += TriggerInteraction;
    }

    private void OnDisable()
    {
        cameraChannel.OnInitSceneCameras -= InitVirtualCameras;
        cameraChannel.OnNextCamera -= NextCamera;
        cameraChannel.OnPreviousCamera -= PreviousCamera;
        cameraChannel.OnGetRoomName -= GetRoomNameEvent;
        gameStateChannel.OnTriggerPlayerInteraction -= TriggerInteraction;
    }

    private void InitVirtualCameras()
    {
        virtualCameras[cameraIndex].Priority++;
        canInteract = true;
        cameraChannel.UpdateRoomNameAction(GetActiveRoomName());
    }

    public void AddVirtualCamera(CinemachineVirtualCamera vCam)
    {
        virtualCameras.Add(vCam);
        vCam.Priority = 1;
    }

    private void TriggerInteraction(bool value)
    {
        canInteract = value;
    }

    private void Update()
    {
        if (canInteract)
        {
            CheckPlayerInput();
        }        
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            cameraChannel.NextCameraAction();

        }else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            cameraChannel.PreviousCameraAction();
        }
    }

    public void NextCamera()
    {
        if(cameraIndex == virtualCameras.Count-1)
        {
            virtualCameras[0].Priority++;
            virtualCameras[cameraIndex].Priority--;
            cameraIndex = 0;
        }
        else {
            virtualCameras[cameraIndex + 1].Priority++;
            cameraIndex++;
            virtualCameras[cameraIndex - 1].Priority--;

        }
        GetRoomNameEvent();

    }

    public void PreviousCamera()
    {
        if (cameraIndex == 0)
        {
            virtualCameras[virtualCameras.Count-1].Priority++;
            virtualCameras[0].Priority--;
            cameraIndex = virtualCameras.Count-1;
        }
        else
        {
            virtualCameras[cameraIndex - 1].Priority++;
            cameraIndex--;
            virtualCameras[cameraIndex + 1].Priority--;

        }
        GetRoomNameEvent();
    }

    private void GetRoomNameEvent()
    {
        cameraChannel.UpdateRoomNameAction(GetActiveRoomName());
    }

    private string GetActiveRoomName()
    {
        return virtualCameras[cameraIndex].transform.name;
    }
}
