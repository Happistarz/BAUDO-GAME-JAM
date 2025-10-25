using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string InteractionText => isOpen ? "Close" : "Open";

    public bool isOpen = false;
    public float angle = 90f;
    public float doorSpeed = 0.5f;
    public GameObject pivotPoint;
    
    public AudioSource audioSource;

    public void Interact()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public IEnumerator RotateDoor(Vector3 finalRotation)
    {
        float t = 0;
        Vector3 originalAngle = pivotPoint.transform.rotation.eulerAngles;
        while (t < doorSpeed)
        {
            pivotPoint.transform.rotation = Quaternion.Euler(Vector3.Lerp(originalAngle, finalRotation, t / doorSpeed));
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            audioSource.Play();
            Vector3 finalRotation = pivotPoint.transform.rotation.eulerAngles;
            finalRotation.y += angle;
            StartCoroutine(RotateDoor(finalRotation));
            isOpen = !isOpen;
        }
    }
    
    public void Close()
    {
        if (isOpen)
        {
            audioSource.Play();
            Vector3 finalRotation = pivotPoint.transform.rotation.eulerAngles;
            finalRotation.y -= angle;
            StartCoroutine(RotateDoor(finalRotation));
            isOpen = !isOpen;
        }
    }

    public bool IsInteractalbe()
    {
        return true;
    }
}
