using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject jumpscareCamera;

    [Header("Event Channels")]
    [SerializeField] CameraChannelSO cameraChannel;

    CameraController cameraController;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void OnEnable()
    {
        cameraChannel.OnInstantiateRoomCamera += InstantiateRoomCamera;
    }

    private void OnDisable()
    {
        cameraChannel.OnInstantiateRoomCamera -= InstantiateRoomCamera;
    }

    private void InstantiateRoomCamera(GameObject roomCamera, string roomName)
    {
        GameObject cameraInstance = Instantiate(roomCamera, transform);
        cameraInstance.transform.name = roomName;
        cameraController.AddVirtualCamera(cameraInstance.GetComponent <CinemachineVirtualCamera>());
    }
}
