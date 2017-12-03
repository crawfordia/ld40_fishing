using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField]
    private Transform to;

    private Transform boat;

    private void LateUpdate()
    {
        if(boat == null)
        {
            BoatController boatContoller = FindObjectOfType<BoatController>();
            if(boatContoller != null)
            {
                boat = boatContoller.transform;
            }
            else
            {
                return;
            }
        }
        transform.right = (boat.position - to.position).normalized;
    }
}
