using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    // Data Members
    private GameObject player;
    private Camera playerCam;

    private KeyCode attack = KeyCode.Mouse0;
    private float charge;
    private float chargeMax = 100;
    private float chargeRate = 50;

    [SerializeField] private Rigidbody arrowModel;
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
            if (charge < chargeMax)
            {
                Debug.Log("Charging");
                return;
            }

            bowAnim.SetTrigger("Shoot");
        }
    }

    void Update()
    {
        if(Input.GetKey(attack) && charge < chargeMax)
        {
            charge += Time.deltaTime * chargeRate;
        }

        if(Input.GetKeyUp(attack))
        {
            Rigidbody arrow = Instantiate(arrowModel, player.transform.position, Quaternion.identity) as Rigidbody;
            arrow.AddForce(playerCam.transform.forward * charge, ForceMode.Impulse);
            charge = 0;
        }
    }
}
