using Menu.Connet;
using TMPro;
using UI.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIWindow
{
    public class UIRoom : UIBase
    {
        [SerializeField] private TextMeshProUGUI _room;
        [Header("Me")]
        [SerializeField] private Transform _me;
        [SerializeField] private TextMeshProUGUI _nameMe;
        [SerializeField] private TextMeshProUGUI _levelMe;
        [SerializeField] private Image _avatarMe;
        [Header("Other")]
        [SerializeField] private Transform _other;
        [SerializeField] private TextMeshProUGUI _nameOther;
        [SerializeField] private TextMeshProUGUI _levelOther;
        [SerializeField] private Image _avatarOther;
        [SerializeField] private Button _btnStart;
        [SerializeField] private Button _btnLeave;
        

        

        public override void Init()
        {
            base.Init();
                _btnStart.onClick.AddListener(() =>
                {
                   
                });
                _btnLeave.onClick.AddListener(() =>
                {
                    ClientMain.Instance.SendLeaveRoom();
                    CloseMe();
                });
        }
        public void SetRoomInfo(string roomID)
        {
            _room.text = $"Phòng số: {roomID}";
        }
        public void SetMeInfo(string name, int level)
        {
            _nameMe.text = name;
            _levelMe.text = level.ToString();

        }
        public void SetOtherInfo(string name, int level)
        {
            _nameOther.text = name;
            _levelOther.text = level.ToString();
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

