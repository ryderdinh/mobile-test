using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button myButton;
    public Button signoutButton;
    public InputField uinput;
    public InputField pinput;
    public InputField Idinput;
    public InputField Emailinput;

    [FormerlySerializedAs("Panel1")] public GameObject panel1;
    [FormerlySerializedAs("Panel2")] public GameObject panel2;
    public GameObject panel3;

    public void Start()
    {
        myButton.onClick.AddListener(SignIn);
        signoutButton.onClick.AddListener(OnSignOutSuccess);
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(true);
    }

    private async void SignIn()
    {
        var username = uinput.text;
        var password = pinput.text;

        // With HttpClient
        var callApi = new CallAPI();
        await callApi.StartCall(username, password, OnSigninSuccess);

        // With UnityWebRequest
        // if (panel3 == null) return;
        // var callApiUnity = panel3.AddComponent<CallAPIUnity>();
        // callApiUnity.Authentication(username, password, OnSigninSuccess);
    }

    private void OnSigninSuccess(UserData data)
    {
        panel1.SetActive(false);
        panel2.SetActive(true);

        Idinput.text = data.id;
        Emailinput.text = data.email;
    }

    private void OnSignOutSuccess()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }
}