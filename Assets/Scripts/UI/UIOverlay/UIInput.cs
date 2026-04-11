using Menu.Connet;
using Menu.System;
using TMPro;
using UI.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIOvelay
{
    public enum TypeInput
    {
        JoinRoom,
    }
    public class UIInput : UIBase
    {
        [SerializeField] private TMP_InputField inputField;
        public Button _btnSummit;
        public TypeInput typeInput;
        public override void Init()
        {
            base.Init();
            _btnSummit.onClick.AddListener(OnSummit);
        }
        public void OnSummit()
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                MainLog.Log("Vui lòng nhập thông tin", "", ReadColor.Red);
                return;
            }
            switch (typeInput)
            {
                case TypeInput.JoinRoom:
                    int roomId;
                    if (!int.TryParse(inputField.text, out roomId))
                    {
                        MainLog.Log("Vui lòng nhập một số hợp lệ", "", ReadColor.Red);
                        return;
                    }
                    ClientMain.Instance.SendJoinRoom(roomId);
                    break;
                default:
                    break;
            }
            CloseMe();
        }
        public void Set(TypeInput typeInput)
        {
            this.typeInput = typeInput;
            OpenMe();
        }
        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }

        public override void Open()
        {
            base.Open();
        }

        public override void OpenMe()
        {
            base.OpenMe();
        }
        public override void Close()
        {
            base.Close();
        }

        public override void CloseMe()
        {
            base.CloseMe();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}

