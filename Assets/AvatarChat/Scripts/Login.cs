using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

  public string Email;
  public InputField EmailField;
  public string Password;
  public InputField PasswordField;
  public string ScreenName;
  public InputField ScreenNameField;

  public string Token = "";
  public Text TokenField;
  string TokenURL = "https://bigfun.co.za/service/avatarchat/?f=gettoken&email={email}&pass={pass}&screenname={screenname}";

  // Use this for initialization
  void Start () {
    PlayerPrefs.SetString("token", "");
    CheckLogin();
	}

  void CheckLogin()
  {
    Email = PlayerPrefs.GetString("email", Email);
    EmailField.text = Email;
    Password = PlayerPrefs.GetString("password", Password);
    PasswordField.text = Password;
    ScreenName = PlayerPrefs.GetString("screenname", ScreenName);
    ScreenNameField.text = ScreenName;
    Token = PlayerPrefs.GetString("token", Token);
    TokenField.text = Token;

    StartCoroutine(GetAccessToken());
  }

  public void SaveAndLogin()
  {
    Email = EmailField.text;
    Password = PasswordField.text;
    ScreenName = ScreenNameField.text;
    SavePrefs();
    StartCoroutine(GetAccessToken());
  }

  void SavePrefs()
  {
    PlayerPrefs.SetString("email", Email);
    PlayerPrefs.SetString("password", Password);
    PlayerPrefs.SetString("screenname", ScreenName);
    PlayerPrefs.SetString("token", Token);
  }

  IEnumerator GetAccessToken()
  {
    string FullURL = TokenURL.Replace("{email}", Email);
    FullURL = FullURL.Replace("{password}", Password);
    FullURL = FullURL.Replace("{screenname}", ScreenName);

    WWW www = new WWW(FullURL);
    yield return www;

    if (!String.IsNullOrEmpty(www.error))
    {
      Debug.Log(www.error + ":" + www.text);      
      yield break;
    }

    Token = www.text;
    TokenField.text = Token;
    SavePrefs();
    SceneManager.LoadScene("Main");
  }


  // Update is called once per frame
  void Update () {
		
	}
}
