using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform PlayerCameraTransform;
    public Transform PlayerTransform;
    public Collider[] BoarderColliders;
    public static GameManager instance = null;

    [Header("Enemie attribute scaling")]
    public float healthScaling;
    public float CurrentZombieHealth;

    [Header("UI")]
    public Canvas canvas;


    private void Awake()
    {



        if (instance != null)
        {

        }
        else
        {
            instance = this;
        }

        OnPausGame();


        PlayerCamera = Camera.main;
        PlayerCameraTransform = PlayerCamera.transform;




    }


    public void OnPausGame()
    {
        Time.timeScale = 0;
        canvas.enabled = true;

    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
        canvas.enabled = false;

    }


    public void OnReloadLevel()
    {
        SceneManager.LoadScene(0);
    }




}
