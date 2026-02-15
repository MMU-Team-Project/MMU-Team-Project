using UnityEditor;
using UnityEngine;

public class Staff : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject orbPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetPlayer(GameObject newPlayer)
    {
        if (player == null)
        {
            player = newPlayer;
        }
        //Set player owner if there isn't already one
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(orbPrefab);
            projectile.transform.position = player.transform.position;
            Debug.Log("Shoot the staff");
        }
    }
}
