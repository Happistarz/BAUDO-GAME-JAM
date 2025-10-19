using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BooksManager : MonoBehaviour
{
    public static BooksManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] public InputActionReference playerBookInputAction;

    [SerializeField] public BookData[] books;

    [SerializeField] public BookController bookController;
    [SerializeField] public PlayerBookController playerBookController;
    
    private void Start()
    {
        playerBookInputAction.action.Enable();
        playerBookInputAction.action.started += ctx => OpenPlayerBook();
    }

    public void OpenBook(BookData bookData)
    {
        bookController.OpenBook(bookData);
    }

    private void OpenPlayerBook()
    {
        playerBookController.OpenBook();
    }
}