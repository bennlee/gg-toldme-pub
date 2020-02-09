using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{

    //    public List<Button> buttons;


    //    private void FixedUpdate()
    //    {
    //        if(Input.GetButtonDown(string Monster1)
    //        { 

    //        }
    //    }
    //}



    //[System.Serializable]
    //public class Button
    //{
    //    public float cooldown;
    //    public Image buttonIcon;
    //    [HideInInspector]
    //    public float currentCoolDown;
    //}

    public float coolDown;
    public Image buttonIcon;
    public float currentCoolDown = Mathf.Infinity;


    void Update()
    {
        if(currentCoolDown < coolDown)
        {
            currentCoolDown += Time.deltaTime;
            buttonIcon.fillAmount = currentCoolDown / coolDown;
        }
    }

    public void ButtonOn()
    {
        if(currentCoolDown >= coolDown)
        {
            currentCoolDown = 0;
        }
        Debug.Log("cool reset!");
    }

}