using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            if (player.TryGetComponent<UnitLife>(out UnitLife unitLife))
            {
                slider.maxValue = unitLife.MaxHealth;
            }
        }
        else
        {
            slider.maxValue = 1f;
        }
        slider.value = slider.maxValue;
    }

    public void DecreaseSliderValue(float delta) // delta --> the ammount used to decrease value
    {
        slider.value -= delta;
    }
}
