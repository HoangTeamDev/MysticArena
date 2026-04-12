using CardData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RoomAll
{
    public enum ZoneType
    {
        Deck,
        Hand,
        Monster,
        SpellTrap,
        Graveyard,
        Banished
    }
    public enum PhaseType
    {
        Start,
        Draw,
        Main,
        End
    }
    [System.Serializable]
    public class CardIntance: MonoBehaviour
    {
        public Card Card;
        public long InstanceId;
        public int CardId;

        public int OwnerPlayerId;
        public int ControllerPlayerId;

        public ZoneType CurrentZone;
        public int SlotIndex;

        public bool IsFaceUp;
        public int CurrentAtk;
        public int CurrentHp;
        [Header("thuoctinh")]
        [Header("Monster")]
        public Image imageMonter;
        public Image element;
        public Image Rate;
        public Image BackGroundRate;
        public List<Image> imagesLevel;
        public TextMeshProUGUI nameMonter;

        public TextMeshProUGUI ATK;
        public TextMeshProUGUI HP;
        [Header("Spell")]
        public Image imageSpell;
        public TextMeshProUGUI nameSpell;
        public GameObject spellCard;
        public Image RateSpell;
        public Image BackGroundRateSpell;
        [Header("Trap")]
        public Image imageTrap;
        public TextMeshProUGUI nameTrap;
        public GameObject trapCard;
        public Image RateTrap;
        public Image BackGroundRateTrap;

        public async void Init()
        {
            switch (this.Card._CardType)
            {
                case 1:
                    {
                        nameMonter.text = this.Card._Name;
                        ATK.text = this.Card._Attack.ToString();
                        HP.text = this.Card._Hp.ToString();
                        for (int i = 0; i < this.Card._Level; i++)
                        {
                            imagesLevel[i].gameObject.SetActive(true);
                        }
                        element.sprite = await GameData.Instance.LoadAsset<Sprite>("E" + this.Card._Element);
                        imageMonter.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());
                        Rate.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{this.Card.GetRate()}");
                        BackGroundRate.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{this.Card.GetRate()}"); ;
                    }
                    break;
                case 2:
                    {
                        nameSpell.text = this.Card._Name;
                        RateSpell.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{this.Card.GetRate()}");
                        BackGroundRateSpell.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{this.Card.GetRate()}");
                    }
                    break;
                case 3:
                    {
                        nameTrap.text = this.Card._Name;
                        RateTrap.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{this.Card.GetRate()}");
                        BackGroundRateTrap.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{this.Card.GetRate()}");
                    }
                    break;
            }
        }
    }
}

