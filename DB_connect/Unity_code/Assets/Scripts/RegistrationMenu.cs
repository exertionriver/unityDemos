using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class RegistrationMenu : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    public void CallRegister() {

        StartCoroutine(Register()) ;
    }

    IEnumerator Register() {

        var register = new AuthReq(nameField.text, passwordField.text) ;
        
        UnityWebRequest uwr = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", register.getJsonWWWForm()) ;
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError) {
            Debug.Log(uwr.error);
        }

        Debug.Log(uwr.downloadHandler.text) ;

        try {
            var response = AuthResp.fromJson(uwr.downloadHandler.text) ;

            if (response.NoError) {
                Debug.Log("User created successfully") ;
                UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
            } else {
                Debug.Log("User creation failed. Error #" + response.code + " details: " + response.details) ;
            }
        } catch (Exception e) {
            Debug.Log("Bad json: " + e) ;
        }

    }

    public void VerifyInputs() {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

}
