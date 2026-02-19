using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    private float homingForce = 20f;
    private float maxSpeed = 100f;
    private float damping = 1f;
    private float missileDmg;

    private float targetDistance = 1f;
    private float targetSpeed = 3f;

    private Rigidbody rb;
    private Camera playerCam;
    private Vector3 homingTarget;


    public void Setup(Camera newPlayerCam, float newMissileDmg)
    {
        playerCam = newPlayerCam;
        rb = transform.GetComponent<Rigidbody>();
        homingTarget = playerCam.transform.position + playerCam.transform.forward * targetDistance;
        missileDmg = newMissileDmg;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(missileDmg);
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (playerCam != null && rb != null)
        {
            //Moves the homingTarget away from the player over time
            targetDistance += targetSpeed * Time.fixedDeltaTime;
            homingTarget = playerCam.transform.position + playerCam.transform.forward * targetDistance;

            //Adds force toward the homingTarget
            Vector3 toTarget = homingTarget - transform.position;
            rb.AddForce(toTarget.normalized * homingForce, ForceMode.Acceleration);

            //Prevents ridiculous unbound speeds
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }

            //Slows the missile as it gets toward its goal
            Vector3 dampingForce = -rb.linearVelocity * damping;
            rb.AddForce(dampingForce, ForceMode.Acceleration);

            if (targetDistance > 100)
            {
                Destroy(gameObject);
            }
        }
    }
}
