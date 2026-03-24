using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float maxHealth; //when changeing health values make sure you change Max health

    [SerializeField]
    private PlayerHealthBar healthBar;

    void Awake()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth); //health cant go above max

        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Debug.Log("You have died");
        }
    }
}
