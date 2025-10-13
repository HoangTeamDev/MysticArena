using Card;
using Menu.System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.ItemUI;
using UI.SystemUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
using WebSocketSharp;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace UI.UIWindow
{
    public class UILibrary : UIBase
    {
        [Header("Object")]
        public GameObject main;
        [Header("Main")]
        public GameObject mainMonter;
        public GameObject ScollMonter;
        public GameObject mainSplell;
        public GameObject ScollSplell;
        public Button buttonMonter;
        public Button buttonSplell;
        public SpriteAtlas spriteAtlas;
        public List<TitleShop> _listButtonShop;
        public TitleShop currentSelected;
        [Header("Item")]
        public ItemSlotLibrary _nomalMonter;
        public ItemSlotLibrary _godMonter;
        public ItemSlotLibrary _spell;
        void OnTabClicked(TitleShop clicked)
        {
            for (int i = 0; i < _listButtonShop.Count; i++)
            {
                if (_listButtonShop[i] == clicked)
                {
                    _listButtonShop[i]._icon.color = ReadColor.FromHex(ReadColor.Green);

                }
                else
                {
                    _listButtonShop[i]._icon.color = ReadColor.FromHex(ReadColor.White);

                }
            }
            currentSelected = clicked;
        }
        public async void LoadData()
        {
            for (int i = 0; i < 174; i++)
            {
                CardData card = await AddressablesManager.Instance.LoadAssetAsync<CardData>($"{i+1}");
                switch (card.cardType)
                {
                    case CardType.Monter:
                        if (card.tribe == Tribe.God)
                        {
                            ItemSlotLibrary god = Instantiate(_godMonter, ScollMonter.transform);
                            god.ItemBase.cardData = card;
                            god.ItemBase.OnInit();
                            god.gameObject.SetActive(true);
                        }
                        else
                        {
                            ItemSlotLibrary nomal = Instantiate(_nomalMonter, ScollMonter.transform);
                            nomal.ItemBase.cardData = card;
                            nomal.ItemBase.OnInit();
                            nomal.gameObject.SetActive(true);
                        }
                        break;
                    case CardType.Spell:
                        ItemSlotLibrary spell = Instantiate(_spell, ScollSplell.transform);
                        spell.ItemBase.cardData = card;
                        spell.ItemBase.OnInit();
                        spell.gameObject.SetActive(true);
                        break;
                }
            }
        }

        public void OnShowMonter()
        {
            mainMonter.SetActive(true);
            mainSplell.SetActive(false);
            OnTabClicked(_listButtonShop[0]);
        }
        public void OnShowSpell()
        {
            mainMonter.SetActive(false);
            mainSplell.SetActive(true);
            OnTabClicked(_listButtonShop[1]);

        }

        public override void Init()
        {
            base.Init();
            buttonMonter.onClick.AddListener(OnShowMonter);
            buttonSplell.onClick.AddListener(OnShowSpell);
            mainMonter.SetActive(true);
            mainSplell.SetActive(false);
            LoadData();
            OnShowMonter();
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
            gameObject.SetActive(false);
        }

        public override void CloseMe()
        {
            base.CloseMe();
        }

        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }



        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}

