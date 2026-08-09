using Menu.Connet;
using RoomAll;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UI.SystemUI;
using UI.UIWindow;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIOvelay
{
    public enum ConfirmType
    {
       Attatck
    }
    public class UIConfirm : UIBase
    {
        UIMainField uIMainField => UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
        //monster
        [Title("Monster")]
        public List<Button> _selectMonster;
        public List<Button> _selectTrap;
        public GameObject _selectTrapObj;
        public GameObject _selectMonsterObj;
        public ConfirmType _confirmType;
        public int _idSelected;
        public int _idAttacker;
        public override void Close()
        {
            base.Close();
        }

        public override void CloseMe()
        {
            base.CloseMe();
            ResetAll();
        }

        public override void Init()
        {
            base.Init();
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
        public void SetConfirmType(ConfirmType type)
        {
            _confirmType= type;
            switch (type)
            {
                case ConfirmType.Attatck:
                    SetMonsterAttack();
                    break;
                default:
                    break;
            }
        }
        public void SetMonsterAttack()
        {
            ResetAll();

            _selectMonsterObj.SetActive(true);

            foreach (Button button in _selectMonster)
            {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
            }

            for (int i = 0; i < uIMainField.monsterZoneOther.Count; i++)
            {
                Transform zone = uIMainField.monsterZoneOther[i];

                if (zone.childCount <= 0)
                    continue;

                Button button = _selectMonster[i];

                button.gameObject.SetActive(true);

                button.onClick.AddListener(() =>
                {
                    // Lấy card ngay tại zone này
                    CardIntance cardIntance =zone.GetComponentInChildren<CardIntance>();

                    if (cardIntance != null)
                    {
                        _idSelected = (int)cardIntance.InstanceId;
                        SendToServer();
                        
                    }

                    CloseMe();
                });
            }
        }
        private void ResetAll()
        {
            _selectMonsterObj.SetActive(false);
            _selectTrapObj.SetActive(false);
            foreach (Button button in _selectMonster)
            {
                button.gameObject.SetActive(false);
            }
            foreach (Button button in _selectTrap)
            {
                button.gameObject.SetActive(false);
            }
        }
        public void SendToServer()
        {
            switch(_confirmType)
            {
                case ConfirmType.Attatck:
                    ClientMain.Instance.SendAttack(_idAttacker, _idSelected);
                    break;
                default:
                    break;
            }
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
    
