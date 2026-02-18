using UnityEditor;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    private GameObject player;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;


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
        if(player != null)
        {
            Debug.Log(player.name + " cast Magic Missile!");
        }
    }
}
