using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform PlayerCameraTransform;
    public Transform PlayerTransform;
    public Collider[] BoarderColliders;
    public static GameManager instance = null;



    private void Awake()
    {
        if (instance != null)
        {

        }
        else
        {
            instance = this;
        }

        PlayerCamera = Camera.main;
        PlayerCameraTransform = PlayerCamera.transform;




    }


}
