using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [Header("Persistent UI")]
    public GameObject menuUI;
    [Header("Screen UI")]
    public GameObject buttonUI;
    [Header("PopUp UI")]
    public GameObject InfoCard;
    [Header("Dialog UI")]
    public GameObject InfoText;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void OpenScreen(GameObject screen)
    {
        CloseAllScreens();
        screen.SetActive(true);
    }
    public void CloseAllScreens()
    {
        
    }
    public void ShowCardInfo( int id)
    {
        
        InfoCard.SetActive(true);
        InfoCard infoCard = InfoCard.GetComponent<InfoCard>();
        if (infoCard != null)
        {
            infoCard.Show(id);
        }
    }
    
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Dùng raycaster để lấy chính xác object bị nhấn
                PointerEventData pointerData = new PointerEventData(eventSystem);
                pointerData.position = Input.mousePosition;
               
                List<RaycastResult> results = new List<RaycastResult>();
                raycaster.Raycast(pointerData, results);
                Debug.Log(results.Count);
                if (results.Count > 0)
                {
                    GameObject hitUI = results[0].gameObject;
                   /* if(hitUI.tag== "panel")
                    {
                    hitUI.SetActive(false);

                    }*/
                }
            }
            else
            {
                Debug.Log("Chuột nhấn ra ngoài (không trúng UI nào).");
            }
        }
    }
}

