using System;
using System.Collections.Generic;
using System.Text;

namespace MysticEmpireAuthenticationServer
{
    class ServerSend
    {
        private static void SendTCPData(int toClient, Packet packet)
        {
            packet.WriteLength();
            Server.clients[toClient].tcp.SendData(packet);
        }

        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(packet);
            }
        }
        private static void SendTCPDataToAll(int exceptClient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != exceptClient)
                {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }

        public static void Welcome(int toClient, string msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(toClient);
                _packet.Write(msg);          

                SendTCPData(toClient, _packet);
            }
        }

        public static void LoginAnswer(int toClient, long uid, string sessionToken, string username, bool failed, string error) 
        {
            using (Packet _packet = new Packet((int)ServerPackets.loginAnswer))
            {
                _packet.Write(uid);
                _packet.Write(sessionToken);
                _packet.Write(username);
                _packet.Write(failed);
                _packet.Write(error);

                SendTCPData(toClient, _packet);
            }
        }

        public static void RegisterAnswer(int toClient, long uid, string sessionToken, string username, bool failed, string error) 
        {
            using (Packet _packet = new Packet((int)ServerPackets.registerAnswer))
            {
                _packet.Write(uid);
                _packet.Write(sessionToken);
                _packet.Write(username);
                _packet.Write(failed);
                _packet.Write(error);

                SendTCPData(toClient, _packet);
            }
        }
    }
}
