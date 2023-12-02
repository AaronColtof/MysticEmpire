using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        int myId = packet.ReadInt();
        string msg = packet.ReadString();

        Debug.Log($"Message from server: {msg}");
        Client.instance.myId = myId;
        ClientSend.WelcomeReceived();
    }

    public static void LoginAnswer(Packet packet)
    {
        long uid = packet.ReadLong();
        string sessionToken = packet.ReadString();
        string username = packet.ReadString();
        bool failed = packet.ReadBool();
        string message = packet.ReadString();
        
        Debug.Log("Server received login and responded.");

        if (failed)
        {
            CanvasManagerLogin.Instance.ThrowServerError(message);
        }
        else
        {
            AuthData authData = new AuthData(uid, sessionToken, username);
            Debug.Log($"UID: {uid}, session token: {sessionToken}, username: {username}");
        }
    }

    public static void RegisterAnswer(Packet packet)
    {
        long uid = packet.ReadLong();
        string sessionToken = packet.ReadString();
        string username = packet.ReadString();
        bool failed = packet.ReadBool();
        string message = packet.ReadString();
        
        Debug.Log("Server received register and responded.");

        if (failed)
        {
            CanvasManagerLogin.Instance.ThrowServerError(message);
        }
        else
        {
            AuthData authData = new AuthData(uid, sessionToken, username);
            Debug.Log($"UID: {uid}, session token: {sessionToken}, username: {username}");
        }
    }
}
