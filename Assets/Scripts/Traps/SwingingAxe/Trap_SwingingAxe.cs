using UnityEngine;
using System.Collections.Generic;

public class Trap_SwingingAxe : MonoBehaviour
{
    [Header("-= Swing Settings =-")]
    public Transform axeModel;
    public float swingAngle = 60f;
    public float swingSpeed = 2f;
    public float peakPauseTime = 0.2f;

    [Header("-= Detection Settings =-")]
    public Vector3 boxSize = new Vector3(1, 1, 1);
    public Vector3 boxOffset = new Vector3(0, 0, 1);
    public LayerMask detectionMask;

    [Header("-= Gizmo Settings =-")]
    public float gizmoRadius = 2f;
    public int gizmoSegments = 20;

    private float currentAngle;
    private float swingTimer;
    private bool isPaused;
    private float pauseTimer;

    private List<GameObject> previousHits = new List<GameObject>();

    void Update()
    {
        Swing();
        Detect();
    }

    void Swing()
    {
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= peakPauseTime)
            {
                isPaused = false;
                pauseTimer = 0f;
            }
            return;
        }

        swingTimer += Time.deltaTime * swingSpeed;

        float sinValue = Mathf.Sin(swingTimer);
        currentAngle = sinValue * swingAngle;

        // detect peak
        if (Mathf.Abs(sinValue) > 0.999f)
        {
            isPaused = true;
        }

        axeModel.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }

    void Detect()
    {
        Quaternion rotation = axeModel.rotation;
        Vector3 center = axeModel.position + rotation * boxOffset;

        Collider[] hits = Physics.OverlapBox(center, boxSize * 0.5f, rotation, detectionMask);

        List<GameObject> currentHits = new List<GameObject>();

        foreach (Collider hit in hits)
        {
            GameObject obj = hit.gameObject;
            currentHits.Add(obj);

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
        if (axeModel == null) return;

        // the code below I partly generated with AI, I wanted the debug gizmos to be accurate, and if it is preferred not it can be removed!!

        Gizmos.color = Color.yellow;

        Vector3 pivot = transform.position;

        Vector3 baseDir = -transform.up;

        // Axis to rotate around
        Vector3 axis = transform.forward;

        Vector3 prevPoint = pivot + Quaternion.AngleAxis(-swingAngle, axis) * baseDir * gizmoRadius;

        for (int i = 1; i <= gizmoSegments; i++)
        {
            float t = (float)i / gizmoSegments;
            float angle = Mathf.Lerp(-swingAngle, swingAngle, t);

            Vector3 nextPoint = pivot + Quaternion.AngleAxis(angle, axis) * baseDir * gizmoRadius;

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }

        // ===== HITBOX GIZMO =====
        Gizmos.color = Color.red;

        Quaternion rotation = axeModel.rotation;
        Vector3 center = axeModel.position + rotation * boxOffset;

        Matrix4x4 matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
        Gizmos.matrix = matrix;

        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}