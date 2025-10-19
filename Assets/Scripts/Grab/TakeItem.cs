using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour
{
    [Header("Reference")]
    public InputActionReference takeAction;
    public Camera playerCamera;
    public Inventory_Stocker inventory;
    public Text takeText;

    [Header("Value")]
    public float takeDistance;
    public string textWhenTakeable = "Take";

    void Start()
    {
        takeAction.action.started += StartGrabItem;
        textWhenTakeable = "[" + takeAction.action.GetBindingDisplayString(0) + "] " + textWhenTakeable;
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo;
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, takeDistance);
        if (hitInfo.collider != null && hitInfo.collider.TryGetComponent<Takeable>(out var takeable))
        {
            takeText.gameObject.SetActive(true);
            takeText.text = textWhenTakeable;
        }
        else
        {
            takeText.gameObject.SetActive(false);
        }
    }

    private void StartGrabItem(InputAction.CallbackContext context)
    {
        RaycastHit hitInfo;
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, takeDistance);
        if(hitInfo.collider.TryGetComponent<Takeable>(out var takeable))
        {
            Item toTake = takeable.Take();
            inventory.AddItemInInventory(toTake);
        }
    }
}
