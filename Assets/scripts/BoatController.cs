using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoatController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private float rowAxis;
    private float turnAxis;

    [SerializeField]
    private float rowForce = 80;
    [SerializeField]
    private float turnForce = 40;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rowAxis = Input.GetAxis("Vertical");
        turnAxis = -Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(transform.up * rowForce * rowAxis * Time.fixedDeltaTime);
        rigidbody.AddTorque(turnForce * turnAxis * Time.fixedDeltaTime);
    }
}
