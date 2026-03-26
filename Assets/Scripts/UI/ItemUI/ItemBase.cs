
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI.ItemUI
{
    public enum TypeItem { Inventory, Equipment, Upgrade, Skill, Shop, Library }
    public class ItemBase : MonoBehaviour
    {
       
        public TypeItem typeItem;
        public RectTransform rectTransform;
        public Image _icon;
        public virtual void OnInit()
        {
            rectTransform = GetComponent<RectTransform>();
            
            if (_icon.sprite == null)
            {
                _icon.sprite = Resources.Load<Sprite>("Sprite/Item/" + 7);
            }
        }
    }
}

