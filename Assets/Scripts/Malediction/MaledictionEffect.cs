using System;
using UnityEngine;

[Serializable]
public abstract class MaledictionEffect
{
    public abstract void Cast();
}

public class Froze : MaledictionEffect
{
    public override void Cast()
    {
        Transform playerView = GameManager.Instance.PlayerCam.transform;
        if (Physics.Raycast(playerView.position, playerView.forward, out RaycastHit hitInfo, 10))
        {
            if (hitInfo.collider.TryGetComponent<FreezeEffect>(out FreezeEffect freezeEffect))
            {
                freezeEffect.Freeze();
            }
        }
    }
}

public class UnFroze : MaledictionEffect
{
    public override void Cast()
    {
        Transform playerView = GameManager.Instance.PlayerCam.transform;
        if(Physics.Raycast(playerView.position, playerView.forward , out RaycastHit hitInfo, 10))
        {
            if (hitInfo.collider.TryGetComponent<FreezeEffect>(out FreezeEffect freezeEffect))
            {
                freezeEffect.UnFreeze();
            }
        }
    }
}

public class Grow : MaledictionEffect
{
    public override void Cast()
    {
        GameManager.Instance.GetSizeController().Grow();
    }
}

public class Shrink : MaledictionEffect
{
    public override void Cast()
    {
        GameManager.Instance.GetSizeController().Shrink();
    }
}