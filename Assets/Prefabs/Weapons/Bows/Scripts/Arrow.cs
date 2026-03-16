using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float arrowDamage;
    private Rigidbody rb;
    private Camera playerCam;

    public void Setup(Camera newPlayerCam, float newArrowDmg)
    {
        playerCam = newPlayerCam;
        rb = transform.GetComponent<Rigidbody>();
        arrowDamage = newArrowDmg;
    }

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
