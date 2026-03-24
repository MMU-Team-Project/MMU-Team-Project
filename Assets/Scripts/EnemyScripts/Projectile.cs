using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject player;
    public float damage;

    [SerializeField]
    private float projectileLifetime;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        CancelInvoke(nameof(Disable));
        Invoke(nameof(Disable), projectileLifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        
        Disable();
    }

    private void Disable()
    {
        CancelInvoke(nameof(Disable));
        Destroy(gameObject);
    }
}
