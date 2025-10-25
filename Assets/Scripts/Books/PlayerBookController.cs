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
        previousBookAction.action.started += PreviousBook;
        nextBookAction.action.started += NextBook;
        closeBookAction.action.started += CloseBookFunction;
    }

    private void OnDestroy()
    {
        previousBookAction.action.started -= PreviousBook;
        nextBookAction.action.started -= NextBook;
        closeBookAction.action.started -= CloseBookFunction;
    }

    public void OpenBook()
    {
        if (bookController == null) return;
        if (bookController.isBookOpen) return;
        
        if (_currentBookIndex < 0 || _currentBookIndex >= BooksManager.Instance.books.Count)
        {
            Debug.LogWarning("Invalid book index.");
            return;
        }

        bookController.OpenBook(BooksManager.Instance.books[_currentBookIndex]);
        UpdateNavigationArrows();
    }

    private void NextBook(InputAction.CallbackContext context)
    {
        if (bookController == null) return;
        if (_currentBookIndex >= BooksManager.Instance.books.Count - 1) return;
        _currentBookIndex++;
        bookController.SetData(BooksManager.Instance.books[_currentBookIndex]);
        UpdateNavigationArrows();
    }

    private void PreviousBook(InputAction.CallbackContext context)
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
        rightArrowImage.gameObject.SetActive(_currentBookIndex < BooksManager.Instance.books.Count - 1);
    }

    private void CloseBookFunction(InputAction.CallbackContext context)
    {
        StartCoroutine(CloseBook());
    }

    private IEnumerator CloseBook()
    {
        if (bookController == null) yield break;
        if (!bookController.isBookOpen) yield break;

        StartCoroutine(bookController.CloseBook());
    }

    private void OnEnable()
    {
        previousBookAction.action.Enable();
        nextBookAction.action.Enable();
        closeBookAction.action.Enable();

        previousBookAction.action.started += PreviousBook;
        nextBookAction.action.started += NextBook;
        closeBookAction.action.started += CloseBookFunction;
    }
    private void OnDisable()
    {
        previousBookAction.action.Disable();
        nextBookAction.action.Disable();
        closeBookAction.action.Disable();

        previousBookAction.action.started -= PreviousBook;
        nextBookAction.action.started -= NextBook;
        closeBookAction.action.started -= CloseBookFunction;   
    }
}
