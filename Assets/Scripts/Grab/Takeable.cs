using UnityEngine;

public class Takeable : MonoBehaviour
{
    public Item itemToTake;
    public EffectAfterTake afterTake;

    public Item Take()
    {
        if (afterTake == EffectAfterTake.DestroyGameObject)
        {
            Destroy(this.gameObject);
        }
        return itemToTake;
    }
}

public enum EffectAfterTake
{
    DestroyGameObject,
    Nothing
}
