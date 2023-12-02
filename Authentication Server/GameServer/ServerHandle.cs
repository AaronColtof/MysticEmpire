using System;
using System.Collections.Generic;
using System.Text;

namespace MysticEmpireAuthenticationServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int fromClient, Packet packet)
        {
            int clientIdCheck = packet.ReadInt();

            Console.WriteLine($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}.");
            if (fromClient != clientIdCheck)
            {
                Console.WriteLine($"Player (ID: {fromClient}) has assumed the wrong client ID ({clientIdCheck})!");
            }
        }

        public static void Login(int fromClient, Packet packet) 
        {
            int clientIdCheck = packet.ReadInt();

            if (clientIdCheck != fromClient) { return; }

            string email = packet.ReadString();
            string password = packet.ReadString();

            Console.WriteLine($"[Login] Received email: {email} and password: {password}");

            ServerSend.LoginAnswer(fromClient, 234234234, "bullshit", "username", false, String.Empty);
        }

        public static void Register(int fromClient, Packet packet) 
        {
            int clientIdCheck = packet.ReadInt();

            if (clientIdCheck != fromClient) { return; }

            string email = packet.ReadString();
            string password = packet.ReadString();
            string username = packet.ReadString();

            Console.WriteLine($"[Register] Received email: {email}, password: {password} and username: {username}");

            ServerSend.RegisterAnswer(fromClient, 232343423, "faeaweraweraer", "username", false, String.Empty);
        }
    }
}
