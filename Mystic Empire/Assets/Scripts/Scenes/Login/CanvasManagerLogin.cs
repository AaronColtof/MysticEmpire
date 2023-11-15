using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Net.Mail;
using System.Linq;

public class CanvasManagerLogin : MonoBehaviour
{
    public static CanvasManagerLogin Instance;
    private int _currentState = 0;
    private string _errorMessage = "";

    [Header("UI Items")] 
    [SerializeField] private GameObject loginUI;
    [SerializeField] private GameObject registerUI;
    [SerializeField] private GameObject errorUI;
    [SerializeField] private List<TMP_InputField> inputFields;
    [SerializeField] private TMP_InputField loginEmailField;
    [SerializeField] private TMP_InputField loginPasswordField;
    [SerializeField] private TMP_InputField registerEmailField;
    [SerializeField] private TMP_InputField registerPasswordField;
    [SerializeField] private TMP_InputField registerUsernameField;
    [SerializeField] private TextMeshProUGUI registerButtonText;
    [SerializeField] private TextMeshProUGUI loginButtonText;
    [SerializeField] private TextMeshProUGUI errorUIText;
    [SerializeField] private Color defaultTextColor;
    [SerializeField] private Color errorTextColor;

    void Start()
    {
        if (Instance != null && Instance == this)
        {
            //An instance already exists, deleting this instance
            Destroy(this);
        }

        Instance = this;
        ChangeUIAccordingToState(_currentState);
        errorUI.SetActive(false);
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
        foreach (var vTMPInputField in inputFields)
        {
            vTMPInputField.text = string.Empty;
        }
    }

    public void ChangeToLoginUI()
    {
        _currentState = 0;
        ChangeUIAccordingToState(_currentState);
    }

    public void ChangeToRegisterUI()
    {
        _currentState = 1;
        ChangeUIAccordingToState(_currentState);
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("Start");
    }

    public void Login()
    {
        if (loginEmailField.text != string.Empty && loginPasswordField.text != string.Empty)
        {
            throw new NotImplementedException();
        }
        else
        {
            _errorMessage = "Please fill in all forms";
            StartCoroutine(ShowLoginError());
        }
    }

    public void Register()
    {
        if (IsEmail(registerEmailField.text) && IsStrongPassword(registerPasswordField.text) && IsUsername(registerUsernameField.text))
        {
            throw new NotImplementedException();
        }
        else
        {
            StartCoroutine(ShowRegisterError());
        }
    }

    private bool IsEmail(string email)
    {
        try
        {
            var emailAddress = new MailAddress(email);
            return true;
        }
        catch
        {
            _errorMessage = "Invalid email address";
            return false;
        }
    }

    private bool IsStrongPassword(string password)
    {
        const int minLength = 8;
        const string specialCharacters = "!@#$%^&*()-+";

        if (password.Length < minLength)
        {
            _errorMessage =  "Password should be at least 8 characters long.";
            return false;
        }

        if (!password.Any(char.IsUpper))
        {
            _errorMessage = "Password should contain at least one uppercase letter.";
            return false;
        }

        if (!password.Any(char.IsLower))
        {
            _errorMessage = "Password should contain at least one lowercase letter.";
            return false;
        }

        if (!password.Any(char.IsDigit))
        {
            _errorMessage = "Password should contain at least one digit.";
            return false;
        }

        if (!password.Any(c => specialCharacters.Contains(c)))
        {
            _errorMessage = "Password should contain at least one special character (!@#$%^&*()-+).";
            return false;
        }

        return true;
    }

    private bool IsUsername(string username)
    {
        if (username.Length < 8 || username.Contains(" "))
        {
            _errorMessage = "Username must be at least 8 characters long and can't contain spaces";
            return false;
        }

        return true;
    }

    IEnumerator ShowRegisterError()
    {
        registerButtonText.color = errorTextColor;
        registerButtonText.text = _errorMessage;
        
        yield return new WaitForSeconds(5);
        
        registerButtonText.color = defaultTextColor;
        registerButtonText.text = "Sign up";
    }
    
    IEnumerator ShowLoginError()
    {
        loginButtonText.color = errorTextColor;
        loginButtonText.text = _errorMessage;
        
        yield return new WaitForSeconds(5);
        
        loginButtonText.color = defaultTextColor;
        loginButtonText.text = "Sign in";
    }

    public void ThrowServerError(string message)
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        errorUI.SetActive(true);
        errorUIText.text = message;
    }

    public void ExitServerError()
    {
        errorUI.SetActive(false);
        ChangeUIAccordingToState(_currentState);
    }
}
