using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class StringEvent : UnityEvent<string> { }
public class DemoUI : MonoBehaviour
{
    public List <UIAnimatorExtended> animator;
    public StringEvent StringEvent;
    void Start()
    {

        StringEvent.AddListener(okela);

       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (var anim in animator)
            {
                anim.Callname?.Invoke();
                StringEvent.Invoke("okkkkkk");
                anim.StringEvent.Invoke("Đa dc goi nay");
            }
        }
       
    }
    public void okela(string b)
    {
        MainLog.Log("OKela", b, ReadColor.Lime);
    }
    
}
