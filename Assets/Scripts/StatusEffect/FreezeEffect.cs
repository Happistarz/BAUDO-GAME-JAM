using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer), typeof(AudioSource))]
public class FreezeEffect : MonoBehaviour
{
    [Header("Status and interaction")]
    public bool isFroozen;
    public bool shouldEvaporate;

    [Header("Materials")]
    public Material waterMaterial;
    public Material frozenMaterial;

    [Header("Sound Clip")]
    public AudioClip freezeClip;
    public AudioClip unfreezeClip;

    Collider _collider;
    MeshRenderer _meshRenderer;
    AudioSource _audioSource;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Freeze()
    {
        if (!isFroozen)
        {
            _meshRenderer.material = frozenMaterial;
            _collider.isTrigger = false;
            isFroozen = true;
            _audioSource.PlayOneShot(freezeClip);
        }
    }

    public void UnFreeze()
    {
        if (isFroozen)
        {
            _audioSource.PlayOneShot(unfreezeClip);
            isFroozen = false;
            if (shouldEvaporate)
            {
                _meshRenderer.enabled = false;
                _collider.enabled = false;
            }
            else
            {
                _meshRenderer.material = waterMaterial;
                _collider.isTrigger = true;
            }
        }
    }
}
