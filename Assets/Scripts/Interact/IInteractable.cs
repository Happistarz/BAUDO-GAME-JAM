public interface IInteractable
{
    string InteractionText { get; }
    bool IsInteractalbe();
    void Interact();
}