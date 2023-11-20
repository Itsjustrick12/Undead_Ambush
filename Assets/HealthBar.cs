using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform colorFill;
    [SerializeField] private RectTransform healthBar;

    private float maxWidth = 128;
    private float currentWidth = 128;

    private void Start()
    {
        maxWidth = healthBar.rect.width;
    }

    public void updateBar(float health, float maxHealth)
    {
        float ratio = health / maxHealth;

        currentWidth = maxWidth * ratio;

        colorFill.sizeDelta = new Vector2(currentWidth, healthBar.rect.height);
    }
}
