using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "Room Settings", menuName = "Room Settings")]
public class RoomSettingsSO : ScriptableObject
{
    [Header("Room Identifier")]
    [SerializeField] public string roomName;

    [Header("Room Camera")]
    [SerializeField] public GameObject roomCamera;

    [Header("List of Anomalies")]
    [SerializeField] public List<AnomalySO> anomalies = new List<AnomalySO>();

}
