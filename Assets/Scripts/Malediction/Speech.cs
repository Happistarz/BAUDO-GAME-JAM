using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(speechUI.GetComponentInChildren<InputField>().gameObject);
        speechUI.SetActive(true);
    }

    public void SubmitSpeech(string speechText)
    {
        _lastSpeech = speechText;
        GameManager.Instance.OnUI = false;
        _isTalking = false;

        MaledictionManager.Instance.MaledictionCheck();
        EventSystem.current.SetSelectedGameObject(null);
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