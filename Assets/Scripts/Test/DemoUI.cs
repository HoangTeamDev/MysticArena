using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUI : MonoBehaviour
{
    public UIAnimatorExtended animator;

    void Start()
    {
       
         

       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.MoveInFromLeft();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.MoveInFromRight();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.MoveInFromDown();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.MoveInFromUP();
        }
    }
    public void OnCloseButtonPressed()
    {
        animator.PlayPopOut();
    }
}
