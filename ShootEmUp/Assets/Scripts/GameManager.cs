using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform PlayerCameraTransform;
    public Transform PlayerTransform;

    public static GameManager instance = null;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PlayerCamera = Camera.main;
        PlayerCameraTransform = PlayerCamera.transform;




    }


}
