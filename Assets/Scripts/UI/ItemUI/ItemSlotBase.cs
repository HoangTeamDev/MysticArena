
using Card;
using Menu.Connet;
using UI.SystemUI;
using UI.UIOvelay;
using UI.UIWindow;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.ItemUI
{
    public enum TypeItemSlot { Inventory, Equiment, Upgrade, Skill, Shop, Library }

    public abstract class ItemSlotBase : MonoBehaviour, IPointerClickHandler
    {
        public GameObject _vfx;
        public RectTransform rectTransform;
        public Button Button;       
        public ItemBase ItemBase;     
        public TypeItemSlot typeItemSlot;
        public UIController uIManager => UIController.Instance;
        private void Awake()
        {
            //Button.onClick.AddListener(Onselect);
        }
        public virtual void OnDrop(PointerEventData eventData)
        {
            

        }
        public virtual void Onselect()
        {
            SeletecMe();
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SeletecMe();
            }
            
        }


        public void OnPointerExit(PointerEventData eventData)
        {

        }
        public virtual void OnDeselect(BaseEventData eventData)
        {
            //UIManager.Instance.SelectUI(null);

        }
        public virtual void OnSelect(BaseEventData baseEventData)
        {
            //SeletecMe();
        }
        public virtual async void SeletecMe()
        {
           
            
             

            
        }
    }
}

