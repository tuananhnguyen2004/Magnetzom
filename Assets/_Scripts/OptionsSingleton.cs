using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSingleton : MonoBehaviour
{
    static OptionsSingleton _instance;

    public static OptionsSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OptionsSingleton>();
            }
            return _instance;
        }
    }

    private CanvasGroup canvasGroup;

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowUI()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void HideUI()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
