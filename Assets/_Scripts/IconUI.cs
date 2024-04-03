using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUI : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowIcon(bool flag)
    {
        anim.SetBool("isShowing", flag);
    }
}
