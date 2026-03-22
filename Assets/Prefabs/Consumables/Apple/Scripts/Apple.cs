using System.Collections;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class Apple : MonoBehaviour, IWeapon
{
    private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Animator appleAnim;
    private Camera playerCam;

    [SerializeField] private float eatCD = 0.5f;
    private float offCDEat = 0f;

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
        Debug.Log("Got here");
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
            if (Time.time < offCDEat)
            {
                Debug.Log("Cooldown!");
                return;
            }

            appleAnim.SetTrigger("Eating");
        }
    }

    public void Eat()
    {
        Player playerScript = player.GetComponent<Player>();
        playerScript.health += 10;
        Destroy(gameObject);
    }
}
