using JetBrains.Annotations;
using UnityEngine;

public class Inventory_Body : MonoBehaviour
{
    [Header("BodyPart")]
    [SerializeField] Inventory_Slot headSlot;
    [SerializeField] Inventory_Slot hand_r;
    [SerializeField] Inventory_Slot hand_l;
    [SerializeField] Inventory_Slot foot_r;
    [SerializeField] Inventory_Slot foot_l;

    public Item GetItemInHead()
    {
        if (headSlot.transform.childCount == 0)
            return null;
        return headSlot.transform.GetChild(0).GetComponent<Inventory_Item>().item;
    }

    public Item GetItemInRightHand()
    {
        if (hand_r.transform.childCount == 0)
            return null;
        return hand_r.transform.GetChild(0).GetComponent<Inventory_Item>().item;
    }

    public Item GetItemInLeftHand()
    {
        if (hand_l.transform.childCount == 0)
            return null;
        return hand_l.transform.GetChild(0).GetComponent<Inventory_Item>().item;
    }

    public Item GetItemInRightFoot()
    {
        if (foot_r.transform.childCount == 0)
            return null;
        return foot_r.transform.GetChild(0).GetComponent<Inventory_Item>().item;
    }

    public Item GetItemInLeftFoot()
    {
        if (foot_l.transform.childCount == 0)
            return null;
        return foot_l.transform.GetChild(0).GetComponent<Inventory_Item>().item;
    }
}
