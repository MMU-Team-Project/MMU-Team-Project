using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Arrow : MonoBehaviour
{
    private float arrowDamage;
    private Camera playerCam;

    public void Setup(Camera newPlayerCam, float newArrowDmg)
    {
        playerCam = newPlayerCam;
        arrowDamage = newArrowDmg;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(arrowDamage);
                Destroy(gameObject);
            }
        }
    }
}
