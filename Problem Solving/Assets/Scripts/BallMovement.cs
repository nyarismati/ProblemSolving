using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    [SerializeField] private float speed = 100f;
    [SerializeField] private MovementType movementType;

    Vector2 keyboardInputAxis;
    Vector2 ballToMouseDirection;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (movementType == MovementType.RANDOM)
        {
            Vector2 ballDirection = GetRandomDirection();
            MoveBall(ballDirection);
        }
    }

    private void Update()
    {
        //get keyboard axis for keyboard controlled movement
        keyboardInputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(movementType == MovementType.MOUSE_CONTROLLED)
        {
            //get mouse position and ball direction to mouse
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if the ball is too close from mouse, stop it to avoid jitter
            if (Vector2.Distance(mousePositionInWorld, transform.position) < 0.1f)
            {
                ballToMouseDirection = Vector2.zero;
            }
            else
            {
                ballToMouseDirection = ((Vector2)(mousePositionInWorld - transform.position)).normalized;
            }
        }
    }

    private void FixedUpdate()
    {
        if(movementType == MovementType.KEYBOARD_CONTROLLED)
        {
            MoveBall(keyboardInputAxis);
        }
        else if(movementType == MovementType.MOUSE_CONTROLLED)
        {
            MoveBall(ballToMouseDirection);
        }
    }

    public void MoveBall(Vector3 direction)
    {
        rigidbody2D.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private Vector2 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        return randomDirection;
    }
}

public enum MovementType
{
    RANDOM,
    KEYBOARD_CONTROLLED,
    MOUSE_CONTROLLED
}
