using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Speech : MonoBehaviour
{
    [Header("Reference")]
    public InputActionReference speechAction;
    public GameObject speechUI;

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
        if (!GameManager.Instance.OnUI && !_isTalking)
        {
            GameManager.Instance.OnUI = true;
            GameManager.Instance.ShowCursor();
            speechUI.SetActive(true);
        }
    }

    public void SubmitSpeech(string speechText)
    {
        _lastSpeech = speechText;
        _maledictionManager.MaledictionCheck();
        GameManager.Instance.HideCursor();
        speechUI.SetActive(false);
        GameManager.Instance.OnUI = false;
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
