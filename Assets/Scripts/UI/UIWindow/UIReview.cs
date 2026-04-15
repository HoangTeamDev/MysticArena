using CardData;
using System.Collections.Generic;
using UI.SystemUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.UIWindow
{
    public class UIReview : UIBase
    {
        public RectTransform panel;
        public Transform listButton;
        public List<ButtonConfirm> buttonConfirms = new List<ButtonConfirm>();
        public ButtonConfirm confirmButton;
        public Canvas canvas;
        public override void Init()
        {
            base.Init();
        }
        private void Update()
        {
           /* if (Input.GetMouseButton(0))
            {
                Vector2 mousePos;

                // Convert từ screen -> UI
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    Input.mousePosition,
                    canvas.worldCamera,
                    out mousePos
                );

                
            }*/
        }
        public ButtonConfirm CreateButtonconfirm()
        {
            var btn = Instantiate(confirmButton, listButton.transform);
            buttonConfirms.Add(btn);
            btn.transform.SetParent(listButton.transform, false);
            btn.gameObject.SetActive(true);
            return btn;
        }
        public void SetReview(int id)
        {
           
            OpenMe();
        }
        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }

        public override void Open()
        {
            base.Open();
            Vector3 vector = UIController.Instance._itemslotcurrent.GetComponent<RectTransform>().position;
            panel.position = new Vector3(vector.x,vector.y+200,vector.z);
        }

        public override void OpenMe()
        {
            base.OpenMe();
        }
        public override void Close()
        {
            base.Close();
            foreach(var data in buttonConfirms)
            {
                Destroy(data.gameObject);
            }
            buttonConfirms.Clear();
        }

        public override void CloseMe()
        {
            base.CloseMe();
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
    }
}

