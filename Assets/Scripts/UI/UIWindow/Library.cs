using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.U2D;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Menu.Card;
using UI.SystemUI;
using System.Text;
using UnityEditor;
using Menu.System;
using UI.ItemUI;

namespace UI.UIWindow
{
    public class Library : UIBase
    {
        [Header("Object")]
        public List<CardInventory> cardInventories;
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
            for (int i = 1; i <= 740; i++)
            {
                CardData card = Resources.Load<CardData>("Data/" + i);
                if (card != null)
                {
                    if (card.IsSpell)
                    {
                        CardInventory newo = Instantiate(cardInventories[0], ScollSplell.transform);
                        newo.image.sprite = spriteAtlas.GetSprite(i.ToString());
                        newo.textMeshProUGUI.text = card.Name;
                        newo.ID = card.ID;
                        newo.gameObject.SetActive(true);
                        newo.OnInit();
                    }
                    else
                    {
                        if (card.Race == RaceType.God)
                        {
                            CardInventory newo = Instantiate(cardInventories[1], ScollMonter.transform);
                            newo.image.sprite = spriteAtlas.GetSprite(i.ToString());
                            newo.textMeshProUGUI.text = card.Name;
                            newo.ID = card.ID;
                            newo.gameObject.SetActive(true);
                            newo.OnInit();
                        }
                        else
                        {
                            CardInventory newo = Instantiate(cardInventories[2], ScollMonter.transform);
                            newo.image.sprite = spriteAtlas.GetSprite(i.ToString());
                            newo.textMeshProUGUI.text = card.Name;
                            newo.ID = card.ID;
                            newo.gameObject.SetActive(true);
                            newo.OnInit();
                        }
                    }
                }
            }
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

        public override void ShowInfoItem(StringBuilder sb, Item item)
        {
            base.ShowInfoItem(sb, item);
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

