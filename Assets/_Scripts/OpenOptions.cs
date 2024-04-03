using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour
{
    public void OpenOptionsUI(bool flag)
    {
        if (flag)
        {
            OptionsSingleton.Instance.ShowUI();
        }
        else
        {
            OptionsSingleton.Instance.HideUI();
        }
    }
}
