using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class LoginMenu : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    public void CallLogin() {

        StartCoroutine(Login()) ;
    }

    IEnumerator Login() {

        var login = new AuthReq(nameField.text, passwordField.text) ;
        
        UnityWebRequest uwr = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", login.getJsonWWWForm()) ;
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError) {
            Debug.Log(uwr.error); 
        }

        Debug.Log(uwr.downloadHandler.text) ;

        try {
            var response = AuthResp.fromJson(uwr.downloadHandler.text) ;

            if (response.NoError && (response.player != null) ) {
                Debug.Log("User logged in successfully") ;
                DBManager.player = response.player ;
                UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
            } else {
                Debug.Log("User login failed. Error #" + response.code + " details: " + response.details) ;
            }
        } catch (Exception e) {
            Debug.Log("Bad json: " + e) ;
        }

    }

    public void VerifyInputs() {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

}
