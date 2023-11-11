using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManagerStart : MonoBehaviour
{
    public static CanvasManagerStart Instance;
    
    void Start()
    {
        if (Instance != null && Instance == this)
        {
            //An instance already exists, deleting this instance
            Destroy(this);
        }

        Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Login");
    }

    public void OpenSettings()
    {
        throw new NotImplementedException();
    }

    public void ExitGame()
    {
        throw new NotImplementedException();
    }
}
