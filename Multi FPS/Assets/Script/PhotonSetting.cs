using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonSetting : MonoBehaviourPunCallbacks
{
    public InputField email;
    public InputField password;
    public InputField username;
    public InputField region;

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = username.text;

        //PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;

        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LogineFailure(PlayFabError error)
    {
        Debug.Log("로그인 실패");
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원가입 실패");
    }

    public void SignUp()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = username.text,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, SignUpSuccess, SignUpFailure);
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email=email.text,
            Password=password.text,
            
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LogineFailure);
    }
}
