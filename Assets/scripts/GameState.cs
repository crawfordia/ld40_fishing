using Assets.scripts.boats;
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
    private RectTransform gameOverUI;

    [SerializeField]
    private float sinkChance;

    private BoatController activeBoat;

    [SerializeField]
    private Transform[] upgradeBoats;
    private int boatLevel = 0;

    float oldSinkChance = 0.0f;
    float newSinkChance = 0.0f;

    [Range(0.0f, 1.0f)]
    public float storminess;

    public float fishingRadius = 10.0f;

    public Slider sinkSlider;

    Dictionary<int, BaseBoat> boats = new Dictionary<int, BaseBoat>();

    // Use this for initialization
    void Start()
    {
        sinkChance = 0.0f;
        storminess = 0.0f;
        Fish = 0;
        fishText.text = Fish.ToString();
        Money = 0;
        cashText.text = Money.ToString();

        if(gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(false);
        }

        boats.Add(0, new BaseBoat(0.1f, 1, 2));
        boats.Add(1, new BaseBoat(0.2f, 2, 2));
        boats.Add(2, new BaseBoat(0.3f, 2, 3));
        boats.Add(3, new BaseBoat(0.4f, 2, 4));

        activeBoat = FindObjectOfType<BoatController>();

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
            endGame();
            Debug.Log("SUNK!");
        }
    }

    public IEnumerator updateSinkChance()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 1f);

            BaseBoat currentBoat = boats[boatLevel];
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
        updateSinkChance();
    }

    public void UpgradeBoat()
    {
        if(boatLevel < upgradeBoats.Length - 1)
        {
            boatLevel += 1;
            Vector3 spawnPoint = activeBoat.transform.position;
            Quaternion orientation = activeBoat.transform.rotation;
            Destroy(activeBoat.gameObject);

            Transform newBoat = Instantiate<Transform>(upgradeBoats[boatLevel]);
            newBoat.position = spawnPoint;
            newBoat.rotation = orientation;

            activeBoat = newBoat.GetComponent<BoatController>();
        }

    }

    private void endGame()
    {
        if(gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
