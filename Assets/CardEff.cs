using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEff : MonoBehaviour
{
    public Outline outline;
    private float speed = 50f; // Tốc độ dao động
    public float minEffect = 10f;
    public float maxEffect = 15f;
    public Color ColorAction = new Color(0f, 0.95f, 0f);
    public Color ColorIdle= new Color(0.45f, 0.2f, 0.7f);
    void Update()
    {
       
        float glowSize = Mathf.PingPong(Time.time * speed, maxEffect - minEffect) + minEffect;

       
        outline.effectDistance = new Vector2(glowSize, glowSize);
    }
    public void ActiveOutline(bool value)
    {
        outline.enabled = value;
    }
    public void ChangeColor(Color color)
    {
        outline.effectColor= color;
    }
}
