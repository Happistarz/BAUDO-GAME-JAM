using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    void Update()
    {
        Transform cameraTransform = GameManager.Instance.PlayerCam.transform;
        transform.forward = cameraTransform.forward * -1;
    }
}
