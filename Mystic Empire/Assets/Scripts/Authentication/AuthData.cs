using System;
using System.Collections;
using System.Collections.Generic;

public class AuthData
{
    public static AuthData Instance;
    public int Uid { get; private set; }
    public string SessionToken { get; private set; }

    
    public AuthData(int uid, string sessionToken)
    {
        Uid = uid;
        SessionToken = sessionToken;
        Instance = this;
    }
}
