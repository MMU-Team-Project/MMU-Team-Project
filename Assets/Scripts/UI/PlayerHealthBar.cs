using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public float Health, MaxHealth, Width, Height;

    [SerializeField]
    private RectTransform healthBar;


    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }
    
    public void SetHealth(float health)
    {
        Health = health;
        float newWidth = (Health / MaxHealth) * Width; //based off CurrentHealth's width/height to allow for border on UI

        healthBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
