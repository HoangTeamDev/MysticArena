using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Menu.Card
{
    public class MonterShow : MonoBehaviour
    {
        public TextMeshProUGUI _name;
        public TextMeshProUGUI _level;
        public TextMeshProUGUI _Atk;
        public TextMeshProUGUI _HP;
        public Image _image;
        public void OnShow(int id, string name, int level, int atk, int hp)
        {
            _name.text = name;
            _level.text = level.ToString();
            _Atk.text = atk.ToString();
            _HP.text = hp.ToString();
            _image.sprite = Resources.Load<Sprite>("Sprite/Image/" + id);
        }
    }
}

