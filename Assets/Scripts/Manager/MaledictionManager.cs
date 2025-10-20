using UnityEngine;

public class MaledictionManager : MonoBehaviour
{
    public static MaledictionManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Speech speech;
    public Malediction[] maledictions;

    private void Start()
    {
        foreach (Malediction item in maledictions)
        {
            item.Init();
        }
    }

    public void MaledictionCheck()
    {
        foreach (Malediction malediction in maledictions)
        {
            if (malediction.CanBeCast())
            {
                Debug.Log(malediction.maledictionName + " lancez");
                malediction.CastAllEffect();
            }
        }

        //On vas reset la valeur contenue dans le speech pour ne pas garder en mémoire un text qui date de 3h et lancer une malédiction au pif
        speech.ResetSpeechText();
    }
}