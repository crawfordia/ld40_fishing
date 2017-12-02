using Assets.scripts.boats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    //[HideInInspector]
    public int Fish;

    [SerializeField]
    private Text fishText;

    [SerializeField]
    private float sinkChance;

    [SerializeField]
    private float storminess;

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

        boats.Add(BoatType.boat1x2, new BaseBoat(0.1f, 1, 2));
        boats.Add(BoatType.boat2x2, new BaseBoat(0.2f, 2, 2));
        boats.Add(BoatType.boat2x3, new BaseBoat(0.3f, 2, 3));
        boats.Add(BoatType.boat2x4, new BaseBoat(0.4f, 2, 4));
    }

    // Update is called once per frame
    void Update()
    {
        updateSinkChance();

        if(sinkChance >= 1.0f) {
            Debug.Log("SUNK!");
        }
    }

    void updateSinkChance()
    {
        BaseBoat currentBoat = boats[currentBoatType];
        // Algorithm:
        sinkChance = Mathf.Max(0.0f, 
            -(currentBoat.stabilityIncrease) 
            + Random.Range(0, storminess) 
            + ((float)Fish / currentBoat.fishCapacity)
            );
    }

    public void AddFish()
    {
        Fish += 1;
        fishText.text = Fish.ToString();
    }
}
