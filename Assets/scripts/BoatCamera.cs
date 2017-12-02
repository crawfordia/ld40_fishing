using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCamera : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D boatRigidbody;

    [SerializeField]
    private Vector3 offset = Vector3.back * 10f;
    [SerializeField]
    private Vector3 localOffset = Vector3.zero;
    [SerializeField]
    private float velocityScale = 1.0f;

    private void LateUpdate()
    {
        transform.position = boatRigidbody.transform.position + offset;
        transform.position += boatRigidbody.transform.TransformDirection(localOffset);
        transform.position += (Vector3)boatRigidbody.velocity * velocityScale;
    }
}
