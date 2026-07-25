using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObtacle : MonoBehaviour
{
    public static List<MovableObtacle> AllObstacles = new List<MovableObtacle>();

    private Vector3 originSpawn;

    private Quaternion originRotation;

    private Rigidbody rb;

    private Collider col;

    public void ResetObjectToOrigin()
    {
        transform.position = originSpawn;
        transform.rotation = originRotation;

        // reset velocity
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        StartCoroutine(SupressCollision());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        AllObstacles.Add(this);
        rb.maxDepenetrationVelocity = 2f;

        originSpawn = transform.position;
        originRotation = transform.rotation;
    }

    private IEnumerator SupressCollision()
    {
        col.enabled = false;
        yield return new WaitForFixedUpdate();
        col.enabled = true;
    }

    private void OnDestroy()
    {
        AllObstacles.Remove(this);
    }
}
