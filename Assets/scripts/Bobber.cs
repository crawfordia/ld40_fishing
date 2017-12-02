using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour {
    public bool hit
    {
        private set;
        get;
    }

    public bool hasFish
    {
        private set;
        get;
    }

    private Coroutine fishRoutine;

    // Temporary
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        hit = false;
        hasFish = false;
    }

    private void OnTriggerEnter2D()
    {
        if(this.enabled)
        {
            hit = true;
        }
    }

    private IEnumerator checkCatch()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f + Random.value * 2f);

            if(!hasFish && Random.value < .5f)
            {
                hasFish = true;
                sprite.color = Color.yellow;
            }
            else
            {
                hasFish = false;
                sprite.color = Color.white;
            }
        }
    }

    private void OnEnable()
    {
        hit = false;
        hasFish = false;

        if(Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Fish")) != null)
        {
            fishRoutine = StartCoroutine(checkCatch());
        }
        
    }

    private void OnDisable()
    {
        if(fishRoutine != null)
        {
            StopCoroutine(fishRoutine);
            fishRoutine = null;
        }
        sprite.color = Color.white;
    }

    public bool TakeFish()
    {
        if(hasFish)
        {
            hasFish = false;
            return true;
        }

        return false;
        
    }
}
