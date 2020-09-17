using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{

    public float frequency;
    public float magnitude;
    public float forwardSpeed;
    public Vector3 direction;

    public float BulletDamage;

    private Rigidbody rb;
    private Vector3 startPos;
    Vector3 pos;

    public bool right;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        pos = transform.position;
    }

    private void FixedUpdate()
    {
        pos += transform.forward * forwardSpeed * Time.deltaTime;

        rb.MovePosition(pos + direction * Mathf.Sin(Time.time * frequency) * magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAttribute>().TakeDamage(BulletDamage);
            Destroy(gameObject);
        }else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }





}
