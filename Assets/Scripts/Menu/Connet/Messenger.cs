using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIScripts.SystemUI;
public class Messenger: MonoBehaviour 
{
    public void Handle(Message message)
    {
        MainLog.Log("Nhận mess", message.Command.ToString(),ReadColor.Green );

        switch (message.Command)
        {
            case 1:
                {
                    HandleLoginSucces(message);
                    break;
                }
            case 2:
                
                break;
            case 3:
                HandleCreatePlayer(message);
                
                break;
            case 4:
                {
                    HandleInfo(message);
                }
                break;
            case 5:
                {
                    
                }
                break;
            case 10:
                {
                    string msg=message.readUTF();
                    Debug.Log(msg);
                }
                break;
           
            default:
                Debug.LogWarning("Unknown opcode: " + message);
                break;
        }
    }
    void HandleInfo(Message message) 
    {
        int id=message.readInt();
        string name=message.readUTF();
        int level=message.readInt();
        int gold=message.readInt();
        int diamond=message.readInt();
        MainLog.Log("Nhân vật", $"{name}  {level} {gold} {diamond}",ReadColor.Green);
    }
    void HandleCreatePlayer(Message message)
    {
        bool t = message.readBool();
        if (t)
        {
            LoginController.Instance.ActiveCreatePlayer();
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
