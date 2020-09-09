
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Transform PlayerTransform;


    public float damping;
    
    private Transform cameraTransform;
    private Vector3 vel = Vector3.zero;

    private void Awake()
    {
        cameraTransform = transform;
    }


    private void FixedUpdate()
    {
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position,PlayerTransform.position, ref vel, damping);
    }
}
