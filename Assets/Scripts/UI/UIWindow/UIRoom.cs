using Menu.Connet;
using TMPro;
using UI.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIWindow
{
    public class PlayerRoomInfo
    {
        public string name;
        public int level;
        public int playerId;
    }
    public class UIRoom : UIBase
    {
        [SerializeField] private TextMeshProUGUI _room;
        [Header("Me")]
        [SerializeField] private Transform _me;
        [SerializeField] private TextMeshProUGUI _nameMe;
        [SerializeField] private TextMeshProUGUI _levelMe;
        [SerializeField] private Image _avatarMe;
        public PlayerRoomInfo playerRoomInfo;
        [Header("Other")]
        [SerializeField] private Transform _other;
        [SerializeField] private TextMeshProUGUI _nameOther;
        [SerializeField] private TextMeshProUGUI _levelOther;
        [SerializeField] private Image _avatarOther;
        public PlayerRoomInfo playerRoomInfoOther;
        [SerializeField] private Button _btnStart;
        [SerializeField] private Button _btnLeave;
        public bool isHost;



        public override void Init()
        {
            base.Init();
                _btnStart.onClick.AddListener(() =>
                {
                   ClientMain.Instance.SendStartGame();
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
            playerRoomInfo = new PlayerRoomInfo()
            {
                name = name,
                level = level,
                playerId = GameData.Instance._mainPlayer._playerid
            };

        }
        public void SetOtherInfo(string name, int level)
        {
            _nameOther.text = name;
            _levelOther.text = level.ToString();
            _other.gameObject.SetActive(true);
        }

        
        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }

        public override void Open()
        {
            base.Open();
            if (!isHost)
            {
                _btnStart.interactable = isHost;
            }
        }

        public override void OpenMe()
        {
            base.OpenMe();
        }
        public override void Close()
        {
            base.Close();
            isHost = false;
            _other.gameObject.SetActive(false);
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

