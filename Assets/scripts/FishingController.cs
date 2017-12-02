using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour {
    enum RodState
    {
        LineIn,
        Casting,
        LineOut,
        Reeling,
    }

    private RodState state;

    private const float tolerance = .02f;

    [SerializeField]
    private Transform bobber;
    [SerializeField]
    private float reelSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        state = RodState.LineIn;
        bobber.position = transform.position;
        bobber.parent = transform;
    }
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case RodState.LineIn:
                if(Input.GetButtonDown("Fire1"))
                {
                    Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    startCast(point);
                }
                
                break;
            case RodState.LineOut:
                if(Input.GetButtonDown("Fire1"))
                {
                    startReel();
                }
                break;

        }
	}

    private void startCast(Vector2 point)
    {
        StartCoroutine(castRoutine(point));
    }

    private IEnumerator castRoutine(Vector2 target)
    {
        while(Vector2.Distance(bobber.transform.position, target) > tolerance)
        {
            bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, target, reelSpeed * Time.deltaTime);
            
            yield return null;
        }

        state = RodState.LineOut;
        bobber.parent = null;

    }

    private IEnumerator reelRoutine()
    {
        while(Vector2.Distance(bobber.transform.position, transform.position) > tolerance)
        {
            bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, transform.position, reelSpeed * Time.deltaTime);

            yield return null;
        }

        state = RodState.LineIn;
        bobber.parent = transform;

    }

    private void startReel()
    {
        StartCoroutine(reelRoutine());
    }
}
