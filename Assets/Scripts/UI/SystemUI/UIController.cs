using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI.SystemUI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        public static bool HasInstance => Instance != null;
    }
}

