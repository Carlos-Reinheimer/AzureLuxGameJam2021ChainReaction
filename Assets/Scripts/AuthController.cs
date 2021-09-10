using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class AuthController : MonoBehaviour
{
    [SerializeField] InputField Displayname, RegisterEmailAddress, RegisterPassword, LoginEmailAddress, LoginPassword;

    public GameObject LoginTab, RegisterTab, LoginButton, RegisterButton, LoginLoading, RegisterLoading, RegisterSuccess;
    public Text LoginErrorMessage, RegisterErrorMessage;

    private void Update()
    {
        if (LoginTab.activeSelf)
        {
            EnableLoginButton();
        }

        if (RegisterTab.activeSelf)
        {
            EnableRegisterButton();
        }


    }


    // -------------  CONTROLS --------------
    public void SwitchToLogin()
    {
        LoginTab.SetActive(true);
        RegisterTab.SetActive(false);
        RegisterErrorMessage.text = "";
        LoginErrorMessage.text = "";
    }

    public void SwitchToRegister()
    {
        RegisterTab.SetActive(true);
        LoginTab.SetActive(false);
        RegisterErrorMessage.text = "";
        LoginErrorMessage.text = "";
        RegisterSuccess.SetActive(false);
    }


    public void EnableLoginButton()
    {
        if ((LoginEmailAddress.text == "") || (LoginPassword.text.Length < 6)) LoginButton.GetComponent<Button>().interactable = false;
        else LoginButton.GetComponent<Button>().interactable = true;

    }

    public void EnableRegisterButton()
    {
        if ((Displayname.text == "") || (RegisterEmailAddress.text == "") || (RegisterPassword.text.Length < 6)) RegisterButton.GetComponent<Button>().interactable = false;
        else RegisterButton.GetComponent<Button>().interactable = true;

    }




    // ------------- LOGIN --------------
    public void Login()
    {
        LoginButton.SetActive(false);
        LoginLoading.SetActive(true);
        RegisterSuccess.SetActive(false);

        var request = new LoginWithEmailAddressRequest
        {
            Email = LoginEmailAddress.text,
            Password = LoginPassword.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, onLoginSuccess, onLoginError);
    }

    private void onLoginSuccess(LoginResult result)
    {
        Debug.Log("Logged in");
        LoginLoading.SetActive(false);
        LoginButton.SetActive(true);
        SceneManager.LoadSceneAsync("Game");
    }

    private void onLoginError(PlayFabError error)
    {
        Debug.Log("Login error");
        LoginLoading.SetActive(false);
        LoginButton.SetActive(true);
        Debug.LogError(error.GenerateErrorReport());

        switch (error.Error)
        {
            case PlayFabErrorCode.AccountNotFound:
                LoginErrorMessage.text = "Account not found!";
                break;
            case PlayFabErrorCode.InvalidEmailOrPassword:
                LoginErrorMessage.text = "Invalid email address or password!";
                break;
            case PlayFabErrorCode.InvalidPassword:
                LoginErrorMessage.text = "Invalid password!";
                break;
            case PlayFabErrorCode.InvalidEmailAddress:
                LoginErrorMessage.text = "Invalid email address!";
                break;
            case PlayFabErrorCode.InvalidParams:
                LoginErrorMessage.text = "Invalid params!";
                break;
            default:
                LoginErrorMessage.text = "There was an unknown error!";
                break;
        }
    }






    // ------------- REGISTER --------------
    public void RegisterUser()
    {
        RegisterButton.SetActive(false);
        RegisterLoading.SetActive(true);

        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = Displayname.text,
            Username = Displayname.text,
            Email = RegisterEmailAddress.text,
            Password = RegisterPassword.text,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, onRegisterSuccess, onRegisterError);
    }

    private void onRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register Success");
        RegisterLoading.SetActive(false);
        RegisterButton.SetActive(true);
        SwitchToLogin();
        RegisterSuccess.SetActive(true);
    }

    private void onRegisterError(PlayFabError error)
    {
        Debug.Log("Register error");
        RegisterLoading.SetActive(false);
        RegisterButton.SetActive(true);
        Debug.LogError(error.GenerateErrorReport());

        switch (error.Error)
        {
            case PlayFabErrorCode.EmailAddressNotAvailable:
                RegisterErrorMessage.text = "Email not available!";
                break;
            case PlayFabErrorCode.InvalidUsername:
                RegisterErrorMessage.text = "Invalid username!";
                break;
            case PlayFabErrorCode.InvalidPassword:
                RegisterErrorMessage.text = "Invalid password!";
                break;
            case PlayFabErrorCode.InvalidEmailAddress:
                RegisterErrorMessage.text = "Invalid email address!";
                break;
            case PlayFabErrorCode.ProfaneDisplayName:
                RegisterErrorMessage.text = "Profane displayname!";
                break;
            default:
                RegisterErrorMessage.text = "There was an unknown error!";
                break;
        }
    }
}
