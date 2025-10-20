using System;
using UnityEngine;

[Serializable]
public abstract class Prerequirement
{
    public abstract bool IsOkay();
    public abstract void InitPrerequirement();
}

[Serializable]
public class Prerequirement_Body : Prerequirement
{
    public BodyPart bodyPart;
    public Item item;
    private Inventory_Body _inventory_Body;

    public override void InitPrerequirement()
    {
        _inventory_Body = GameObject.FindAnyObjectByType<Inventory_Body>();
    }

    public override bool IsOkay()
    {
        return bodyPart switch
        {
            BodyPart.Head => _inventory_Body.GetItemInHead() == item,
            BodyPart.Left_Hand => _inventory_Body.GetItemInLeftHand() == item,
            BodyPart.Right_Hand => _inventory_Body.GetItemInRightHand() == item,
            BodyPart.Left_Foot => _inventory_Body.GetItemInLeftFoot() == item,
            BodyPart.Right_Foot => _inventory_Body.GetItemInRightFoot() == item,
            _ => false,
        };
    }
}

[Serializable]
public class Prerequirement_Speech : Prerequirement
{
    public string textWanted;
    private Speech _speech;
    public override void InitPrerequirement()
    {
        _speech = GameObject.FindAnyObjectByType<Speech>();
    }

    public override bool IsOkay()
    {
        return _speech.GetSpeechText().ToLower() == textWanted.ToLower();
    }
}

[Serializable]
public enum BodyPart
{
    Head,
    Left_Hand,
    Right_Hand,
    Left_Foot,
    Right_Foot
}