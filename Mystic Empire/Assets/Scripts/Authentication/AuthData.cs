using System;
using System.Collections;
using System.Collections.Generic;

public class AuthData
{
    public static AuthData Instance;
    public long Uid { get; private set; }
    public string SessionToken { get; private set; }
    public string Username { get; private set; }

    
    public AuthData(long uid, string sessionToken, string username)
    {
        Uid = uid;
        SessionToken = sessionToken;
        Username = username;
        Instance = this;
    }
}
