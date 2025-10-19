using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public Item item;
    private RectTransform _rectTransform;
    private Transform _itemSlotAfterDrag;
    private Image _image;

    public void InitializeItem(Item newItem)
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _image = gameObject.GetComponent<Image>();
        item = newItem;
        _image.sprite = newItem.itemSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _itemSlotAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetParent(_itemSlotAfterDrag);
        _rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void SetDropSlot(Inventory_Slot newSlot)
    {
        _itemSlotAfterDrag = newSlot.transform;
        
    }

    public void SetNewSlot(Inventory_Slot newSlot)
    {
        transform.SetParent(newSlot.transform);
        _rectTransform.anchoredPosition = new Vector2(0, 0); 
    }
}
