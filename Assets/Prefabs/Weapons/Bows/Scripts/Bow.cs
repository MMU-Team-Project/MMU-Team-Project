using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    // Data Members
    private GameObject player;
    private Camera playerCam;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Animator bowAnim;
    [SerializeField] private GameObject attackPrefab;

    private float offCDShooting = 0f;
    [SerializeField] private float shootingCD = 2f;
    [SerializeField] private float shootDmg = 50;

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

            bowAnim.SetTrigger("Shoot!");
        }
    }

    public void Arrow()
    {
        offCDShooting = Time.time + shootingCD; //Puts cast on cooldown

        GameObject arrow = Instantiate(attackPrefab);

        arrow.transform.position = player.transform.position + playerCam.transform.forward * 2;
        arrow.transform.rotation = playerCam.transform.rotation;

        MagicMissile projectileScript = arrow.GetComponent<MagicMissile>();
        projectileScript.Setup(playerCam,shootDmg); //Starts script for projectile handling
    }
}
