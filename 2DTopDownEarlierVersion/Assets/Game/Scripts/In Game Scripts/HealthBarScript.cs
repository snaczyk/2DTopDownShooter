using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class HealthBarScript : MonoBehaviour
{

    public Slider Slider;
    public Gradient Gradient;

    [SerializeField] private Image fill;

    public void SetMaxHealth(int health)
    {
        Slider.maxValue = health;
        Slider.value = health;

        fill.color = Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        Slider.value = health;

        fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
