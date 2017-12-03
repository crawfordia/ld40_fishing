using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlesController : MonoBehaviour {

    public GameObject boat;

    public GameObject[] particles;

    private void Start()
    {
        StartCoroutine(playParticles());
    }

    public IEnumerator playParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            bool boatMoving = boat.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f;


            foreach (GameObject go in particles)
            {
                ParticleSystem ps = go.GetComponent<ParticleSystem>();
                if(boatMoving)
                {
                    ps.Play();
                }
            }
        }
    }
}
