using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    public static string GetJsonString(this object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public static void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}