using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManagerLogin : MonoBehaviour
{
    public static CanvasManagerLogin Instance;
    private int _currentState = 0;

    [Header("UI Items")] 
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject registerUI;
    [SerializeField] private List<TMP_InputField> _inputFields;

    void Start()
    {
        if (Instance != null && Instance == this)
        {
            //An instance already exists, deleting this instance
            Destroy(this);
        }

        Instance = this;
        ChangeUIAccordingToState(_currentState);
    }

    void ChangeUIAccordingToState(int currentState)
    {
        switch (currentState)
        {
            case 0:
                loginUI.SetActive(true);
                registerUI.SetActive(false);
                break;
            
            case 1:
                loginUI.SetActive(false);
                registerUI.SetActive(true);
                break;
            
            default:
                throw new Exception("An unusual currentState parameter was given.");
        }
        
        ResetInputFields();
    }

    void ResetInputFields()
    {
        foreach (var vTMPInputField in _inputFields)
        {
            vTMPInputField.text = string.Empty;
        }
    }

    public void LoginUI()
    {
        _currentState = 0;
        ChangeUIAccordingToState(_currentState);
    }

    public void RegisterUI()
    {
        _currentState = 1;
        ChangeUIAccordingToState(_currentState);
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("Start");
    }
}
