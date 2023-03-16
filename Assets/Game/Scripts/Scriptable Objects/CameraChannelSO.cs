using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

[CreateAssetMenu(fileName = "CameraChannel", menuName = "Event Channels/Camera Channel")]
public class CameraChannelSO : ScriptableObject
{
    public delegate void InitSceneCamerasCallback();
    public InitSceneCamerasCallback OnInitSceneCameras;

    public delegate void InstantiateRoomCameraCallback(GameObject roomCamera, string roomName);
    public InstantiateRoomCameraCallback OnInstantiateRoomCamera;

    public delegate void NextCameraCallback();
    public NextCameraCallback OnNextCamera;

    public delegate void PreviousCameraCallback();
    public PreviousCameraCallback OnPreviousCamera;

    public delegate void GetRoomNameCallback();
    public GetRoomNameCallback OnGetRoomName;

    public delegate void UpdateRoomNameCallback(string roomName);
    public UpdateRoomNameCallback OnUpdateRoomName;

    public void InitSceneCamerasAction()
    {
        OnInitSceneCameras?.Invoke();
    }

    public void InstantiateRoomCameraAction(GameObject roomCamera, string roomName)
    {
        OnInstantiateRoomCamera?.Invoke(roomCamera, roomName);
    }

    public void NextCameraAction()
    {
        OnNextCamera?.Invoke();
    }

    public void PreviousCameraAction()
    {
        OnPreviousCamera?.Invoke();
    }

    public void GetRoomNameAction()
    {
        OnGetRoomName?.Invoke();
    }

    public void UpdateRoomNameAction(string roomName)
    {
        OnUpdateRoomName?.Invoke(roomName);
    }
}
