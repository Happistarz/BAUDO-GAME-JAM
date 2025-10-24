using UnityEngine;

public class EndInteractable : MonoBehaviour, IInteractable
{
    public string InteractionText => "Eteindre le reveil";
    public string EndScene;

    public void Interact()
    {
        GameManager.Instance.ChangeSceneTo(EndScene);
    }

    public bool IsInteractalbe()
    {
        return true;
    }
}
