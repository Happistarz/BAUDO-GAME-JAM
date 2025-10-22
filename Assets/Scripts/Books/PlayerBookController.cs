using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBookController : MonoBehaviour
{
    [SerializeField] private InputActionReference previousBookAction;
    [SerializeField] private InputActionReference nextBookAction;
    [SerializeField] private InputActionReference closeBookAction;
    
    [SerializeField] public BookController bookController;
    
    [SerializeField] public Image leftArrowImage;
    [SerializeField] public Image rightArrowImage;
    
    private int _currentBookIndex;
    
    private void Start()
    {
        previousBookAction.action.Enable();
        nextBookAction.action.Enable();
        closeBookAction.action.Enable();
        
        previousBookAction.action.started += _ => PreviousBook();
        nextBookAction.action.started += _ => NextBook();
        closeBookAction.action.started += _ => StartCoroutine(CloseBook());
    }
    
    public void OpenBook()
    {
        if (bookController == null) return;
        if (bookController.isBookOpen) return;
        
        if (_currentBookIndex < 0 || _currentBookIndex >= BooksManager.Instance.books.Length)
        {
            Debug.LogWarning("Invalid book index.");
            return;
        }

        bookController.OpenBook(BooksManager.Instance.books[_currentBookIndex]);
        UpdateNavigationArrows();
    }

    private void NextBook()
    {
        if (bookController == null) return;
        if (_currentBookIndex >= BooksManager.Instance.books.Length - 1) return;
        _currentBookIndex++;
        bookController.SetData(BooksManager.Instance.books[_currentBookIndex]);
        UpdateNavigationArrows();
    }

    private void PreviousBook()
    {
        if (bookController == null) return;
        if (_currentBookIndex <= 0) return;
        _currentBookIndex--;
        bookController.SetData(BooksManager.Instance.books[_currentBookIndex]);
        UpdateNavigationArrows();
    }
    
    private void UpdateNavigationArrows()
    {
        leftArrowImage.gameObject.SetActive(_currentBookIndex > 0);
        rightArrowImage.gameObject.SetActive(_currentBookIndex < BooksManager.Instance.books.Length - 1);
    }

    private IEnumerator CloseBook()
    {
        if (bookController == null) yield break;
        if (!bookController.isBookOpen) yield break;
        bookController.isBookOpen = false;
        
        yield return bookController.CloseBook();
    }
}
