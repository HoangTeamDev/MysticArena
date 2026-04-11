using CardData;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UI.ItemUI;
using UI.SystemUI;
using UIScripts.SystemUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIWindow
{
    public class UIDeck : UIBase
    {
        [Title("conttent")]
        [SerializeField] private Transform contentmoster;
        [SerializeField] private Transform contentspell;
        [SerializeField] private Transform contentTrap;
        [SerializeField] private Transform contentDeck;
        [Title("prefab")]
        [SerializeField] private ItemSlotDeck prefabMosters;
        [SerializeField] private ItemSlotDeck prefabSpells;
        [SerializeField] private ItemSlotDeck prefabTraps;

        public List<ItemSlotDeck> listMonsterCard;
        public List<ItemSlotDeck> listSpellCard;
        public List<ItemSlotDeck> listTrapCard;

        public List<ItemSlotDeck> listDeckCard;
        [Title("Button")]
       
        public List<Button> buttons;
        public Sprite _btnShow;
        public Sprite _btnHide;
        public List<Transform> transforms;
        public TextMeshProUGUI _gold;
        public TextMeshProUGUI _diamond;
        public int indextab = 0;
        public int curenttab = 0;
        public int totaltabMonster = 0;
        public int totaltabSpell = 0;
        public int totaltabTraps = 0;
        [Title("text")]
        public TextMeshProUGUI _quantityMonster;
        public TextMeshProUGUI _quantitySpell;
        public TextMeshProUGUI _quantityTrap;
        public TextMeshProUGUI _quantityCard;
        public async override void Init()
        {
            base.Init();
            indextab = 0;
          
           
            UpdateCurrency();
            for (int i = 0; i < buttons.Count; i++)
            {
                int x = i;
                buttons[i].onClick.AddListener(() =>
                {

                    OpenTab(x);
                });
            }
            OpenTab(0);
            CreateDeck();
            GameEvent.Instance.Subscribe(ListEvent.Currency.ToString(), () =>
            {

                UpdateCurrency();
            });
            GameEvent.Instance.Subscribe(ListEvent.UpdatePlayerCard.ToString(), () =>
            {
                
                CreatePlayerCard();
            });
            GameEvent.Instance.Subscribe(ListEvent.UpdateDeck.ToString(), () =>
            {
                foreach (Transform child in contentDeck)
                {
                    Destroy(child.gameObject);
                }
                CreateDeck();
            });
            var d = GameData.Instance._mainPlayer._playerCardData;
            totaltabMonster = Mathf.CeilToInt((float)d.MonsterCard.Count / 14);
            totaltabSpell = Mathf.CeilToInt((float)d.SpellCard.Count / 14);
            totaltabTraps = Mathf.CeilToInt((float)d.TrapCard.Count / 14);

        }
        public void UpdateCurrency()
        {
            var d = GameData.Instance._mainPlayer;
            AnimateTextNumber(_gold, d._gold);
            AnimateTextNumber(_diamond, d._diamond);
        }
        private void AnimateTextNumber(TextMeshProUGUI textComponent, long newValue)
        {
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NumberGroupSeparator = ".";
            textComponent.text = newValue.ToString("N0", culture);
        }
        public void OpenTab(int index)
        {
            indextab = 0;
            curenttab = index;  
            for (int i = 0; i < buttons.Count; i++)
            {
                Image image = buttons[i].GetComponent<Image>();
                if (image != null)
                {
                    if (i == index)
                    {
                        image.sprite =_btnShow;
                        transforms[i].gameObject.SetActive(true);
                        CreatePlayerCard();
                    }
                    else
                    {
                        image.sprite = _btnHide;
                        transforms[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        public void PreTab()
        {
            switch (curenttab)
            {
                case 0:
                    if (indextab <= 0)
                    {
                        indextab = totaltabMonster - 1;
                    }
                    else
                    {
                        indextab -= 1;
                    }
                    break;
                case 1:
                    if (indextab <= 0)
                    {
                        indextab = totaltabSpell - 1;
                    }
                    else
                    {
                        indextab -= 1;
                    }
                    break;
                case 2:
                    if (indextab <= 0)
                    {
                        indextab = totaltabTraps - 1;
                    }
                    else
                    {
                        indextab -= 1;
                    }
                    break;
            }
           

            
            CreatePlayerCard();
        }
        public void NextTab()
        {
            switch (curenttab)
            {
                case 0:
                    if (indextab >= totaltabMonster-1)
                    {
                        indextab = 0;
                    }
                    else
                    {
                        indextab += 1;
                    }
                    break;
                case 1:
                    if (indextab >= totaltabSpell - 1)
                    {
                        indextab = 0;
                    }
                    else
                    {
                        indextab += 1;
                    }
                    break;
                case 2:
                    if (indextab >= totaltabTraps - 1)
                    {
                        indextab = 0;
                    }
                    else
                    {
                        indextab += 1;
                    }
                    break;
            }
          

            
            CreatePlayerCard();
        }
        public void UpdateQuantity()
        {
            var b = GameData.Instance._mainPlayer;
            int m = b._playerDeckCard.MonsterCard.Count;
            int s= b._playerDeckCard.SpellCard.Count;
            int t= b._playerDeckCard.TrapCard.Count;
            _quantityCard.text=$"{m+s+t}/30";
            _quantityMonster.text = $"Quái: {m}";
            _quantitySpell.text = $"Phép: {s}";
            _quantityTrap.text = $"Bẫy: {t}";
          
        }
        public void CreatePlayerCard()
        {
           
           var d = GameData.Instance._mainPlayer._playerCardData;
            switch (curenttab)
            {
                case 0:
                    {
                        foreach(var data in listMonsterCard)
                        {
                            data.gameObject.SetActive(false);
                        }
                        int indexvalue = 0;
                        int pageSize = 14;

                        int startIndex = indextab * pageSize;
                        int endIndex = Mathf.Min(startIndex + pageSize, d.MonsterCard.Count);

                        for (int i = startIndex; i < endIndex; i++)
                        {
                           
                            Card card = GameData.Instance.GetCardByID(d.MonsterCard[i]._CardId);
                            ItemSlotDeck monstercard = listMonsterCard[indexvalue];
                            monstercard.card = card;
                            monstercard.Init();
                            monstercard._numberCard.text = "x " + d.MonsterCard[i]._quantity.ToString();
                            monstercard.gameObject.SetActive(true);
                            monstercard.type = 1;                           
                            indexvalue++;
                        }
                    }
                    break;
                case 1:
                    {
                        foreach (var data in listSpellCard)
                        {
                            data.gameObject.SetActive(false);
                        }
                        int indexvalue = 0;
                        int pageSize = 14;

                        int startIndex = indextab * pageSize;
                        int endIndex = Mathf.Min(startIndex + pageSize, d.SpellCard.Count);

                        for (int i = startIndex; i < endIndex; i++)
                        {

                            Card card = GameData.Instance.GetCardByID(d.SpellCard[i]._CardId);
                            ItemSlotDeck monstercard = listSpellCard[indexvalue];
                            monstercard.card = card;
                            monstercard.Init();
                            monstercard._numberCard.text = "x " + d.SpellCard[i]._quantity.ToString();
                            monstercard.gameObject.SetActive(true);
                            monstercard.type = 1;                          
                            indexvalue++;
                        }
                    }
                    break;
                case 2:
                    {
                        foreach (var data in listTrapCard)
                        {
                            data.gameObject.SetActive(false);
                        }
                        int indexvalue = 0;
                        int pageSize = 14;

                        int startIndex = indextab * pageSize;
                        int endIndex = Mathf.Min(startIndex + pageSize, d.TrapCard.Count);

                        for (int i = startIndex; i < endIndex; i++)
                        {

                            Card card = GameData.Instance.GetCardByID(d.TrapCard[i]._CardId);
                            ItemSlotDeck monstercard = listTrapCard[indexvalue];
                            monstercard.card = card;
                            monstercard.Init();
                            monstercard._numberCard.text = "x " + d.TrapCard[i]._quantity.ToString();
                            monstercard.gameObject.SetActive(true);
                            monstercard.type = 1;                          
                            indexvalue++;
                        }
                    }
                    break;
            }
            UpdateQuantity();


        }
        public void CreateDeck()
        {
            foreach (Transform child in contentDeck)
            {
                Destroy(child.gameObject);
            }
            var d = GameData.Instance._mainPlayer._playerDeckCard;
            foreach (var item in d.MonsterCard)
            {
                Card card = GameData.Instance.GetCardByID(item._CardId);
                if (card != null)
                {
                    if (card._CardType is 1)
                    {
                        ItemSlotDeck monstercard = Instantiate(prefabMosters, contentDeck);
                        monstercard.card = card;
                        monstercard.Init();
                        monstercard._numberCard.text = "x " + item._quantity.ToString();
                        monstercard.gameObject.SetActive(true);
                        monstercard.type = 2;
                        listDeckCard.Add(monstercard);
                    }
                }
            }
            foreach (var item in d.SpellCard)
            {
                Card card = GameData.Instance.GetCardByID(item._CardId);
                if (card != null)
                {
                    ItemSlotDeck monstercard = Instantiate(prefabSpells, contentDeck);
                    monstercard.card = card;
                    monstercard.Init();
                    monstercard._numberCard.text = "x " + item._quantity.ToString();
                    monstercard.gameObject.SetActive(true);
                    monstercard.type = 2;
                    listDeckCard.Add(monstercard);
                }
            }
            foreach (var item in d.TrapCard)
            {
                Card card = GameData.Instance.GetCardByID(item._CardId);
                if (card != null)
                {
                    ItemSlotDeck monstercard = Instantiate(prefabTraps, contentDeck);
                    monstercard.card = card;
                    monstercard.Init();
                    monstercard._numberCard.text = "x " + item._quantity.ToString();
                    monstercard.gameObject.SetActive(true);
                    monstercard.type = 2;
                    listDeckCard.Add(monstercard);
                }
            }
         

        }
      
       
       

        public void UpdatePlayerCard(int id, int quantity)
        {
            Card cardvb = GameData.Instance.GetCardByID(id);
            if (quantity == 0)
            {
                
                ItemSlotDeck itemSlotDeck = null;
                if (cardvb._CardType is 1)
                {
                    itemSlotDeck = listMonsterCard.FirstOrDefault(x => x.card._CardId == id);
                    listMonsterCard.Remove(itemSlotDeck);

                }
                if (cardvb._CardType is 2)
                {
                    itemSlotDeck = listSpellCard.FirstOrDefault(x => x.card._CardId == id);
                    listMonsterCard.Remove(itemSlotDeck);
                }
                if (cardvb._CardType is 3)
                {
                    itemSlotDeck = listTrapCard.FirstOrDefault(x => x.card._CardId == id);
                    listMonsterCard.Remove(itemSlotDeck);
                }
                Destroy(itemSlotDeck.gameObject);
            }
            else
            {
                Card cardvb1 = GameData.Instance.GetCardByID(id);
                ItemSlotDeck itemSlotDeck=null;
                if (cardvb1._CardType is 1)
                {
                     itemSlotDeck = listMonsterCard.FirstOrDefault(x => x.card._CardId == id);
                    if (itemSlotDeck != null)
                    {
                        foreach (var item in listMonsterCard)
                        {
                            if (item.card._CardId == id)
                            {
                                item._numberCard.text = "x " + quantity.ToString();
                            }
                        }
                    }
                }
                if (cardvb1._CardType is 2)
                {
                    itemSlotDeck = listSpellCard.FirstOrDefault(x => x.card._CardId == id);
                    if (itemSlotDeck != null)
                    {
                        foreach (var item in listSpellCard)
                        {
                            if (item.card._CardId == id)
                            {
                                item._numberCard.text = "x " + quantity.ToString();
                            }
                        }
                    }
                }
                if (cardvb1._CardType is 3)
                {
                    itemSlotDeck = listTrapCard.FirstOrDefault(x => x.card._CardId == id);
                    if (itemSlotDeck != null)
                    {
                        foreach (var item in listTrapCard)
                        {
                            if (item.card._CardId == id)
                            {
                                item._numberCard.text = "x " + quantity.ToString();
                            }
                        }
                    }
                }


                if (itemSlotDeck is null)
                {
                    Card card = GameData.Instance.GetCardByID(id);
                    if (card != null)
                    {
                        if (card._CardType is 1)
                        {
                            ItemSlotDeck monstercard = Instantiate(prefabMosters, contentmoster);
                            monstercard.card = card;
                            monstercard.Init();
                            monstercard._numberCard.text = "x " + quantity.ToString();
                            monstercard.gameObject.SetActive(true);
                            monstercard.type = 1;
                            listMonsterCard.Add(monstercard);
                        }
                        else if (card._CardType is 2)
                        {
                            ItemSlotDeck spellcard = Instantiate(prefabSpells, contentspell);
                            spellcard.card = card;
                            spellcard.Init();
                            spellcard._numberCard.text = "x " +quantity.ToString();
                            spellcard.gameObject.SetActive(true);
                            spellcard.type = 1;
                            listSpellCard.Add(spellcard);
                        }
                        else
                        {
                            ItemSlotDeck trapcard = Instantiate(prefabTraps, contentTrap);
                            trapcard.card = card;
                            trapcard.Init();
                            trapcard._numberCard.text = "x " +quantity.ToString();
                            trapcard.gameObject.SetActive(true);
                            trapcard.type = 1;
                            listTrapCard.Add(trapcard);
                        }
                    }
                }

            }
          

        }
        public void UpdateDeckCard(Card card)
        {
            if (card._quantity == 0)
            {
                ItemSlotDeck itemSlotDeck = listDeckCard.FirstOrDefault(x => x.card._CardId == card._CardId);
                listDeckCard.Remove(itemSlotDeck);
                Destroy(itemSlotDeck.gameObject);
            }
            else
            {
                ItemSlotDeck card2 = listDeckCard.FirstOrDefault(x => x.card._CardId == card._CardId);
                if (card2 != null)
                {
                    card2._numberCard.text= "x " + card._quantity.ToString();
                   
                }
                else
                {
                    Card card1 = GameData.Instance.GetCardByID(card._CardId);
                    if (card._CardType is 1)
                    {
                        ItemSlotDeck monstercard = Instantiate(prefabMosters, contentDeck);
                        monstercard.card = card1;
                        monstercard.Init();
                        monstercard._numberCard.text = "x " + card._quantity.ToString();
                        monstercard.gameObject.SetActive(true);
                        monstercard.type = 2;
                        listDeckCard.Add(monstercard);
                    }
                    else if (card._CardType is 2)
                    {
                        ItemSlotDeck spellcard = Instantiate(prefabSpells, contentDeck);
                        spellcard.card = card1;
                        spellcard.Init();
                        spellcard._numberCard.text = "x " + card._quantity.ToString();
                        spellcard.gameObject.SetActive(true);
                        spellcard.type = 2;
                        listDeckCard.Add(spellcard);
                    }
                    else
                    {
                        ItemSlotDeck trapcard = Instantiate(prefabTraps, contentDeck);
                        trapcard.card = card1;
                        trapcard.Init();
                        trapcard._numberCard.text = "x " + card._quantity.ToString();
                        trapcard.gameObject.SetActive(true);
                        trapcard.type = 2;
                        listDeckCard.Add(trapcard);
                    }
                }

            }
            SortDeckUI();
        }
        public void SortDeckUI()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };
            listDeckCard = listDeckCard
                .OrderBy(x => rarityOrder.ContainsKey(x.card._Rarity)
                                ? rarityOrder[x.card._Rarity]
                                : int.MaxValue)
                .ThenBy(x => x.card._CardType == 1 ? x.card._Level : int.MaxValue)
                .ThenBy(x=>x.card._Name)
                .ToList();

            for (int i = 0; i < listDeckCard.Count; i++)
            {
                listDeckCard[i].transform.SetSiblingIndex(i);
            }
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

