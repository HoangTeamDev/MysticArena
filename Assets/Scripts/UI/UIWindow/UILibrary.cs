
using CardData;
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
      
        [Header("Main")]
        public List<Transform> _contents;
        public List<Button> Buttons;
        public Sprite _show;
        public Sprite _hide;
        public List<GameObject> _listpanel;
        public SpriteAtlas spriteAtlas;
        
        [Header("Item")]
        public ItemSlotLibrary _nomalMonter;
        public ItemSlotLibrary _godMonter;
        public ItemSlotLibrary _spell;
        public ItemSlotLibrary _trap;

        public bool isLoad;

      

        public override void Init()
        {
            base.Init();
            //LoadAllCard();
            isLoad=false;
            for(int i = 0; i < Buttons.Count; i++)
            {
                int x = i;
                Buttons[i].onClick.AddListener(() =>
                {
                   
                    OpenTab(x);
                });
            }
        }

        public void OpenTab(int index)
        {
            for(int i = 0;i< Buttons.Count; i++)
            {
                Image image = Buttons[i].GetComponent<Image>();
                if (image != null)
                {
                    if (i == index)
                    {
                        image.sprite = _show ;
                        _listpanel[i].gameObject.SetActive(true);

                    }
                    else
                    {
                        image.sprite = _hide;
                        _listpanel[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        private void LoadAllCard()
        {
            foreach(var item in GameData.Instance._allCard)
            {
                if(item._CardType is 1)
                {
                    CreateMonster(item);
                }else if(item._CardType is 2)
                {
                    CreateSpell(item);
                }else
                {
                    CreateTrap(item);
                }

            }
            isLoad=true;
        }
        private void CreateMonster(Card card)
        {
            
            if (card._Rarity == "GR")
            {
                ItemSlotLibrary itemSlotLibrary = Instantiate(_nomalMonter, _contents[0]);
                itemSlotLibrary.card = card;

                itemSlotLibrary.Init();
                itemSlotLibrary.gameObject.SetActive(true);
            }
            else
            {
                ItemSlotLibrary itemSlotLibrary = Instantiate(_nomalMonter, _contents[0]);               
                itemSlotLibrary.card = card;
             
                itemSlotLibrary.Init();
                itemSlotLibrary.gameObject.SetActive(true);

            }

        }
           
        private void CreateSpell(Card card)
        {
            ItemSlotLibrary itemSlotLibrary = Instantiate(_spell, _contents[1]);
        
            itemSlotLibrary.card = card;
            itemSlotLibrary.Init();
            itemSlotLibrary.gameObject.SetActive(true);
        }
        private void CreateTrap(Card card)
        {
            ItemSlotLibrary itemSlotLibrary = Instantiate(_trap, _contents[2]);

            itemSlotLibrary.card = card;
            itemSlotLibrary.Init();
            itemSlotLibrary.gameObject.SetActive(true);
        }
        public override void Open()
        {
            base.Open();
            if (!isLoad)
            {
                LoadAllCard();
            }
            OpenTab(0);
           
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

