using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] float moveSpeed = 5f, rotationSpeed = 720f;

    Rigidbody rb;
    public bool isGameOver = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isGameOver) return;

        Vector3 forwardMove = transform.forward * moveSpeed;
        rb.velocity = new Vector3(forwardMove.x, rb.velocity.y, forwardMove.z);

        Vector3 joystickDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if (joystickDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(joystickDirection.normalized);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    public void StopMovement()
    {
        isGameOver = true;
    }
}
