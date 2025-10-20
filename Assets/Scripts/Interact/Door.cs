using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Door : MonoBehaviour, IInteractable
{
    public string InteractionText => "Open";

    public bool isOpen = false;
    public float angle = 90f;
    public float doorSpeed = 0.5f;
    public GameObject pivotPoint;

    public void Interact()
    {
        Vector3 finalRotation = pivotPoint.transform.rotation.eulerAngles;
        if (isOpen)
            finalRotation.y -= angle;
        else
            finalRotation.y += angle;
        isOpen = !isOpen;
        Debug.Log(finalRotation);
        StartCoroutine(RotateDoor(finalRotation));
    }
    
    public IEnumerator RotateDoor(Vector3 finalRotation)
    {
        float t = 0;
        Vector3 originalAngle = pivotPoint.transform.rotation.eulerAngles;
        while (t < doorSpeed)
        {
            pivotPoint.transform.rotation =  Quaternion.Euler(Vector3.Lerp(originalAngle, finalRotation, t / doorSpeed));
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
