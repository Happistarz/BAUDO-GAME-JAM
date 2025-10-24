using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MaledictionDoorEvent : MonoBehaviour
{
    public string maledictionText;
    public Door doorToClose;
    Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<InteractionController>(out InteractionController interactionController))
        {
            interactionController.canOpenDoor = false;
            _collider.enabled = false;
            doorToClose.Close();
            GameManager.Instance.ShowCurseUI(maledictionText);
        }
    }
}
