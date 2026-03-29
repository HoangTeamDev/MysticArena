using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
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
        public Button _btnDeck;
        [Header("InforPlayer")]
        public Image _avatar;
        [SerializeField] private TextMeshProUGUI _namePlayer;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _gold;
        [SerializeField] private TextMeshProUGUI _diamond;
        public override void Init()
        {
            base.Init();
            _Library.onClick.AddListener(() =>
            {
                UILibrary library = UIController.Instance.Get<UILibrary>(WindowType.UI_Library);
                if (library != null )
                {
                    library.OpenMe();
                }
            });
            _btnDeck.onClick.AddListener(() =>
                {
                    UIDeck deck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                    if (deck != null)
                    {
                        deck.OpenMe();
                    }
                });
            SetInfo();
        }
        private void SetInfo()
        {
            var d=GameData.Instance._mainPlayer;
            _namePlayer.text = d._namePlayer;
            _level.text=$"Level:{d._level}";
            
            AnimateTextNumber(_gold, d._gold);
            AnimateTextNumber(_diamond, d._diamond);
           
        }
        private void AnimateTextNumber(TextMeshProUGUI textComponent, long newValue)
        {
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NumberGroupSeparator = ".";
            textComponent.text = newValue.ToString("N0", culture);
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

