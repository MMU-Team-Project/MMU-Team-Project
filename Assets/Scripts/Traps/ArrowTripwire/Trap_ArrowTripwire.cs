using UnityEngine;

public class Trap_ArrowTripwire : MonoBehaviour
{
    [Header("-= References =-")]
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] Transform ArrowSpawnPoint;
    [Header("- Trap Settings -")]
    [SerializeField] bool ShowTripwire = true; //Option to hide the tripwire, perhaps on harder difficulties?
    [SerializeField] float ArrowDirectionRandomness = 0.5f; // Randomly offsets arrow
    bool hasBeenTriggered; // Used to prevent multiple arrows being shot

    private void Start()
    {
        LineRenderer renderer = gameObject.GetComponent<LineRenderer>();

        if (ShowTripwire)
        {
            if(renderer != null) SetupTripwire(renderer);
        }
        else
        {
            if (renderer != null) renderer.enabled = false;
        }
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 999))
        {
            print(hit.collider.gameObject.name);
            if(hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
            {
                if (!hasBeenTriggered) ShootArrow();
            }
            else
            {
                hasBeenTriggered = false;
            }
        }
    }

    void ShootArrow()
    {
        hasBeenTriggered = true;
        GameObject Arrow = Instantiate(ArrowPrefab);
        Arrow.transform.position = ArrowSpawnPoint.position + (transform.forward / 5); // Move it slightly forward to avoid hitting wall on spawn
        Arrow.transform.rotation = Quaternion.Euler(transform.eulerAngles.x + Random.Range(-ArrowDirectionRandomness, ArrowDirectionRandomness), transform.eulerAngles.y + Random.Range(-ArrowDirectionRandomness, ArrowDirectionRandomness), transform.eulerAngles.z + Random.Range(-ArrowDirectionRandomness, ArrowDirectionRandomness));
    }

    private void SetupTripwire(LineRenderer line)
    {
        line.SetPosition(0, transform.position);
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,999))
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            Debug.LogError("Tripwire failed to setup, raycast returned null");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawLine(Vector3.zero, Vector3.forward);
        Gizmos.color = Color.yellow;
        if (ArrowSpawnPoint != null) Gizmos.DrawLine(ArrowSpawnPoint.localPosition, ArrowSpawnPoint.localPosition + Vector3.forward);
    }
}
