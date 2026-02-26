using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    // Data Members
    private GameObject player;
    private float offCDShoot = 0f;
    private Camera playerCam;
    [SerializeField] private Animator bowAnim;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;

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
            if (Time.time < offCDShoot)
            {
                Debug.Log("Cooldown!");
                return;
            }

            bowAnim.SetTrigger("Shoot");
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
