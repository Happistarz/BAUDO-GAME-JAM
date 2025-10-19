using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Speech : MonoBehaviour
{
    [Header("Reference")]
    public InputActionReference speechAction;
    public GameObject speechUI;
    public MonoBehaviour[] toDisable;

    private bool _isTalking = false;
    private MaledictionManager _maledictionManager;
    private string _lastSpeech ="";

    void Start()
    {
        speechAction.action.started += StartSpeech;
        _maledictionManager = FindAnyObjectByType<MaledictionManager>();
    }

    private void StartSpeech(InputAction.CallbackContext context)
    {
        if (!_isTalking)
        {
            foreach (MonoBehaviour toDisab in toDisable)
            {
                toDisab.enabled = false;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            speechUI.SetActive(true);
        }
    }

    public void SubmitSpeech(string speechText)
    {
        _lastSpeech = speechText;
        foreach (MonoBehaviour toDisab in toDisable)
        {
            toDisab.enabled = true;
        }
        _maledictionManager.MaledictionCheck();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        speechUI.SetActive(false);
    }

    public string GetSpeechText()
    {
        return _lastSpeech;
    }

    public void ResetSpeechText()
    {
        _lastSpeech = "";
    }
    
    private void OnEnable()
    {
        speechAction.action.Enable();
    }

    private void OnDisable()
    {
        speechAction.action.Disable();
    }
}
