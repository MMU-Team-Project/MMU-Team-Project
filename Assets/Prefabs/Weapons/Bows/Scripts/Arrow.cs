using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int arrowDamage = 20;

    private void OnTriggerEnter(Collider target)
    {
        if(target.GetComponent<EnemyHealth>())
        {
            EnemyHealth health = target.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(arrowDamage);
            }
        }
    }
}
