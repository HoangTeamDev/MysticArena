
using CardData;
using DG.Tweening;
using Menu.System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UI.ItemUI;
using UI.SystemUI;
using UIScripts.SystemUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIOvelay
{
    public class UIInfoCard : UIBase
    {
        [Header("Monter")]     
        public Image imageMonter;     
        public Image element;
        public Image Rate;
        public List<Image> imagesLevel;
        public TextMeshProUGUI nameMonter;
       
        public TextMeshProUGUI ATK;
        public TextMeshProUGUI HP;
        public GameObject montercard;
       
        [Header("Spell")]
        public Image imageSpell;
        public TextMeshProUGUI nameSpell;
        public GameObject spellCard;
        public Image RateSpell;
        [Header("Trap")]
        public Image imageTrap;
        public TextMeshProUGUI nameTrap;
        public GameObject trapCard;
        public Image RateTrap;
        [Header("Ability")]
        public TextMeshProUGUI info1;
        public TextMeshProUGUI info2;        
        public RectTransform Sollview;
        public RectTransform main;
        public List<SkillDes> skillDes;
        public SkillDes skillDesper;
      
        
        public static string ColorWrap(string text, string colorHex) => $"<color={colorHex}>{text}</color>";
        public async void ShowItem(Card card)
        {
            if (card._CardType is 1)
            {
                nameMonter.text = card._Name;
                ATK.text = card._Attack.ToString();
                HP.text = card._Hp.ToString();
                for (int i = 0; i < card._Level; i++)
                {
                    imagesLevel[i].gameObject.SetActive(true);
                }
                element.sprite = await GameData.Instance.LoadAsset<Sprite>("E" + card._Element);
                imageMonter.sprite = await GameData.Instance.LoadAsset<Sprite>(card._CardId.ToString());
                Rate.sprite = await GameData.Instance.LoadAsset<Sprite>(card._Rarity);
                montercard.gameObject.SetActive(true);
                spellCard.gameObject.SetActive(false);
                trapCard.gameObject.SetActive(false);
                info1.text = $"Tộc: {ColorWrap( card.GetRace(), ReadColor.Gold)} - Từ Khóa: {ColorWrap(card.GetKeyWord(), ReadColor.Gold)}";
            }
            if (card._CardType is 2)
            {
                nameSpell.text = card._Name;
                RateSpell.sprite = await GameData.Instance.LoadAsset<Sprite>(card._Rarity);
                montercard.gameObject.SetActive(false);
                spellCard.gameObject.SetActive(true);
                trapCard.gameObject.SetActive(false);
                info1.text = $"Từ Khóa: {ColorWrap(card.GetKeyWord(), ReadColor.Gold)}";
            }
            if (card._CardType is 3)
            {
                nameTrap.text = card._Name;
                RateTrap.sprite = await GameData.Instance.LoadAsset<Sprite>(card._Rarity);
                montercard.gameObject.SetActive(false);
                spellCard.gameObject.SetActive(false);
                trapCard.gameObject.SetActive(true);
                info1.text = $"Từ Khóa: {ColorWrap(card.GetKeyWord(),ReadColor.Gold)}";
            }
            foreach (var item in card.CardEffects)
            {
                var  skill=Instantiate(skillDesper, Sollview);
                skill.gameObject.SetActive(true);
                skillDes.Add(skill);
                skill._nameSkill.text = $"{ColorWrap(item._Skillname, "#FFD700")}\n {ColorWrap(item._Des,ReadColor.White)}";
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(Sollview);
        }
        public override void Init()
        {
            base.Init();
        }

        public override void Open()
        {
            base.Open();
            LayoutRebuilder.ForceRebuildLayoutImmediate(Sollview);
        }

        public override void OpenMe()
        {
            base.OpenMe();
        }

        public override void Close()
        {
            base.Close();
            gameObject.SetActive(false);
            foreach (var item in skillDes)
            {
                Destroy(item.gameObject);
            }
                skillDes.Clear();
            foreach (var item in imagesLevel)
            {
                item.gameObject.SetActive(false);
            }
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
            
            if (main != null)
            {
                Vector3 origin = new Vector3(main.localPosition.x, -(Screen.height / 2 + main.sizeDelta.y / 2));

                main.localPosition = new Vector3(main.localPosition.x, -(Screen.height / 2 + main.sizeDelta.y / 2));
                main.DOLocalMoveY(0f, 0.2f).SetEase(Ease.Linear);
            }
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
