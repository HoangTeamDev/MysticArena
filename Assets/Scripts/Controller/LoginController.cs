using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [SerializeField] Button _buttonlogin;
    [SerializeField] Button _buttonconfirm;
    [SerializeField] TMP_InputField _inputfieldTK;
    [SerializeField] TMP_InputField _inputfieldMK;
    public string tk;
    public string mk;
    public GameObject _panelLogin;
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
    }
    public void Login()
    {
        if (tk=="")
        {
            _panelLogin.SetActive(true);
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
    }
}
