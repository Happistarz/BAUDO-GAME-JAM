using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory_Stocker : MonoBehaviour
{
    [Header("Show Inventory")]
    public InputActionReference showInventoryAction;
    public MonoBehaviour[] toDisable;
    public GameObject inventory;

    [Header("Manage Inventory")]
    public GameObject ItemObjectPrefab;
    public Inventory_Slot[] inventorySlots;

    void Start()
    {
        showInventoryAction.action.started += InventoryInput;
    }

    private void InventoryInput(InputAction.CallbackContext context)
    {
        inventory.SetActive(!inventory.activeSelf);
        foreach (MonoBehaviour toDisab in toDisable)
        {
            toDisab.enabled = !inventory.activeSelf;
        }

        if (inventory.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

    public void AddItemInInventory(Item newItem)
    {
        foreach (Inventory_Slot slot in inventorySlots)
        {
            if (!slot.HasItem())
            {
                AddItemInSlot(newItem, slot);
                return;
            }
        }
    }

    private void AddItemInSlot(Item item, Inventory_Slot slot)
    {
        GameObject newItemObj = Instantiate(ItemObjectPrefab);
        Inventory_Item newItem = newItemObj.GetComponent<Inventory_Item>();
        newItem.InitializeItem(item);
        newItem.SetNewSlot(slot);
    }

    private void OnEnable()
    {
        showInventoryAction.action.Enable();
    }

    private void OnDisable()
    {
        showInventoryAction.action.Disable();
    }
}
