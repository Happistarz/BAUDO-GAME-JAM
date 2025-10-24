using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("References")]
    public InputActionReference interactInputAction;
    public Camera mainCamera;
    public Text interactionText;

    [Header("Settings")]
    public float interactionDistance = 3f;
    public bool canOpenDoor;

    private string _interactionKey;
    
    private void Start()
    {
        interactInputAction.action.Enable();
        interactInputAction.action.started += OnInteractInput;
        _interactionKey = "[" + interactInputAction.action.GetBindingDisplayString(0) + "] ";
    }
    
    private void FixedUpdate()
    {
        if (GameManager.Instance.OnUI)
        {
            interactionText.gameObject.SetActive(false);
            return;
        }

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hit,
                interactionDistance))
        {
            if (!hit.collider.TryGetComponent<IInteractable>(out var interactable)) return;
            if (!interactable.IsInteractalbe()) return;
                    
            interactionText.gameObject.SetActive(true);
            interactionText.text = _interactionKey + interactable.InteractionText;
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }
    
    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.OnUI) return;

        if (!Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out var hit,
            interactionDistance)) return;

        if (!hit.collider.TryGetComponent<IInteractable>(out var interactable)) return;

        if (!canOpenDoor && interactable is Door) return;
        if (!interactable.IsInteractalbe()) return;
        
        interactable.Interact();
        interactionText.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        interactInputAction.action.Enable();
    }
    
    private void OnDisable()
    {
        interactInputAction.action.Disable();
    }
}