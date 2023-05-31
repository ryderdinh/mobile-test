using UnityEngine;

public class SigninFormData
{
    public string account;
    public string password;

    public SigninFormData(string u, string p)
    {
        this.account = u;
        this.password = p;
    }
}
