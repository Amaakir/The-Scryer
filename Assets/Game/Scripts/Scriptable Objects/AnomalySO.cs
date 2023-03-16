using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnomalySO", menuName = "Anomalies")]
public class AnomalySO : ScriptableObject
{
    [Header("Anomaly Names")]
    [SerializeField] public string pictureAnomalyName;
    [SerializeField] public string bloodAnomalyName;
    [SerializeField] public string openCloseAnomalyName;
    [SerializeField] public string intruderAnomalyName;
    [SerializeField] public string cameraAnomalyName;
    [SerializeField] public string objDisappearanceAnomalyName;
    [SerializeField] public string extraObjAnomalyName;
    [SerializeField] public string objMovementAnomalyName;

}
