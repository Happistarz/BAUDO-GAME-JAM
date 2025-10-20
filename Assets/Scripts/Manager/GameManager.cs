using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool OnUI { get; set; }

    public Camera PlayerCam;
    public GameObject MaledictionUI;
    public TextMeshProUGUI maledictionText;

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

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public InteractionController GetInteractionController()
    {
        return FindFirstObjectByType<InteractionController>();
    }

    public SizeController GetSizeController()
    {
        return FindFirstObjectByType<SizeController>();
    }

    public PlayerMovements GetPlayerMovement()
    {
        return FindFirstObjectByType<PlayerMovements>();
    }

    public void ShowCurseUI(string maledictionName)
    {
        MaledictionUI.SetActive(true);
        maledictionText.text = maledictionName;
    }

    public void HideCurseUI()
    {
        MaledictionUI.SetActive(false);
    }
}