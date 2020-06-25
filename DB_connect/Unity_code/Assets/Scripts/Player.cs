using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[Serializable]
public class Player
{
    public string username ;
    public int score;

    public WWWForm getJsonWWWForm() {

        WWWForm returnForm = new WWWForm() ;

        returnForm.AddField("json", JsonUtility.ToJson(this));

        return returnForm ;
    }

}