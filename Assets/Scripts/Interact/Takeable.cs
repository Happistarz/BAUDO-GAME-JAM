using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Rendering;
using UnityEngine;

public class Takeable : MonoBehaviour, IInteractable
{
    public Item itemToTake;
    public EffectAfterTake afterTake;
    public string InteractionText => "Take";
    public int itemNumber = 1;

    public void Interact()
    {
        if (itemToTake != null)
        {
            if (afterTake == EffectAfterTake.DestroyGameObject)
            {
                InventoryManager.Instance.AddItemInInventory(itemToTake);
                Destroy(gameObject);
            }
            else if (afterTake == EffectAfterTake.Limited && itemNumber >= 0)
            {
                InventoryManager.Instance.AddItemInInventory(itemToTake);
                itemNumber--;
            }
        }
        else
        {
            Debug.LogWarning("No Item assigned to this Takeable.");
        }
    }

    
    public bool IsInteractalbe()
    {
        if (afterTake == EffectAfterTake.Limited)
            return itemNumber > 0;
        return true;
    }
}



public enum EffectAfterTake
{
    DestroyGameObject,
    Limited,
    Nothing
}
