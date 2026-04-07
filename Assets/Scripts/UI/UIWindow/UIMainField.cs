using Assets.Scripts.RoomAll;
using TMPro;
using UI.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.UIWindow
{
    public class UIMainField : UIBase
    {
        [Header("Me")]
        [SerializeField] private TextMeshProUGUI _nameMe;
        [SerializeField] private TextMeshProUGUI _HPMe;
        [Header("Enemy")]
        [SerializeField] private TextMeshProUGUI _nameEnemy;
        [SerializeField] private TextMeshProUGUI _HPEnemy;


        public override void Init()
        {
            base.Init();
            
        }
        public void InitValue()
        {
            Room room = GameData.Instance.CurrentRoom;
            PlayerState me= room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.HostPlayer : room.GuestPlayer;
            PlayerState enemy = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.GuestPlayer : room.HostPlayer;
            _nameMe.text = me.PlayerName;
            _HPMe.text = $"{me.hp}";
            _nameEnemy.text = enemy.PlayerName;
            _HPEnemy.text = $"{enemy.hp}";

        }
        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }

        public override void Open()
        {
            base.Open();
            InitValue();
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
   
