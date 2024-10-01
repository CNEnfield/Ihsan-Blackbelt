using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;

    public GunSway gunsway;

    private bool isScoped = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
        }

        if(isScoped == true)
        {
            gunsway.enabled = false;
        }

        if(isScoped == false)
        {
            gunsway.enabled = true;
        }
    }

}

//Get a reference to the scripts you wish to turn off
//If isScoped == true scripts.enabled = false;
//if isScoped == false scripts.enabled = true;
