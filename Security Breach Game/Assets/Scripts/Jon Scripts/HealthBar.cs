using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HealthBar : UnityEngine.MonoBehaviour
{
    public Slider slider;

    public UnityEngine.Gradient grad;

    public Image fill;

    public int priorityLevel = 0;

    private void Update()
    {
        
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = grad.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        slider.value -= health;

        fill.color = grad.Evaluate(slider.normalizedValue);

        if (slider.value <= 0)
        {
            gameObject.SetActive(false); //It is now dead.
        }
    }
}
