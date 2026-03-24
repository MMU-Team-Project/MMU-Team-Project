using System.Collections;
using UnityEngine;
public class Bow : MonoBehaviour, IWeapon
{
    // Data Members
    private GameObject player;
    private Camera playerCam;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Animator bowAnim;
    [SerializeField] private GameObject attackPrefab;

    private float offCDShooting = 0f;
    [SerializeField] private float shootingRange = 30f;
    [SerializeField] private float shootingCD = 1f;
    [SerializeField] private float shootDmg = 30f;

    public void SetPlayer(GameObject newPlayer)
    {
        // Set player owner if there isn't already one
        if (player == null)
        {
            player = newPlayer;
        }
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
            if (Time.time < offCDShooting)
            {
                Debug.Log("Charging!");
                return;
            }

            bowAnim.SetTrigger("Shoot");
        }
    }

    public void Arrow()
    {
        // Shooting cooldown
        offCDShooting = Time.time + shootingCD;

        // Instantiate an arrow
        GameObject arrow = Instantiate(attackPrefab);

        rb = arrow.GetComponent<Rigidbody>();
        arrow.transform.position = transform.position;
        rb.linearVelocity = playerCam.transform.forward*shootingRange;

        // Elements borrowed from MagicMissle
        Arrow projectileScript = arrow.GetComponent<Arrow>();
        projectileScript.Setup(shootDmg,rb);

        // Make arrow disappear after some time
        StartCoroutine(arrowUpdate(arrow));
    }

    IEnumerator arrowUpdate(GameObject arrow)
    {
        yield return new WaitForSeconds(5f);
        Destroy(arrow);
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
