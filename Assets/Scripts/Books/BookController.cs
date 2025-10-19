using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private static readonly int End = Animator.StringToHash("END");
    private static readonly int Start1 = Animator.StringToHash("START");
    
    public InputActionReference closeBookAction;
    public bool isBookOpen;
    
    [Header("UI Elements")]
    public GameObject bookUIPanel;
    public TMP_Text bookTitleText;
    public TMP_Text bookContentText;
    public Image bookCoverImage;
    
    public Animator bookAnimator;
    
    private void Start()
    {
        closeBookAction.action.started += _ => StartCoroutine(CloseBook());
    }
    
    public void OpenBook(BookData bookData)
    {
        if (GameManager.Instance.OnUI) return;
        GameManager.Instance.OnUI = true;
        
        isBookOpen = true;
        bookUIPanel.SetActive(true);
        SetData(bookData);
        bookAnimator.SetTrigger(Start1);
    }
    
    public void SetData(BookData bookData)
    {
        bookTitleText.text = bookData.title;
        bookContentText.text = bookData.content;
        bookCoverImage.sprite = bookData.cover;
    }
    
    public IEnumerator CloseBook()
    {
        if (!isBookOpen) yield break;
        
        isBookOpen = false;
        GameManager.Instance.OnUI = false;
        
        bookAnimator.SetTrigger(End);
        yield return new WaitForSeconds(0.6f);
        bookUIPanel.SetActive(false);
    }
}