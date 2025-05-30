using System.Collections;
using System.Collections.Generic;
using System.Text;
using UI.ItemUI;
using UI.SystemUI;
using UI.UIWindow;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIHUD
{
    public class PlayerHUD : UIBase
    {
        [Header("Function")]
        public Button _Library;
        public override void Init()
        {
            base.Init();
            _Library.onClick.AddListener(() =>
            {
                Library library = UIController.Instance.Get<Library>(WindowType.Library);
                if (library != null )
                {
                    library.OpenMe();
                }
            });
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

        public override void ShowInfoItem(StringBuilder sb, Item item)
        {
            base.ShowInfoItem(sb, item);
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
        public override void Close()
        {
            base.Close();
        }

        public override void CloseMe()
        {
            base.CloseMe();
        }

    }
}

