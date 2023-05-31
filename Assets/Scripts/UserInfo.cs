using System;

[Serializable]
public class UserInfo : ApiSuccessResponse
{
    public UserData data;
}

[Serializable]
public class UserData
{
    public string id, userName, email, wallet, created, modified, connectWalletAt, needToSetup;
}