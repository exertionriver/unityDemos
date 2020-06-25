using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Text playerDisplay ;
    public Button registerButton;
    public Button loginButton;
    public Button playButton;

    void Start() {
        playerDisplay = GameObject.Find("txtLoginStatus").GetComponent<Text>();

        registerButton = GameObject.Find("btnRegister").GetComponent<Button>() ;
        loginButton = GameObject.Find("btnLogin").GetComponent<Button>() ;
        playButton = GameObject.Find("btnPlay").GetComponent<Button>() ;

        registerButton.interactable = !DBManager.LoggedIn;
        loginButton.interactable = !DBManager.LoggedIn;
        playButton.interactable = DBManager.LoggedIn;

        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.player.username ;
        } else {
            playerDisplay.text = "No user logged in." ;
        }
    }
    public void GoToRegister() {
        SceneManager.LoadScene("registermenu");
    }

    public void GoToLogin() {
        SceneManager.LoadScene("loginmenu");
    }


    public void GoToGame() {
        SceneManager.LoadScene("game");
    }
}
