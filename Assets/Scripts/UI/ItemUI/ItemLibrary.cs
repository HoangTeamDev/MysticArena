using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI.ItemUI
{
    public class ItemLibrary : ItemBase
    {
        public TextMeshProUGUI _nameCard;
        public override void OnInit()
        {
            base.OnInit();
            _nameCard.text = cardData.nameCard;
        }
    }
}
  
