using System;
using UI.SystemUI;
using UI.UIOvelay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.Card
{
    public enum CardType { Monster, Spell }
    public enum Quality { R, SR, UR, GR }

    [Serializable]
    public abstract class Card: MonoBehaviour
    {
        public bool _canDrag;
        public CanvasGroup _canvasGroup;
        public CardData _data;
        public Button _button;
        [Header("info")]
        public int ID;
        public string Name;
        public string Description;
        public CardType Type;
        public KeyWords Keywords;
        public Transform mparent = null;
        public CardEff cardEff;
        
        public virtual void OnSelect(BaseEventData eventData)
        {
            /*InfoCard infoCard = UIController.Instance.Get<InfoCard>(WindowType.InfoCard);
            if (infoCard != null)
            {
                infoCard.Show(ID);
                infoCard.OpenMe();
            }*/
        }
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if ( _canDrag)
            {
                mparent = this.transform.parent;
                this.transform.SetParent(this.transform.parent.parent);
                _canvasGroup.blocksRaycasts = false;
            }
           
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_canDrag)
            {
                this.transform.SetParent(mparent);
                this.transform.position = mparent.position;
                _canvasGroup.blocksRaycasts = true;
                this.transform.localScale = Vector3.one;
                cardEff.ActiveOutline(true);
                cardEff.ChangeColor(cardEff.ColorIdle);
            }
           
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_canDrag)
            {
                this.transform.position = eventData.position;

            }

        }      
        public virtual void OnInit()
        {
            //cardEff.ActiveOutline(true);
            //cardEff.ChangeColor(cardEff.ColorAction);
        }
        protected virtual void OnEnable()
        {
            
        }
        protected virtual void OnDisable()
        {
            
        }
        protected virtual void OnDestroy()
        {
            
        }
        public virtual void ActivateEffect() { }
    }
}
