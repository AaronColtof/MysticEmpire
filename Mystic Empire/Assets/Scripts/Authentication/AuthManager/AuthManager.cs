using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 26950;
    public int myId = 0;
    public TCP Tcp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Tcp = new TCP();
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        Tcp.Connect();
    }
    
    public class TCP
    {
        public TcpClient Socket;

        private NetworkStream _stream;
        private byte[] _receiveBuffer;

        public void Connect()
        {
            Socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            _receiveBuffer = new byte[dataBufferSize];
            Socket.BeginConnect(Instance.ip, Instance.port, ConnectCallback, Socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            Socket.EndConnect(result);

            if (!Socket.Connected)
            {
                return;
            }

            _stream = Socket.GetStream();

            _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = _stream.EndRead(result);
                if (byteLength <= 0)
                {
                    // TODO: disconnect
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(_receiveBuffer, data, byteLength);
                
                _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch
            {
                // TODO: disconnect
            }
        }
    }
}
