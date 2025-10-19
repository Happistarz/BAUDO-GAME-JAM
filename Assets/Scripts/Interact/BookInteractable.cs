using UnityEngine;

public class BookInteractable : MonoBehaviour, IInteractable
{
    public BookData bookData;
    public string InteractionText => "Open";

    public void Interact()
    {
        if (bookData != null)
        {
            BooksManager.Instance.OpenBook(bookData);
        }
        else
        {
            Debug.LogWarning("No BookData assigned to this Interactable.");
        }
    }
}