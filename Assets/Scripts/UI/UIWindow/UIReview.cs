using CardData;
using UI.SystemUI;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.UIWindow
{
    public class UIReview : UIBase
    {
        public Transform monstercard;
        public Transform spellcard;
        public Transform trapcard;
        public Canvas canvas;
        public override void Init()
        {
            base.Init();
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos;

                // Convert từ screen -> UI
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    Input.mousePosition,
                    canvas.worldCamera,
                    out mousePos
                );

                monstercard.localPosition = mousePos;
            }
        }
        public void SetReview(int id)
        {
            Card card= GameData.Instance.GetCardByID(id);
            if(card != null)
            {
                monstercard.gameObject.SetActive(true);
            }
            UIController.Instance.SetSelectUICurrent(monstercard.gameObject);
            OpenMe();
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
        public override void Close()
        {
            base.Close();
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

