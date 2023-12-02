using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.instance.myId);

            SendTCPData(packet);
        }
    }

    public static void Login(string email, string password)
    {
        using (Packet packet = new Packet((int)ClientPackets.login))
        {
            packet.Write(Client.instance.myId);
            packet.Write(email);
            packet.Write(password);
            
            SendTCPData(packet);
        }
    }

    public static void Register(string email, string password, string username)
    {
        using (Packet packet = new Packet((int)ClientPackets.register))
        {
            packet.Write(Client.instance.myId);
            packet.Write(email);
            packet.Write(password);
            packet.Write(username);
            
            SendTCPData(packet);
        }
    }

    #endregion
}
