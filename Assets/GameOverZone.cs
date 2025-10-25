using System.Collections;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    public float timeOfDeath = 0.5f;
    public AudioClip deathSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        float t = 0;
        while (t < timeOfDeath)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.ReloadScene();
    }
}
