using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField]
    private Transform from;
    [SerializeField]
    private Transform to;

    private void LateUpdate()
    {
        transform.right = (from.position - to.position).normalized;
    }
}
