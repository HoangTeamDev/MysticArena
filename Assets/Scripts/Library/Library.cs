using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Library : MonoBehaviour
{
    [Header("Object")]
    public List<CardInventory> cardInventories;
    public GameObject main;
    [Header("Main")]
    public GameObject mainMonter;
    public GameObject ScollMonter;
    public GameObject mainSplell;
    public GameObject ScollSplell;
    public Button buttonMonter;
    public Button buttonSplell;
    void Start()
    {
        buttonMonter.onClick.AddListener(OnShowMonter);
        buttonSplell.onClick.AddListener(OnShowSpell);
        mainMonter.SetActive(true);
        mainSplell.SetActive(false);
        for (int i = 1; i <= 740; i++)
        {
            CardData card = Resources.Load<CardData>("Data/" + i);
            if (card !=null)
            {
                if (card.IsSpell)
                {
                    CardInventory newo = Instantiate(cardInventories[0], ScollSplell.transform);
                    newo.image.sprite = Resources.Load<Sprite>("Sprite/Image/" + i);
                    newo.textMeshProUGUI.text = card.Name;
                    newo.idCard = card.ID;
                    newo.gameObject.SetActive(false);
                }
                else
                {
                    if (card.Race == RaceType.God)
                    {
                        CardInventory newo = Instantiate(cardInventories[1], ScollMonter.transform);
                        newo.image.sprite = Resources.Load<Sprite>("Sprite/Image/" + i);
                        newo.textMeshProUGUI.text = card.Name;
                        newo.idCard = card.ID;
                        newo.gameObject.SetActive(false);
                    }
                    else
                    {
                        CardInventory newo = Instantiate(cardInventories[2], ScollMonter.transform);
                        newo.image.sprite = Resources.Load<Sprite>("Sprite/Image/" + i);
                        newo.textMeshProUGUI.text = card.Name;
                        newo.idCard = card.ID;
                        newo.gameObject.SetActive(false);
                    }
                }
            }
        }
        OnShowMonter();
        EventSystem.current.SetSelectedGameObject(buttonMonter.gameObject);
    }

    public void OnShowMonter()
    {
        mainMonter.SetActive(true);
        mainSplell.SetActive(false);
        foreach (Transform child in ScollMonter.transform.Cast<Transform>())
        {
            child.gameObject.SetActive(true);
        }
    }
    public void OnShowSpell()
    {
        mainMonter.SetActive(false);
        mainSplell.SetActive(true);
        foreach (Transform child in ScollSplell.transform.Cast<Transform>())
        {
            child.gameObject.SetActive(true);
        }
    }
}
