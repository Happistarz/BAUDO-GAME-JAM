using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BooksManager : MonoBehaviour
{
    public static BooksManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    [SerializeField] public InputActionReference playerBookInputAction;

    public List<BookData> books;

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