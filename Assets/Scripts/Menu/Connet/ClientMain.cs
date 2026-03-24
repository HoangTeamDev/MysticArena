using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static UnityEditor.ShaderData;

public class ClientMain : MonoBehaviour
{
    TcpClient client;
    public static ClientMain Instance;

    NetworkStream stream;
    BinaryReader reader;
    BinaryWriter writer;

    Messenger messenger = new Messenger();

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

            SendName("Player_" + UnityEngine.Random.Range(1000, 9999));
        }
        catch (Exception e)
        {
            Debug.LogError("Connect failed: " + e.Message);
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
                ;
                MainThreadDispatcher.Enqueue(() => messenger.Handle(msg));
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
    [ContextMenu("Send")]
    public void sendOk()
    {
        SendLogin("testuser", "123456");
    }
    [ContextMenu("GetALlCard")]
    public void GetallCard()
    {
        Message msg = new Message(9);
        
        Send(msg);
    }
    public void SendLogin(string user, string pass)
    {
        Message msg = new Message(3);
        msg.writeUTF(user);
        msg.writeUTF(pass);
        Send(msg);
    }

    public void SendName(string name)
    {
        Message msg = new Message(1);
        msg.writeUTF(name);
        Send(msg);
    }

    public void SendChat(string mess)
    {
        Message msg = new Message(2);
        msg.writeUTF(mess);
        Send(msg);
    }
    public void Disconet()
    {
        reader?.Close();
        writer?.Close();
        stream?.Close();
        client?.Close();
    }
    void Send(Message m)
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
