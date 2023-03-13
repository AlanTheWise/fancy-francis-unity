using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    public Slider slider;
    public Color low, high;
    public Vector3 offset;

    public void SetHealth(int currentHealth, int maxHealth){
        slider.gameObject.SetActive(true);
        slider.value = (float) currentHealth;
        slider.maxValue = (float) maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = high;
    }

    void Update(){
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
