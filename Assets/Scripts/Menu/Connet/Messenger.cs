using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Messenger
{
    public void Handle(Message message)
    {
      

        switch (message.Command)
        {
            case 2:
                HandleChat(message);
                break;

            case 3:
                HandleNameAccepted(message);
                break;
            case 1000:
                {
                    string msg=message.readUTF();
                    Debug.Log(msg);
                }
                break;
            case 8:
                {
                    HandleLoginSucces(message);
                }
                break;
            case 9:
                {
                    HandleGetAllCard(message);
                }
                
                break;
            default:
                Debug.LogWarning("Unknown opcode: " + message);
                break;
        }
    }
    void HandleLoginSucces (Message message)
    {
       bool t =  message.readBool();
        if (t)
        {
            SceneManager.LoadScene(1);
        }
    }
    void HandleGetAllCard(Message message)
    {
        int cout=message.readInt();
        for (int i = 0; i < cout; i++)
        {
            string card=message.readUTF();
            Debug.Log("CardName:" + card);
        }
    }
    void HandleChat(Message message)
    {
        string msg = message.readUTF();
        string msg1 = message.readUTF();
        Debug.Log("CHAT FROM SERVER: " + msg+"___"+msg1);
    }

    void HandleNameAccepted(Message message)
    {
        string result = message.readUTF();
        Debug.Log("SERVER RESPONSE: " + result);
    }
}
