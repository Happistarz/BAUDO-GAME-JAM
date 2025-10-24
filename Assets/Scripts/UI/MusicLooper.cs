using System;
using System.Collections;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public float loopDelay = 10f;

    private bool _isWaiting;
    private int _currentLoop;

    private void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[_currentLoop]);
        StartCoroutine(WaitForClipEnd());
    }

    private IEnumerator WaitForClipEnd()
    {
        while (true)
        {
            yield return new WaitForSeconds(audioClips[_currentLoop].length);

            if (_isWaiting) continue;
            
            _isWaiting = true;
            yield return new WaitForSeconds(loopDelay);
            
            _currentLoop = (_currentLoop + 1) % audioClips.Length;
            
            audioSource.PlayOneShot(audioClips[_currentLoop]);
            _isWaiting = false;
        }
    }
}
