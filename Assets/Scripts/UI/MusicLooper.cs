using System;
using System.Collections;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    
    public AudioSource audioSource;
    public float loopDelay = 10f;

    private bool _isWaiting;

    private void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        StartCoroutine(WaitForClipEnd());
    }

    private IEnumerator WaitForClipEnd()
    {
        while (true)
        {
            yield return new WaitForSeconds(audioSource.clip.length);

            if (_isWaiting) continue;
            
            _isWaiting = true;
            yield return new WaitForSeconds(loopDelay);
            audioSource.Play();
            _isWaiting = false;
        }
    }
}
