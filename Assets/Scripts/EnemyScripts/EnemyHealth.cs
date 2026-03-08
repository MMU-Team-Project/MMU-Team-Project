using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float maxHealth; //when changeing health values make sure you change Max health

    [SerializeField]
    private float difficultyMultiplier; //how much the health is multiplied by difficulty

    [SerializeField]
    private GameObject manager;

    [SerializeField]
    private HealthBarUI healthBar;

    [SerializeField]
    private GameObject itemPlaceHolder;

    [SerializeField]
    public float Damage;

    void Awake()
    {
        manager = GameObject.FindWithTag("DifficultyManager");
        difficultyMultiplier = 1 + manager.GetComponent<DifficultyManager>().difficultyLevel / 10;
        maxHealth *= difficultyMultiplier;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            TakeDamage(50);  //temporary damage dealer to test damage script
        }
        else if (Input.GetKeyDown("k"))
        {
            TakeDamage(-20);
        }
    }

    public void TakeDamage(float damage) //might make this change health instead, in case we want to incorporate healers?
    { //to implement, the healers would need to do negative damage
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth); //health cant go above max

        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("enemy killed");

        RollDropTable();

        Destroy(gameObject); //will add list to select item to spawn from later
    }

    public void RollDropTable()
    {
        int roll = Random.Range(0, 2);

        if (roll == 0)
        {
            Instantiate(itemPlaceHolder, transform.position, Quaternion.identity);
        }

        //foreach item in droptable check if roll is that number then spawn that item
        //when list added, will make a preliminary roll to check if an item is dropped
        //e.g. enemy drops an item 25% of the time, and then the drop table is rolled to see what reward is given?
    }
}
