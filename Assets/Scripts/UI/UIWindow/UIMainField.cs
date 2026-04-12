using Assets.Scripts.RoomAll;
using RoomAll;
using System.Collections.Generic;
using TMPro;
using UI.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.UIWindow
{
    public class UIMainField : UIBase
    {
        [Header("Pre")]
        [SerializeField] private CardIntance _preCardMonster;
        [SerializeField] private CardIntance _preCardSpell;
        [SerializeField] private CardIntance _preCardTRap;
        [SerializeField] private GameObject _preCardFake;
        public Transform _pointDraw;
        public Transform _pointDrawOther;
        [Header("Me")]
        [SerializeField] private TextMeshProUGUI _nameMe;
        [SerializeField] private TextMeshProUGUI _HPMe;
        [SerializeField] private TextMeshProUGUI _cardMe;
        [SerializeField] private CardRowLayout _cardRowLayoutMe;
        [Header("Enemy")]
        [SerializeField] private TextMeshProUGUI _nameEnemy;
        [SerializeField] private TextMeshProUGUI _HPEnemy;
        [SerializeField] private CardRowLayout _cardRowLayoutEnemy;
        [SerializeField] private TextMeshProUGUI _cardDeckEnemy;

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
            _cardDeckEnemy.text = enemy.Deck.Count.ToString();
            _cardMe.text = me.Deck.Count.ToString();
        }
        public void UpdateCardDeckMe()
        {
            Room room = GameData.Instance.CurrentRoom;
            PlayerState me = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.HostPlayer : room.GuestPlayer;
            _cardMe.text = me.Deck.Count.ToString();
        }
        public void UpdateCardDeckEnemy()
        {
            Room room = GameData.Instance.CurrentRoom;
            PlayerState enemy = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.GuestPlayer : room.HostPlayer;
            _cardDeckEnemy.text = enemy.Deck.Count.ToString();
        }

        [ContextMenu("MeDrawCard")]
        public async void MeDrawCard()
        {
            Debug.Log("drawMe");
            List<RectTransform> list = new List<RectTransform>();
            Room room = GameData.Instance.CurrentRoom;
            PlayerState me = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.HostPlayer : room.GuestPlayer;
            foreach(var data in me.Hand)
            {
                data.Card = GameData.Instance.GetCardByID(data.CardId);
                if(data.Card._CardType  is 1)
                {
                    var item = Instantiate(_preCardMonster, _pointDraw);
                    item.Card=data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if(data.Card._CardType is 2)
                {
                    var item = Instantiate(_preCardSpell, _pointDraw);
                    item.Card=data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 3)
                {
                    var item = Instantiate(_preCardTRap, _pointDraw);
                    item.Card=data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
               
            }                    
           await _cardRowLayoutMe.DrawMultipleCardsSequential(list);
        }
        [ContextMenu("EnemyDrawCard")]
        public async void EnemyDrawCard()
        {
            List<RectTransform> list = new List<RectTransform>();
            Room room = GameData.Instance.CurrentRoom;
            PlayerState enemy = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.GuestPlayer : room.HostPlayer;
            foreach (var data in enemy.Hand)
            {
                var item = Instantiate(_preCardFake, _pointDrawOther);
                item.gameObject.SetActive(true);
                list.Add(item.GetComponent<RectTransform>());
            }
          

            await _cardRowLayoutEnemy.DrawMultipleCardsSequential(list);
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
   
