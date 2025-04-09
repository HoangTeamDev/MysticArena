using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Menu.Connet
{
    public partial class GameServer : MonoBehaviourPun

    {
        public static GameServer Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;

        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                PlayCard("Rồng Thần");
            }
        }
    }

}
