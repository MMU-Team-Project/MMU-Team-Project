using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float enemyHealth;

    [SerializeField]
    private float difficultyMultiplier; //how much the health is multiplied by difficulty

    [SerializeField]
    private GameObject manager;


    void Awake()
    {
        manager = GameObject.FindWithTag("DifficultyManager");
        difficultyMultiplier = 1 + manager.GetComponent<DifficultyManager>().difficultyLevel/10;
        enemyHealth *= difficultyMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("h"))
        {
            TakeDamage(50);  //temporary damage dealer to test damage script
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
