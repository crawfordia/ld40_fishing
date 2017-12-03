﻿using Assets.scripts.boats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    //[HideInInspector]
    public int Fish;
    public int Money;

    [SerializeField]
    private Text fishText;
    [SerializeField]
    private Text cashText;

    [SerializeField]
    private float sinkChance;

    float oldSinkChance = 0.0f;
    float newSinkChance = 0.0f;

    [Range(0.0f, 1.0f)]
    public float storminess;

    public float fishingRadius = 10.0f;

    public Slider sinkSlider;

    enum BoatType {
        boat1x2,
        boat2x2,
        boat2x3,
        boat2x4
    }
    BoatType currentBoatType = BoatType.boat1x2;

    Dictionary<BoatType, BaseBoat> boats = new Dictionary<BoatType, BaseBoat>();

    // Use this for initialization
    void Start()
    {
        sinkChance = 0.0f;
        storminess = 0.0f;
        Fish = 0;
        fishText.text = Fish.ToString();
        Money = 0;
        cashText.text = Money.ToString();

        boats.Add(BoatType.boat1x2, new BaseBoat(0.1f, 1, 2));
        boats.Add(BoatType.boat2x2, new BaseBoat(0.2f, 2, 2));
        boats.Add(BoatType.boat2x3, new BaseBoat(0.3f, 2, 3));
        boats.Add(BoatType.boat2x4, new BaseBoat(0.4f, 2, 4));

        StartCoroutine(updateSinkChance());
    }

    // Update is called once per frame
    void Update()
    {
        sinkChance = Mathf.Lerp(oldSinkChance, newSinkChance, 0.2f);
        oldSinkChance = sinkChance;
        sinkSlider.value = sinkChance;

        if (sinkChance >= 1.0f)
        {
            Debug.Log("SUNK!");
        }
    }

    public IEnumerator updateSinkChance()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 1f);

            BaseBoat currentBoat = boats[currentBoatType];
            oldSinkChance = sinkChance;
            // Algorithm:
            newSinkChance = Mathf.Max(0.0f,
                -(currentBoat.stabilityIncrease)
                + Random.Range(0, storminess)
                + ((float)Fish / currentBoat.fishCapacity)
                );
        }
    }

    public void AddFish(int numFish)
    {
        Fish += numFish;
        fishText.text = Fish.ToString();
    }

    public void SellFish()
    {
        Money += Fish;
        Fish = 0;
        fishText.text = Fish.ToString();
        cashText.text = Money.ToString();
    }
}
