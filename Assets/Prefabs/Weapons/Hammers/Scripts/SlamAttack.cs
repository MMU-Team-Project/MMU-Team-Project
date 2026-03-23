using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlamAttack : MonoBehaviour
{
    [SerializeField] Vector3 startSize = Vector3.one;
    [SerializeField] Vector3 endSize = Vector3.one;
    [SerializeField] float growTime = 1f;

    private float FXprogress = 0f;
    private float hammerDmg = 0;
    private List<Collider> hitEnemies = new List<Collider>();

    public void Setup(float dmg)
    {
        hammerDmg = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        FXprogress += Time.deltaTime / growTime;

        transform.localScale = Vector3.Lerp(startSize, endSize, FXprogress);

        if (FXprogress >= growTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Enemy"))
        {
            if (hitEnemies.Contains(target) == false)
            {
                EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    hitEnemies.Add(target);
                    enemyHealth.TakeDamage(hammerDmg);
                }
            }
        }
    }
}
