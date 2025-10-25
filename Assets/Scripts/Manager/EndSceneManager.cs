using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{

    public AudioSource EndMusic;

    void Start()
    {
        EndMusic.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
