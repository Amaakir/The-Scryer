using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    [Header("UI Manager Settings")]
    [SerializeField] TextMeshProUGUI roomNameText;
    [SerializeField] float reportTime;
    [SerializeField] GameObject gameplayUIParent;

    [Header("Screen Messages")]
    [SerializeField] GameObject camSwitchScreen;
    [SerializeField] float camSwitchDuration;
    [SerializeField] GameObject fixAnomalyScreen;
    [SerializeReference] float fixAnomalyDuration;
    [SerializeField] TextMeshProUGUI errorMessageText;
    [SerializeField] string reportPendingMessage;

    [Header("SFX Audio Clips")]
    [SerializeField] AudioClipDataSO staticShort;
    [SerializeField] AudioClipDataSO staticLong;
    [SerializeField] AudioClipDataSO ghostWhisper;

    [Header("Report Buttons")]
    [SerializeField] Button pictureButton;
    [SerializeField] Button bloodButton;
    [SerializeField] Button openCloseButton;
    [SerializeField] Button intruderButton;
    [SerializeField] Button cameraButton;
    [SerializeField] Button objDisappearanceButton;
    [SerializeField] Button extraObjButton;
    [SerializeField] Button objMovementButton;

    [Header("Button Text")]
    [SerializeField] GameObject pictureText;
    [SerializeField] GameObject reportPictureText;
    [SerializeField] GameObject bloodText;
    [SerializeField] GameObject reportBloodText;
    [SerializeField] GameObject openCloseText;
    [SerializeField] GameObject reportOpenCloseText;
    [SerializeField] GameObject intruderText;
    [SerializeField] GameObject reportIntruderText;
    [SerializeField] GameObject cameraText;
    [SerializeField] GameObject reportCameraText;
    [SerializeField] GameObject objDisappearanceText;
    [SerializeField] GameObject reportObjDisappearanceText;
    [SerializeField] GameObject extraObjText;
    [SerializeField] GameObject reportExtraObjText;
    [SerializeField] GameObject objMovementText;
    [SerializeField] GameObject reportObjMovementText;

    [Header("Event Channels")]
    [SerializeField] GameStateChannelSO gameStateChannel;
    [SerializeField] CameraChannelSO cameraChannel;
    [SerializeField] AnomalyChannelSO anomalyChannel;
    [SerializeField] SoundChannelSO soundChannel;
    [SerializeField] UIChannelSO uiChannel;
    [SerializeField] AnomalySO anomalyNames;

    private bool isReporting = false;
    private bool isReportMessage = false;
    private bool canInteract = true;
    private string activeRoomName;
    private Coroutine reportAnomalyCoroutine;
    private Coroutine camSwitchCoroutine;
    private Coroutine fixAnomalyCoroutine;

    private void OnEnable()
    {
        cameraChannel.OnUpdateRoomName += UpdateRoomName;
        cameraChannel.OnNextCamera += StartCamSwitchCoroutine;
        cameraChannel.OnPreviousCamera += StartCamSwitchCoroutine;
        uiChannel.OnDisplayErrorMessage += DisplayErrorMessage;
        uiChannel.OnPlayFixAnomalyScreen += StartFixAnomalyCoroutine;
        uiChannel.OnStartGameIntro += PlayIntro;
        gameStateChannel.OnTriggerPlayerInteraction += TriggerInteraction;
        gameStateChannel.OnWinGame += DisableGameplayUI;
    }

    private void OnDisable()
    {
        cameraChannel.OnUpdateRoomName -= UpdateRoomName;
        cameraChannel.OnNextCamera -= StartCamSwitchCoroutine;
        cameraChannel.OnPreviousCamera -= StartCamSwitchCoroutine;
        uiChannel.OnDisplayErrorMessage -= DisplayErrorMessage;
        uiChannel.OnPlayFixAnomalyScreen -= StartFixAnomalyCoroutine;
        uiChannel.OnStartGameIntro -= PlayIntro;
        gameStateChannel.OnTriggerPlayerInteraction -= TriggerInteraction;
        gameStateChannel.OnWinGame -= DisableGameplayUI;
    }

    private void Start()
    {
        InitUIManager();
    }

    private void InitUIManager()
    {
        cameraChannel.GetRoomNameAction();
        isReporting = false;
        isReportMessage = false;
        canInteract = true;
    }

    private void UpdateRoomName(string roomName)
    {
        roomNameText.text = roomName;
        activeRoomName = roomName;
    }

    private void TriggerInteraction(bool value)
    {
        canInteract = value;
    }

    #region Button Events 
    public void OnClickNextCamera()
    {
        if (canInteract)
        {
            cameraChannel.NextCameraAction();
        }
        
    }

    public void OnClickPreviousCamera()
    {
        if (canInteract)
        {
            cameraChannel.PreviousCameraAction();
        }
        
    }

    public void OnClickPicture()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.pictureAnomalyName, activeRoomName, pictureButton, pictureText, reportPictureText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickBlood()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.bloodAnomalyName, activeRoomName, bloodButton, bloodText, reportBloodText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickOpeningClosing()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.openCloseAnomalyName, activeRoomName, openCloseButton, openCloseText, reportOpenCloseText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickIntruder()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.intruderAnomalyName, activeRoomName, intruderButton, intruderText, reportIntruderText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickCameraMalfunction()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.cameraAnomalyName, activeRoomName, cameraButton, cameraText, reportCameraText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickObjectDisappearance()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.objDisappearanceAnomalyName, activeRoomName, objDisappearanceButton, objDisappearanceText, reportObjDisappearanceText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickExtraObject()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.extraObjAnomalyName, activeRoomName, extraObjButton, extraObjText, reportExtraObjText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    public void OnClickObjectMovement()
    {
        if (canInteract)
        {
            if (!isReporting)
            {
                reportAnomalyCoroutine = StartCoroutine(
                    ReportAnomaly(anomalyNames.objMovementAnomalyName, activeRoomName, objMovementButton, objMovementText, reportObjMovementText));
            }
            else
            {
                CheckIfReportMessage(reportPendingMessage);
            }
        }
        
    }

    #endregion

    private IEnumerator ReportAnomaly(string anomalyGuess, string roomGuess, Button reportButton, GameObject regularText, GameObject reportingText)
    {
        isReporting = true;
        regularText.SetActive(false);
        reportingText.SetActive(true);
        TriggerButton(reportButton, false);
        yield return new WaitForSeconds(reportTime);
        anomalyChannel.SendAnomalyGuessAction(anomalyGuess, roomGuess);
        isReporting = false;
        regularText.SetActive(true);
        reportingText.SetActive(false);
        TriggerButton(reportButton, true);
        ResetCoroutine(reportAnomalyCoroutine);
    }

    private void ResetCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }

    private void TriggerButton(Button reportButton, bool value)
    {
        reportButton.interactable = value;
    }

    private void StartCamSwitchCoroutine()
    {
        camSwitchCoroutine = StartCoroutine(PlayCamSwitch());
    }

    private IEnumerator PlayCamSwitch()
    {
        canInteract = false;
        camSwitchScreen.SetActive(true);
        soundChannel.PlayAudioAction(staticShort);
        yield return new WaitForSeconds(camSwitchDuration);
        canInteract = true;
        camSwitchScreen.SetActive(false);
        ResetCoroutine(camSwitchCoroutine);
    }

    private void StartFixAnomalyCoroutine()
    {
        fixAnomalyCoroutine = StartCoroutine(PlayFixAnomaly());
    }

    private IEnumerator PlayFixAnomaly()
    {
        gameStateChannel.OnTriggerPlayerInteraction(false);
        fixAnomalyScreen.SetActive(true);
        soundChannel.PlayAudioAction(staticLong);
        yield return new WaitForSeconds(fixAnomalyDuration);
        gameStateChannel.OnTriggerPlayerInteraction(true);
        fixAnomalyScreen.SetActive(false);
        ResetCoroutine(fixAnomalyCoroutine);
    }

    private void CheckIfReportMessage(string message)
    {
        if (!isReportMessage)
        {
            isReportMessage = true;
            DisplayErrorMessage(message);
        }
    }

    private void DisplayErrorMessage(string message)
    {
        errorMessageText.text = message;
        uiChannel.PlayUIDirectorAction();
    }

    private void PlayIntro()
    {
        soundChannel.PlayAudioAction(ghostWhisper);
        uiChannel.PlayIntroDirector();
    }

    public void ResetTimeline()
    {
        isReportMessage = false;
        uiChannel.ResetUITimelineAction();
    }

    public void ResetIntroTimeline()
    {
        anomalyChannel.StartSpawnTimerAction();
        uiChannel.ResetIntroTimelineAction();
    }

    private void DisableGameplayUI()
    {
        gameplayUIParent.SetActive(false);
    }


}
