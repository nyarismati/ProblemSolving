using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float timeToDestroyMissile = 5f;

    private Rigidbody2D missileRigidbody;
    Vector2 missileMoveDirection;
    Transform target;

    private void OnEnable()
    {
        SetMissileDirectionAndRotation();
        StartCoroutine(DelayDisablingMissile());
    }

    private void Awake()
    {
        missileRigidbody = GetComponent<Rigidbody2D>();
        SetMissileTarget(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public void SetMissileTarget(Transform _target)
    {
        target = _target;
    }

    private void SetMissileDirectionAndRotation()
    {
        missileMoveDirection = target.position - transform.position;
        missileMoveDirection.Normalize();

        float angle = Mathf.Atan2(missileMoveDirection.y, missileMoveDirection.x) * Mathf.Rad2Deg - 90f;
        missileRigidbody.rotation = angle;
    }

    private void FixedUpdate()
    {
        MoveMissile();
    }

    private void MoveMissile()
    {
        missileRigidbody.velocity = missileMoveDirection * speed * Time.fixedDeltaTime;
    }

    private IEnumerator DelayDisablingMissile()
    {
        yield return new WaitForSeconds(timeToDestroyMissile);
        gameObject.SetActive(false);
    }
}
