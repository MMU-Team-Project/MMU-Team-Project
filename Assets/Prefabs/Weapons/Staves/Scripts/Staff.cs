using System.Collections;
using UnityEditor;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    private GameObject player;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Animator staffAnim;
    private Camera playerCam;

    [SerializeField] private float magicMissileCD = 0.5f;
    private float offCDMissile = 0f;

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
        playerCam = player.transform.GetComponentInChildren<Camera>();
    }

    public void Use()
    {
        if (player != null)
        {
            if (Time.time < offCDMissile)
            {
                Debug.Log("Cooldown!");
                return;
            }

            staffAnim.SetTrigger("Cast");
        }
    }

    public void MagicMissile()
    {
        offCDMissile = Time.time + magicMissileCD; //Puts cast on cooldown

        GameObject magicMissile = Instantiate(attackPrefab);

        magicMissile.transform.position = player.transform.position + playerCam.transform.forward * 2;
        magicMissile.transform.rotation = playerCam.transform.rotation;

        MagicMissile projectileScript = magicMissile.GetComponent<MagicMissile>();
        projectileScript.Setup(playerCam); //Starts script for projectile handling
    }
}
