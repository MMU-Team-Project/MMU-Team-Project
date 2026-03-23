using UnityEngine;

public class Hammer : MonoBehaviour, IWeapon
{
    private GameObject player;
    [SerializeField] private float hammerDmg = 50;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Animator hammerAnim;

    [SerializeField] private float slammerCD = 2f;
    private float offCDSlammer = 0f;

    public void SetPlayer(GameObject newPlayer)
    {
        if (player == null)
        {
            player = newPlayer;
        }
        //Set player owner if there isn't already one
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Equip(GameObject newPlayer)
    {
        SetPlayer(newPlayer);
        transform.SetParent(player.transform, false);
        transform.localPosition = offset;
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void Use()
    {
        if (player != null)
        {
            if (Time.time < offCDSlammer)
            {
                Debug.Log("Cooldown!");
                return;
            }
            offCDSlammer = Time.time + slammerCD;
            hammerAnim.SetTrigger("Slam");
        }
    }

    public void Slammer()
    {
        GameObject slamAttack = Instantiate(attackPrefab);
        slamAttack.transform.position = player.transform.position + player.transform.forward * 3;
        slamAttack.transform.rotation = player.transform.rotation;

        SlamAttack slamScript = slamAttack.GetComponent<SlamAttack>();
        slamScript.Setup(hammerDmg);
    }
}
