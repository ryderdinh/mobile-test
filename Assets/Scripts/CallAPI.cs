using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class CallAPI
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task StartCall(string username, string password, Action<UserData> onSigninSuccess)
    {
        try
        {
            const string apiUrl = "https://api-demo.rofi.games/api/v1/users/auth";
            SigninFormData body = new(username, password);
            HttpContent httpContent = new StringContent(body.GetJsonString(), Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync(apiUrl, httpContent);

            if (res.IsSuccessStatusCode)
            {
                var resBody = await res.Content.ReadAsStringAsync();
                var accessToken = JsonUtility.FromJson<AuthToken>(resBody).accessToken;
                var getUserData = new GetUserData();
                await getUserData.StartCall(accessToken, onSigninSuccess);
            }
            else
            {
                Debug.Log($"Error: {res.StatusCode} - {res.ReasonPhrase}");
            }
        }
        catch (Exception err)
        {
            Debug.Log($"Error: {err.Message}");
        }
    }
}

public class GetUserData
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task StartCall(string accessToken, Action<UserData> onSuccess)
    {
        try
        {
            const string apiUrl = "https://api-demo.rofi.games/api/v1/users/info";
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var res = await _httpClient.GetAsync(apiUrl);

            if (res.IsSuccessStatusCode)
            {
                var resBody = await res.Content.ReadAsStringAsync();
                var data = JsonUtility.FromJson<UserInfo>(resBody).data;
                Debug.Log(data.GetJsonString());

                onSuccess(data);

            }
            else
            {
                Debug.Log($"Error: {res.StatusCode} - {res.ReasonPhrase}");
            }
        }
        catch (Exception err)
        {
            Debug.Log($"Error: {err.Message}");
        }
    }
}