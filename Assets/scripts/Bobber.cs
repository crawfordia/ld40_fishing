using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour {
    public bool hit
    {
        private set;
        get;
    }

    private void Start()
    {
        hit = false;
    }

    private void OnTriggerEnter2D()
    {
        if(this.enabled)
        {
            hit = true;
        }
    }

    private void OnEnable()
    {
        hit = false;
    }
}
