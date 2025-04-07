using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Transform destroyZone;

    void Start()
    {
        GameObject dz = GameObject.FindGameObjectWithTag("DestroyZone");
        if (dz != null)
        {
            destroyZone = dz.transform;
        }
        else
        {
            Debug.LogWarning("DestroyZone not found! Please tag the destroy object properly.");
        }
    }

    void Update()
    {
        // Move the obstacle toward the player
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

        // Destroy it if it goes behind the destroy zone
        if (destroyZone != null && transform.position.z < 13.0f)
        {
            ScoreManager.Instance.AddPoint();
            Destroy(gameObject);
        }
    }
}
