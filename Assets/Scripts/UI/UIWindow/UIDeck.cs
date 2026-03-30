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
        public List<Transform> transforms;
        public TextMeshProUGUI _gold;
        public TextMeshProUGUI _diamond;

        public async override void Init()
        {
            base.Init();
            await UIDelaySystem.Delay(2f, this);
            CreatePlayerCard();
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
                foreach (Transform child in contentmoster)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in contentspell)
                {
                    Destroy(child.gameObject);
                }
                foreach (Transform child in contentTrap)
                {
                    Destroy(child.gameObject);
                }
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
            for (int i = 0; i < buttons.Count; i++)
            {
                Image image = buttons[i].GetComponent<Image>();
                if (image != null)
                {
                    if (i == index)
                    {
                        image.color = Color.green;
                        transforms[i].gameObject.SetActive(true);

                    }
                    else
                    {
                        image.color = Color.white;
                        transforms[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        public void CreatePlayerCard()
        {
            foreach (Transform child in contentmoster)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in contentspell)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in contentTrap)
            {
                Destroy(child.gameObject);
            }
            var d = GameData.Instance._mainPlayer._playerCardData;
            foreach (var item in d.AllCard)
            {
                Card card = GameData.Instance.GetCardByID(item.Key);
                if (card != null)
                {
                    if (card._CardType is 1)
                    {
                        ItemSlotDeck monstercard = Instantiate(prefabMosters, contentmoster);
                        monstercard.card = card;
                        monstercard.Init();
                        monstercard._numberCard.text = "x " +item.Value.ToString();
                        monstercard.gameObject.SetActive(true);
                        monstercard.type = 1;
                        listMonsterCard.Add(monstercard);
                    }
                    else if (card._CardType is 2)
                    {
                        ItemSlotDeck spellcard = Instantiate(prefabSpells, contentspell);
                        spellcard.card = card;
                        spellcard.Init();
                        spellcard._numberCard.text = "x " + item.Value.ToString();
                        spellcard.gameObject.SetActive(true);
                        spellcard.type = 1;
                        listSpellCard.Add(spellcard);
                    }
                    else
                    {
                        ItemSlotDeck trapcard = Instantiate(prefabTraps, contentTrap);
                        trapcard.card = card;
                        trapcard.Init();
                        trapcard._numberCard.text = "x " + item.Value.ToString();
                        trapcard.gameObject.SetActive(true);
                        trapcard.type = 1;
                        listTrapCard.Add(trapcard);
                    }
                }


            }
            SortMonster();
            SortSpell();
            SortTrapCard();
        }
        public void CreateDeck()
        {
            foreach (Transform child in contentDeck)
            {
                Destroy(child.gameObject);
            }
            var d = GameData.Instance._mainPlayer._playerDeckCard;
            foreach (var item in d.Cards)
            {
                Card card = GameData.Instance.GetCardByID(item.Key);
                if (card != null)
                {
                    if (card._CardType is 1)
                    {
                        ItemSlotDeck monstercard = Instantiate(prefabMosters, contentDeck);
                        monstercard.card = card;
                        monstercard.Init();
                        monstercard._numberCard.text = "x " + item.Value.ToString();
                        monstercard.gameObject.SetActive(true);
                        monstercard.type = 2;
                        listDeckCard.Add(monstercard);
                    }
                    else if (card._CardType is 2)
                    {
                        ItemSlotDeck spellcard = Instantiate(prefabSpells, contentDeck);
                        spellcard.card = card;
                        spellcard.Init();
                        spellcard._numberCard.text = "x " + item.Value.ToString();
                        spellcard.gameObject.SetActive(true);
                        spellcard.type = 2;
                        listDeckCard.Add(spellcard);
                    }
                    else
                    {
                        ItemSlotDeck trapcard = Instantiate(prefabTraps, contentDeck);
                        trapcard.card = card;
                        trapcard.Init();
                        trapcard._numberCard.text = "x " + item.Value.ToString();
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
            listDeckCard = listDeckCard
                .OrderBy(x => x.card._CardType)
                .ThenBy(x => x.card._CardType == 1 ? x.card._Level : int.MaxValue)
                .ToList();

            for (int i = 0; i < listDeckCard.Count; i++)
            {
                listDeckCard[i].transform.SetSiblingIndex(i);
            }
        }
        public void SortMonster()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };

            listMonsterCard = listMonsterCard
                .OrderBy(x => rarityOrder.ContainsKey(x.card._Rarity)
                                ? rarityOrder[x.card._Rarity]
                                : int.MaxValue)
                .ThenBy(x => x.card._Level) // Monster level thấp → cao
                .ToList();

            for (int i = 0; i < listMonsterCard.Count; i++)
            {
                listMonsterCard[i].transform.SetSiblingIndex(i);
            }
        }
        public void SortSpell()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };

            listSpellCard = listSpellCard
                .OrderBy(x => rarityOrder.ContainsKey(x.card._Rarity)
                                ? rarityOrder[x.card._Rarity]
                                : int.MaxValue)
               
                .ToList();

            for (int i = 0; i < listSpellCard.Count; i++)
            {
                listSpellCard[i].transform.SetSiblingIndex(i);
            }
        }
        public void SortTrapCard()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };

            listTrapCard = listTrapCard
                .OrderBy(x => rarityOrder.ContainsKey(x.card._Rarity)
                                ? rarityOrder[x.card._Rarity]
                                : int.MaxValue)
                
                .ToList();

            for (int i = 0; i < listTrapCard.Count; i++)
            {
                listTrapCard[i].transform.SetSiblingIndex(i);
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
            SortMonster();
            SortSpell();
            SortTrapCard();

        }
        public void UpdateDeckCard(int id, int quantity)
        {
            if (quantity == 0)
            {
                ItemSlotDeck itemSlotDeck = listDeckCard.FirstOrDefault(x => x.card._CardId == id);
                listDeckCard.Remove(itemSlotDeck);
                Destroy(itemSlotDeck.gameObject);
            }
            else
            {
                ItemSlotDeck card2 = listDeckCard.FirstOrDefault(x => x.card._CardId == id);
                if (card2 != null)
                {
                    foreach (var item in listDeckCard)
                    {
                        if (item.card._CardId == id)
                        {
                            item._numberCard.text = "x " + quantity.ToString();
                        }
                    }
                }
                else
                {
                    Card card = GameData.Instance.GetCardByID(id);
                    if (card._CardType is 1)
                    {
                        ItemSlotDeck monstercard = Instantiate(prefabMosters, contentDeck);
                        monstercard.card = card;
                        monstercard.Init();
                        monstercard._numberCard.text = "x " + quantity.ToString();
                        monstercard.gameObject.SetActive(true);
                        monstercard.type = 2;
                        listDeckCard.Add(monstercard);
                    }
                    else if (card._CardType is 2)
                    {
                        ItemSlotDeck spellcard = Instantiate(prefabSpells, contentDeck);
                        spellcard.card = card;
                        spellcard.Init();
                        spellcard._numberCard.text = "x " + quantity.ToString();
                        spellcard.gameObject.SetActive(true);
                        spellcard.type = 2;
                        listDeckCard.Add(spellcard);
                    }
                    else
                    {
                        ItemSlotDeck trapcard = Instantiate(prefabTraps, contentDeck);
                        trapcard.card = card;
                        trapcard.Init();
                        trapcard._numberCard.text = "x " + quantity.ToString();
                        trapcard.gameObject.SetActive(true);
                        trapcard.type = 2;
                        listDeckCard.Add(trapcard);
                    }
                }

            }
            SortDeckUI();
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

