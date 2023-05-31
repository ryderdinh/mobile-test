using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CallAPIUnity : MonoBehaviour
{
    public void Authentication(string username, string password, Action<UserData> onSigninSuccess)
    {
        // Send the request
        StartCoroutine(GetToken(username, password, onSigninSuccess));
    }

    private IEnumerator GetToken(string username, string password, Action<UserData> onSigninSuccess)
    {
        Debug.Log("GetToken ");
        // Create data
        SigninFormData body = new(username, password);

        // Create a UnityWebRequest with the target URL and form data
        var url = "https://api-demo.rofi.games/api/v1/users/auth";

        using var request = new UnityWebRequest(url, "POST");
        // Set the request headers
        request.SetRequestHeader("Content-Type", "application/json");

        // Set the request body
        var bodyRaw = Encoding.UTF8.GetBytes(body.GetJsonString());
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.Success)
        {
            // Request succeeded
            var accessToken = JsonUtility.FromJson<AuthToken>(request.downloadHandler.text).accessToken;
            StartCoroutine(GetUserInfo(accessToken, onSigninSuccess));
        }
        else
            // Request failed
        {
            Debug.Log("Error: " + request.error);
        }
    }

    private IEnumerator GetUserInfo(string accessToken, Action<UserData> onSuccess)
    {
        // Create a UnityWebRequest with the target URL and form data
        const string url = "https://api-demo.rofi.games/api/v1/users/info";

        using var request = new UnityWebRequest(url, "GET");
        request.SetRequestHeader("Authorization", $"Bearer {accessToken}");
        // Set the request body
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Request succeeded
            var resBody = request.downloadHandler.text;
            var data = JsonUtility.FromJson<UserInfo>(resBody).data;
            Debug.Log(data.GetJsonString());
            onSuccess(data);
        }
        else
            // Request failed
        {
            Debug.Log("Error: " + request.error);
        }
    }
}