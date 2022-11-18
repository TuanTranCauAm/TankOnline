using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool destroy;

    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float accelation = 50f;
    [SerializeField] private float braking = 10f;
    [SerializeField] private float rotationSpeed = 7f;
    Rigidbody rb;

    void Start() { rb = GetComponent<Rigidbody>(); }

    void FixedUpdate()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");
        var moveDir = new Vector3(moveX, 0f, moveZ);
        if (moveDir == default)
            rb.velocity = Vector3.Lerp(rb.velocity, default, braking * Time.deltaTime);
        else
        {
            rb.velocity += moveDir * Time.deltaTime * accelation;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), rotationSpeed * Time.deltaTime);
        }

    }
}
