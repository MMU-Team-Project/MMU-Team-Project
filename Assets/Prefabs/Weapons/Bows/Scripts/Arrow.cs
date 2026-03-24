using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    private float arrowDamage;
    private float arrowSpeed = 5f;
    private Rigidbody rigidBody;

    public void Setup(float newArrowDmg, Rigidbody rb)
    {
        arrowDamage = newArrowDmg;
        rigidBody = rb;
    }

    private void FixedUpdate()
    {
        // Apply Vector3 rotation variables from the arrows' rigidbody velocity's constant changes of rotations into this data member
        transform.rotation = Quaternion.LookRotation(rigidBody.linearVelocity);
        // Add force to the rigidbody element via the data member for speed
        rigidBody.AddForce(arrowSpeed*rigidBody.linearVelocity);
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
