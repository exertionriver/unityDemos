using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[Serializable]
public class AuthResp
{
    public static AuthResp fromJson(string json) {
        return JsonUtility.FromJson<AuthResp>(json) ;
    }
    public int code ;
    public string details ;
    public Player player = null ;

    public bool NoError { get {return code == 0 ; }

    }

}