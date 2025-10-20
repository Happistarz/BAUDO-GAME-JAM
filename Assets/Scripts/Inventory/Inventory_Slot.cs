using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class Inventory_Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Inventory_Item item = eventData.pointerDrag.GetComponent<Inventory_Item>();

        if (transform.childCount == 0 && item != null)
        {
            item.SetDropSlot(this);
        }
    }
    
    public bool HasItem()
    {
        return transform.childCount != 0;
    }
}
