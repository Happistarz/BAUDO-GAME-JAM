using System;
using System.Collections;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    public float maxSize;
    public float minSize;
    public float timeToChangeSize;

    public void Grow()
    {
        if (transform.localScale.y == minSize)
        {
            Debug.Log("Agrandissement");
            StartCoroutine(ChangeToSize(maxSize));
        }
    }

    public void Shrink()
    {
        if (gameObject.transform.localScale.y == maxSize)
        {
            Debug.Log("Retressisement");
            StartCoroutine(ChangeToSize(minSize));
        }
    }
    
    private IEnumerator ChangeToSize(float desiredSize)
    {
        float t = 0;
        float actualSize = transform.localScale.y;
        while (t < timeToChangeSize)
        {
            float newY = Mathf.Lerp(actualSize, desiredSize, t / timeToChangeSize);
            transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(transform.localScale.x, desiredSize, transform.localScale.z);
    }
}
