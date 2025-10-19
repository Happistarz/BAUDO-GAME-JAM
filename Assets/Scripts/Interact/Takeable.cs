using UnityEngine;

public class Takeable : MonoBehaviour, IInteractable
{
    public Item itemToTake;
    public EffectAfterTake afterTake;
    public string InteractionText => "Take";

    public void Interact()
    {
        if (itemToTake != null)
        {
            InventoryManager.Instance.AddItemInInventory(itemToTake);
            if (afterTake == EffectAfterTake.DestroyGameObject)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("No Item assigned to this Takeable.");
        }
    }
}

public enum EffectAfterTake
{
    DestroyGameObject,
    Nothing
}
