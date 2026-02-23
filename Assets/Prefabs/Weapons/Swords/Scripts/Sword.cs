using UnityEngine;
public class Sword : MonoBehaviour, IWeapon
{
    private GameObject player;
    [SerializeField] private float swordDmg = 55;
    //[SerializeField] private 
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Animator swordAnim;
    [SerializeField] private float SwingCooldown = 1f;
    [SerializeField] private float DamageCooldown = 1f;
    private Camera playerCam;



    public void SetPlayer(GameObject newPlayer)
    {
        if (player == null)
        {
            player = newPlayer;
        }
        //Set player owner if there isn't already one
    }


    public void Equip(GameObject newPlayer)
    {
        SetPlayer(newPlayer);
        transform.SetParent(player.transform, false);
        transform.localPosition = offset;
        transform.localRotation = Quaternion.Euler(rotation);
        playerCam = player.transform.GetComponentInChildren<Camera>();
    }

    public void Use()
    {
        if (player != null)
        {
            if (Time.time < SwingCooldown)
            {
                Debug.Log("Cooldown!");
                return;
            }

            swordAnim.SetTrigger("Swing");
        }
    }

    public void Swing()
    {
        SwingCooldown = Time.time + SwingCooldown; //Puts attack on cooldown




        //GameObject magicMissile = Instantiate(attackPrefab);

        //magicMissile.transform.position = player.transform.position + playerCam.transform.forward * 2;
        //magicMissile.transform.rotation = playerCam.transform.rotation;

        //MagicMissile projectileScript = magicMissile.GetComponent<MagicMissile>();
        //projectileScript.Setup(playerCam, staffDmg); //Starts script for projectile handling
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //attempting to add damage cooldown - currently insta-kills since checks for collision every frame.
                //if (Time.time > DamageCooldown)
                //{
                //    Debug.Log("Cooldown!");
                //    return;
                //    enemyHealth.TakeDamage(swordDmg);
                //}
                enemyHealth.TakeDamage(swordDmg);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
