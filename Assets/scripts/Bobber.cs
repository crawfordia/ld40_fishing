using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour {

    [HideInInspector]
    FishingSpot fishingSpot;

    public bool hit
    {
        private set;
        get;
    }

    public int fishOn
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
        fishOn = 0;

        gameObject.layer = LayerMask.NameToLayer("Bobber");
    }

    private void OnTriggerEnter2D()
    {
        if(this.enabled)
        {
            hit = true;
        }
    }

    private IEnumerator checkCatch(FishingSpot fishSpot)
    {
        while(true)
        {
            yield return new WaitForSeconds(1f + Random.value * 2f);

            if((fishOn = fishSpot.FishOnBobber()) > 0)
            {
                sprite.color = Color.yellow;
            }
            else
            {
                sprite.color = Color.white;
            }
        }
    }

    private void OnEnable()
    {
        hit = false;
        fishOn = 0;

        transform.rotation = Quaternion.identity;

        Collider2D collider = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Fish"));

        if(collider != null)
        {
            fishingSpot = collider.GetComponent<FishingSpot>();
            fishRoutine = StartCoroutine(checkCatch(fishingSpot));
        }
        else
        {
            fishingSpot = null;
        }
        
    }

    private void OnDisable()
    {
        if(fishRoutine != null && fishingSpot != null)
        {
            StopCoroutine(fishRoutine);
            fishRoutine = null;
            fishingSpot.TakeFish(fishOn);
        }
        sprite.color = Color.white;
    }

    public int TakeFish()
    {
        int fish = fishOn;
        fishOn = 0;
        return fish;        
    }
}
