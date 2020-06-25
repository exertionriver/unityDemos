using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    public Text playerDisplay ;
    public Text scoreDisplay;

    void Awake()
    {
        if(!DBManager.LoggedIn) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu") ;
        } else {
            playerDisplay.text = "Player: " + DBManager.player.username;
            scoreDisplay.text = "Score: " + DBManager.player.score;        
        }

    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData()) ;        
    }

    IEnumerator SavePlayerData() {
            
        UnityWebRequest uwr = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", DBManager.player.getJsonWWWForm()) ;
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError) {
            Debug.Log(uwr.error);
        }

        Debug.Log(uwr.downloadHandler.text) ;

        try {
            var response = AuthResp.fromJson(uwr.downloadHandler.text) ;

            if (response.NoError) {
                Debug.Log("User data saved successfully") ;
            } else {
                Debug.Log("User data save failed. Error #" + response.code + " details: " + response.details) ;
            }
        } catch (Exception e) {
            Debug.Log("Bad json: " + e) ;
        }

        DBManager.LogOut() ;
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu") ;
    }

    public void IncreaseScore() {
        DBManager.player.score++;
        scoreDisplay.text = "Score: " + DBManager.player.score;
    }
}
