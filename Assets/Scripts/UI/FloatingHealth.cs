using UnityEngine;
using UnityEngine.UI;

public class FloatingHealth : MonoBehaviour
{

    public float Health, MaxHealth, Width, Height;

    [SerializeField]
    private RectTransform healthBar;

    [SerializeField]
    private Canvas canvas;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

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

    void Update()
    {
        var toCam = canvas.transform.position - cam.transform.position;
        canvas.transform.rotation = Quaternion.LookRotation(toCam, Vector3.up);
    }
}
