using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap_SpikeHoles : MonoBehaviour
{
    [Header("-= Detection Settings =-")]
    public Vector3 boxSize = new Vector3(1, 1, 1);
    public float triggerExtension = 0.5f;
    public LayerMask detectionMask;

    [Header("- Trap Settings -")]
    public Transform spikes;
    public float riseAmount = 2f;
    public float riseSpeed = 8f;
    public float fallSpeed = 3f;
    public float stayUpTime = 1.5f;

    [Header("- Cooldown -")]
    public float cooldownTime = 2f;

    private Vector3 startLocalPos;
    private Vector3 targetLocalPos;

    private bool isActive = false;
    private bool isOnCooldown = false;
    private bool isRisingOrUp = false;

    private List<GameObject> previousHits = new List<GameObject>();

    void Start()
    {
        startLocalPos = spikes.localPosition;
        targetLocalPos = startLocalPos + (Vector3.up * riseAmount);
    }

    void Update()
    {
        Detect();
    }

    void Detect()
    {
        if (isActive || isOnCooldown) return;

        Collider[] hits = Physics.OverlapBox(transform.position, boxSize * 0.5f * triggerExtension, transform.rotation, detectionMask);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
            {
                StartCoroutine(ActivateTrap());
                break;
            }
        }
    }

    IEnumerator ActivateTrap()
    {
        isActive = true;
        isRisingOrUp = true;

        previousHits.Clear();

        // Rise
        while (Vector3.Distance(spikes.localPosition, targetLocalPos) > 0.01f)
        {
            spikes.localPosition = Vector3.MoveTowards(spikes.localPosition, targetLocalPos, riseSpeed * Time.deltaTime);
            CheckDamage();
            yield return null;
        }

        spikes.localPosition = targetLocalPos;

        // Stay up
        float timer = 0f;
        while (timer < stayUpTime)
        {
            CheckDamage();
            timer += Time.deltaTime;
            yield return null;
        }

        // Fall
        isRisingOrUp = false;

        while (Vector3.Distance(spikes.localPosition, startLocalPos) > 0.01f)
        {
            spikes.localPosition = Vector3.MoveTowards(spikes.localPosition, startLocalPos, fallSpeed * Time.deltaTime);
            yield return null;
        }

        spikes.localPosition = startLocalPos;

        // Cooldown
        isActive = false;
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    void CheckDamage()
    {
        if (!isRisingOrUp) return;

        Collider[] hits = Physics.OverlapBox(transform.position, boxSize * 0.5f * triggerExtension, transform.rotation, detectionMask);

        List<GameObject> currentHits = new List<GameObject>();

        foreach (Collider hit in hits)
        {
            GameObject obj = hit.gameObject;
            currentHits.Add(obj);

            // If NOT in last frame, so it just entered
            if (!previousHits.Contains(obj))
            {
                if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
                {
                    Debug.Log("Damaged " + obj.name);
                }
            }
        }

        previousHits = currentHits;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(Vector3.zero, boxSize * 0.5f * triggerExtension);
    }
}