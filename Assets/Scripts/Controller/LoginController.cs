using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public static LoginController Instance;
    [SerializeField] Button _buttonlogin;
    [SerializeField] Button _buttonconfirm;
    [SerializeField] TMP_InputField _inputfieldTK;
    [SerializeField] TMP_InputField _inputfieldMK;
    [Header("DKTK")]
    [SerializeField] Button _buttonCreateTK;
    [SerializeField] TMP_InputField _inputfieldCreateTK;
    [SerializeField] TMP_InputField _inputfieldCreateMK;
    [Header("CreatePlayer")]
    [SerializeField] TMP_InputField _inputnamePlayer;
    [SerializeField] Button _buttonCreatePlayer;
    public string tk;
    public string mk;
    public GameObject _panelLogin;
    public GameObject _panelCrerateTK;
    public GameObject _panelCreatePlayer;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        tk = string.Empty;
        mk = string.Empty;
        _buttonlogin.onClick.AddListener(() =>
        {
            Login();
        });
        _buttonconfirm.onClick.AddListener(() =>
        {
            ConfirmTK();
        });
        _buttonCreateTK.onClick.AddListener(() => {
            SendCreateTK();
        });
        _buttonCreatePlayer.onClick.AddListener(() => {
            ClientMain.Instance.CreatePlayer(_inputnamePlayer.text);
        });
    }
    public void SendCreateTK()
    {
        ClientMain.Instance.CreateTK(_inputfieldCreateTK.text, _inputfieldCreateMK.text);
    }
    public void Login()
    {
        if (tk=="")
        {
            _panelLogin.SetActive(true);
            _buttonlogin.gameObject.SetActive(false);
        }
        else
        {
            ClientMain.Instance.SendLogin(tk, mk);
        }
    }

    public void ConfirmTK()
    {
        tk=_inputfieldTK.text;
        mk=_inputfieldMK.text;
        _buttonlogin.gameObject.SetActive(true);
    }
    public void ActiveCreatePlayer()
    {
        _panelLogin.gameObject.SetActive(false);
        _panelCrerateTK.gameObject.SetActive(false);
        _panelCreatePlayer.gameObject.SetActive(true); 
    }
}
