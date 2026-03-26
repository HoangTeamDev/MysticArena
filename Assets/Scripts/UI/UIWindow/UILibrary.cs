
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
        
        [Header("Item")]
        public ItemSlotLibrary _nomalMonter;
        public ItemSlotLibrary _godMonter;
        public ItemSlotLibrary _spell;
       
       

      

        public override void Init()
        {
            base.Init();
           
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

