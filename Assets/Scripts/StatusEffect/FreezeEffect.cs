using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class FreezeEffect : MonoBehaviour
{
    [Header("Status and interaction")]
    public bool isFroozen;
    public bool shouldEvaporate;

    [Header("Materials")]
    public Material waterMaterial;
    public Material frozenMaterial;

    Collider _collider;
    MeshRenderer _meshRenderer;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Freeze()
    {
        if (!isFroozen)
        {
            _meshRenderer.material = frozenMaterial;
            _collider.isTrigger = false;
            isFroozen = true;
        }
    }

    public void UnFreeze()
    {
        if (isFroozen)
        {
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
