using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteBasedOnDirection : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {

        Vector2 mouseDir = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var localScale = transform.localScale;
        if (mouseDir.x >= 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        else
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        transform.localScale = localScale;
    }
}
