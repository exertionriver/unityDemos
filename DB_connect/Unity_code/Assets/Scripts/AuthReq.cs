using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[Serializable]
public class AuthReq
{

    public AuthReq(string username, string password) {
        this.username = username ;
        this.password = password ;
    }

    public string username ;
    public string password ;

    //https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
    public WWWForm getJsonWWWForm() {

        WWWForm returnForm = new WWWForm() ;

        returnForm.AddField("json", JsonUtility.ToJson(this));

        return returnForm ;
    }
}