using Assets.Scripts.RoomAll;
using CardData;
using DG.Tweening;
using Menu.Connet;
using NUnit.Framework;
using RoomAll;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UI.SystemUI;
using UIScripts.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIWindow
{
    public class UIMainField : UIBase
    {
        [Header("Pre")]
        [SerializeField] private CardIntance _preCardMonster;
        [SerializeField] private CardIntance _preCardSpell;
        [SerializeField] private CardIntance _preCardTRap;
        [SerializeField] private CardIntance _preCardFake;
        public Transform _pointDraw;
        public Transform _pointDrawOther;
        public RectTransform _point3;
        public RectTransform _point4;
        public RectTransform _pointActive;
        [Header("Me")]
        [SerializeField] private TextMeshProUGUI _nameMe;
        [SerializeField] private TextMeshProUGUI _HPMe;
        [SerializeField] private TextMeshProUGUI _cardMe;
        [SerializeField] public CardRowLayout _cardRowLayoutMe;
        [SerializeField] private Button _btnEndTurn;
        [Header("Enemy")]
        [SerializeField] private TextMeshProUGUI _nameEnemy;
        [SerializeField] private TextMeshProUGUI _HPEnemy;
        [SerializeField] public CardRowLayout _cardRowLayoutEnemy;
        [SerializeField] private TextMeshProUGUI _cardDeckEnemy;
        [Title("TurnAndPhase")]
        [SerializeField] private TextMeshProUGUI _textChangePhase;
        [SerializeField] private TextMeshProUGUI _textTurn;
        [SerializeField] private TextMeshProUGUI _textTitleTurn;
        public TextMeshProUGUI txtTime;
        private Tween countdownTween;
        [Title("MonsterZone")]
        public List<RectTransform> monsterZoneMe;
        public List<CardIntance> cardIntancesmonsterZoneMe = new List<CardIntance>();
        public List<RectTransform> monsterZoneOther;
        public List<CardIntance> cardIntancesmonsterZoneOther = new List<CardIntance>();
        [Title("TrapZone")]
        public List<RectTransform> TrapZoneMe;
        public List<CardIntance> CardTrapZoneMe;
        public List<RectTransform> TrapZoneOther;
        public List<CardIntance> CardTrapZoneOther;
        public override void Init()
        {
            base.Init();
            _btnEndTurn.onClick.RemoveAllListeners();
            _btnEndTurn.onClick.AddListener(() =>
            {
                ClientMain.Instance.SendEndTurn();
            });
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
        public void StartCountdown(float duration)
        {
            countdownTween?.Kill(); // tránh chạy chồng

            countdownTween = DOVirtual.Float(duration, 0, duration, (value) =>
            {
                int seconds = Mathf.CeilToInt(value);
                txtTime.text = seconds.ToString() + "s";
            })
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("Hết giờ!");
            });
        }
        public async void TextChangePhase(string title,int turn,int playerId)
        {
            StartCountdown(60);
            _textTurn.text = turn.ToString();
            _btnEndTurn.interactable = playerId == GameData.Instance._mainPlayer._playerid;
            _textTitleTurn.text = playerId == GameData.Instance._mainPlayer._playerid ? "Me":"Other";
            await UIDelaySystem.WaitUntil(() => !_textChangePhase.gameObject.activeInHierarchy, this);
            _textChangePhase.transform.localScale = Vector3.one;
            _textChangePhase.gameObject.SetActive(true);
            _textChangePhase.text = title;
            _textChangePhase.transform.DOScale(1.3f,0.1f).SetEase(Ease.Linear).OnComplete(async ()=> {
                await UIDelaySystem.Delay(1, this);
                _textChangePhase.gameObject.SetActive(false);
            } );
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
                    item.InstanceId=data.InstanceId;
                    item.Card=data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if(data.Card._CardType is 2)
                {
                    var item = Instantiate(_preCardSpell, _pointDraw);
                    item.InstanceId = data.InstanceId;
                    item.Card=data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 3)
                {
                    var item = Instantiate(_preCardTRap, _pointDraw);
                    item.InstanceId = data.InstanceId;
                    item.Card=data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
               
            }                    
           await _cardRowLayoutMe.DrawMultipleCardsSequential(list);
            ClientMain.Instance.SendConfirmDrawStart();
        }
        public async void MeDrawCard(List<CardIntance> cardIntances)
        {
            List<RectTransform> list = new List<RectTransform>();           
            foreach (var data in cardIntances)
            {
                data.Card = GameData.Instance.GetCardByID(data.CardId);
                if (data.Card._CardType is 1)
                {
                    var item = Instantiate(_preCardMonster, _pointDraw);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 2)
                {
                    var item = Instantiate(_preCardSpell, _pointDraw);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 3)
                {
                    var item = Instantiate(_preCardTRap, _pointDraw);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
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
                data.Card = GameData.Instance.GetCardByID(data.CardId);
                if (data.Card._CardType is 1)
                {
                    var item = Instantiate(_preCardMonster, _pointDrawOther);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    item._frotCard.gameObject.SetActive(false);
                    item._BackGround.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 2)
                {
                    var item = Instantiate(_preCardSpell, _pointDrawOther);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    item._frotCard.gameObject.SetActive(false);
                    item._BackGround.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 3)
                {
                    var item = Instantiate(_preCardTRap, _pointDrawOther);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    item._frotCard.gameObject.SetActive(false);
                    item._BackGround.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
            }
          

            await _cardRowLayoutEnemy.DrawMultipleCardsSequential(list);
        }
        public async void EnemyDrawCard(List<CardIntance> cardIntances)
        {
            List<RectTransform> list = new List<RectTransform>();
            foreach (var data in cardIntances)
            {
                data.Card = GameData.Instance.GetCardByID(data.CardId);
                if (data.Card._CardType is 1)
                {
                    var item = Instantiate(_preCardMonster, _pointDrawOther);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    item._frotCard.gameObject.SetActive(false);
                    item._BackGround.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 2)
                {
                    var item = Instantiate(_preCardSpell, _pointDrawOther);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    item._frotCard.gameObject.SetActive(false);
                    item._BackGround.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
                if (data.Card._CardType is 3)
                {
                    var item = Instantiate(_preCardTRap, _pointDrawOther);
                    item.InstanceId = data.InstanceId;
                    item.Card = data.Card;
                    item.Init();
                    item.gameObject.SetActive(true);
                    item._frotCard.gameObject.SetActive(false);
                    item._BackGround.gameObject.SetActive(true);
                    list.Add(item.GetComponent<RectTransform>());
                }
            }
            await _cardRowLayoutEnemy.DrawMultipleCardsSequential(list);
        }
        #region//Summon
        public void NomalSummon( int playerid, CardIntance cardIntance)
        {
            if (playerid == GameData.Instance._mainPlayer._playerid)
            {
                foreach (var data in _cardRowLayoutMe.cards)
                {
                    CardIntance card = data.GetComponent<CardIntance>();
                    if(card != null && card.InstanceId == cardIntance.InstanceId)
                    {
               
                        _cardRowLayoutMe.RemoveCard(data);
                        cardIntancesmonsterZoneMe.Add(card);
                        card.SlotIndex=cardIntance.SlotIndex;
                        card.CurrentAtk = cardIntance.CurrentAtk;
                        card.CurrentHp = cardIntance.CurrentHp;
                        card.HasAttack = cardIntance.HasAttack;
                        card.SummonToSlot(cardIntance.SlotIndex);
                        break;
                    }
                }
            }
            else
            {
                foreach (var data in _cardRowLayoutEnemy.cards)
                {
                    CardIntance card = data.GetComponent<CardIntance>();
                    if (card != null && card.InstanceId == cardIntance.InstanceId)
                    {

                        _cardRowLayoutEnemy.RemoveCard(data);
                        _cardRowLayoutEnemy.cardIntances.Remove(card);                                         
                        cardIntancesmonsterZoneOther.Add(card);
                        card.SlotIndex = cardIntance.SlotIndex;
                        card.CurrentAtk = cardIntance.CurrentAtk;
                        card.CurrentHp = cardIntance.CurrentHp;
                        card.HasAttack = cardIntance.HasAttack;
                        card.EnemySummon(cardIntance.SlotIndex);

                           
                            
                        
                    
                        break;
                    }
                }
            }
        }


        #endregion
        #region Settrap
        public void SetTrap(int playerid, CardIntance cardIntance)
        {
            if (playerid == GameData.Instance._mainPlayer._playerid)
            {
                foreach (var data in _cardRowLayoutMe.cardIntances)
                {
                   
                    if ( data.InstanceId == cardIntance.InstanceId)
                    {

                        _cardRowLayoutMe.RemoveCard(data.rectTransform);
                        _cardRowLayoutMe.cardIntances.Remove(data);
                        data.SlotIndex = cardIntance.SlotIndex;
                        data.Set(data.SlotIndex);
                        break;
                    }
                }
            }
            else
            {
                foreach (var data in _cardRowLayoutEnemy.cardIntances)
                {

                    if (data.InstanceId == cardIntance.InstanceId)
                    {

                        _cardRowLayoutEnemy.RemoveCard(data.rectTransform);
                        _cardRowLayoutEnemy.cardIntances.Remove(data);
                        data.SlotIndex = cardIntance.SlotIndex;
                        data.Set(data.SlotIndex,false);
                        break;
                    }
                }
            }
        }
        #endregion
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
   
