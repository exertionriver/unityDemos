using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public static class DBManager
{
    public static Player player ;

    public static bool LoggedIn { get { return player != null ; } }

    public static void LogOut() { player = null ; }

}