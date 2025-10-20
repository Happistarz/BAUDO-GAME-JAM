using UnityEngine;
using UnityEngine.InputSystem;

public class Speech : MonoBehaviour
{
    [Header("Reference")] public InputActionReference speechAction;
    public GameObject speechUI;

    private bool _isTalking;
    private string _lastSpeech = "";

    void Start()
    {
        speechAction.action.started += StartSpeech;
    }

    private void StartSpeech(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.OnUI) return;
        if (_isTalking) return;
        
        _isTalking = true;
        GameManager.Instance.OnUI = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        speechUI.SetActive(true);
    }

    public void SubmitSpeech(string speechText)
    {
        _lastSpeech = speechText;
        GameManager.Instance.OnUI = false;
        _isTalking = false;

        MaledictionManager.Instance.MaledictionCheck();
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