using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Menu.Connet
{
    public partial class ClientMain : MonoBehaviour
    {
        TcpClient client;
        public static ClientMain Instance;

        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;

        public Messenger messenger;
        public GameBattleRead battleRead;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        async void Start()
        {
            await Connect();
        }
        private void OnApplicationQuit()
        {
            Disconet();
        }
        async Task Connect()
        {
            try
            {
                Debug.Log("Connecting to Server...");

                client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 7777);

                stream = client.GetStream();
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);

                Debug.Log("Connected to Server!");

                _ = Task.Run(ListenServer); // run in background thread


            }
            catch (Exception e)
            {
                Debug.LogError("Connect failed: " + e.Message);
            }
        }
        private void Update()
        {
            if (GameController.HasInstance)
            {
                GameController.Instance.Doupdate();
            }
        }
        async Task ListenServer()
        {
            try
            {
                Debug.Log("ListenServer started");

                while (client != null && client.Connected)
                {
                    if (stream == null || !stream.CanRead)
                        break;

                    // ======================
                    // READ OPCODE (2 bytes)
                    // ======================
                    byte[] opBuf = new byte[2];
                    int received = 0;

                    while (received < 2)
                    {
                        int r = await stream.ReadAsync(opBuf, received, 2 - received);
                        if (r <= 0) throw new Exception("Server disconnected");
                        received += r;
                    }

                    short opcode = BitConverter.ToInt16(opBuf, 0);

                    // ======================
                    // READ LENGTH (2 bytes)
                    // ======================
                    byte[] lenBuf = new byte[2];
                    received = 0;

                    while (received < 2)
                    {
                        int r = await stream.ReadAsync(lenBuf, received, 2 - received);
                        if (r <= 0) throw new Exception("Server disconnected");
                        received += r;
                    }

                    ushort length = BitConverter.ToUInt16(lenBuf, 0);
                    Debug.Log($"RECEIVED OPCODE = {opcode}, LENGTH = {length}");
                    // ======================
                    // READ PAYLOAD
                    // ======================
                    byte[] payload = Array.Empty<byte>();

                    if (length > 0)
                    {
                        payload = new byte[length];
                        received = 0;

                        while (received < length)
                        {
                            int r = await stream.ReadAsync(payload, received, length - received);
                            if (r <= 0) throw new Exception("Server disconnected");
                            received += r;
                        }
                    }

                    // ======================
                    // HANDLE MESSAGE
                    // ======================
                    Message msg = new Message(opcode, payload);
                    if(opcode is 14 or 15)
                    {
                        MainThreadDispatcher.EnqueueBattle(() => battleRead.Handle(msg));
                    }
                    else
                    {
                        MainThreadDispatcher.Enqueue(() => messenger.Handle(msg));

                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Disconnected: " + e.Message);
            }
            finally
            {
                Debug.Log("Server connection closed");

                reader?.Close();
                writer?.Close();
                stream?.Close();
                client?.Close();
            }
        }


        // ------------------------
        // SEND PACKETS
        // ------------------------

        public void Disconet()
        {
            reader?.Close();
            writer?.Close();
            stream?.Close();
            client?.Close();
        }
        public void Send(Message m)
        {
            if (m == null || client == null || !client.Connected)
                return;

            try
            {
                short cmd = m.Command;
                byte[] data = m.ToArray();

                writer.Write(cmd);
                ushort length = (ushort)(data?.Length ?? 0);
                writer.Write(length);

                if (data != null && data.Length > 0)
                    writer.Write(data);

                writer.Flush();

                Debug.Log("SEND OPCODE = " + cmd);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[SEND ERROR] " + ex.Message);
            }
        }
    }
}

