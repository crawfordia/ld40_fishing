using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [HideInInspector]
    public int Fish;
    [HideInInspector]
    public int Money;
    
    [Space(10)]
    public float fishingRadius = 10.0f;
    [Range(0.0f, 1.0f)]
    public float storminess;

    [Header("Wave Spawn Settings")]
    public Transform wavePrefab;
    public float waveSpawnRadius;
    public float waveSpeed;
    public float baseWaveTime;
    public float randWaveTime;
    public int numWavesPerSpawn;

    [System.Serializable]
    private struct BoatUpgrade
    {
        public Transform boat;
        public int cost;
        public float sinkResist;
        public int cols;
        public int rows;
    }

    [Space(10)]
    [SerializeField]
    private BoatUpgrade[] upgrades;

    [Header("UI Objects")]
    [SerializeField]
    private Text fishText;
    [SerializeField]
    private Text cashText;
    [SerializeField]
    private Text upgradeText;
    [SerializeField]
    private RectTransform gameOverUI;
    [SerializeField]
    private Slider sinkSlider;

    private int boatLevel = 0;
    private float sinkChance;
    private float sinkChanceIncrease;
    private BoatController activeBoat;

    // Use this for initialization
    void Start()
    {
        Fish = 0;
        fishText.text = Fish.ToString();
        Money = 0;
        cashText.text = Money.ToString();
        upgradeText.text = string.Format("Upgrade - {0}", upgrades[1].cost);

        if(gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(false);
        }

        activeBoat = FindObjectOfType<BoatController>();

        StartCoroutine(updateSinkChanceRoutine());
        StartCoroutine(sendWaves());
    }

    // Update is called once per frame
    void Update()
    {
        sinkChanceIncrease = Mathf.Lerp(sinkChanceIncrease, 0.0f, 0.1f * Time.deltaTime);
        //sinkChance = Mathf.Lerp(oldSinkChance, newSinkChance, 0.5f * Time.deltaTime);
        sinkSlider.value = sinkChance;

        if (sinkChance >= 1.0f)
        {
            endGame();
            Debug.Log("SUNK!");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public IEnumerator updateSinkChanceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.value * 1f);
            updateSinkChance();
        }
    }

    public void updateSinkChance()
    {
        float resist = upgrades[boatLevel].sinkResist;
        float storm = Random.Range(0.0f, storminess);
        int boatRows = upgrades[boatLevel].rows;
        int boatCols = upgrades[boatLevel].cols;
        float fishCap = Fish / (float)(boatRows * boatCols * 4);

        sinkChance = Mathf.Max(0.0f,
            -resist
            + storm
            + fishCap
            + sinkChanceIncrease
            );
    }

    public void AddFish(int numFish)
    {
        Fish += numFish;
        fishText.text = Fish.ToString();
        updateSinkChance();
        activeBoat.GetComponentInChildren<FishPiles>().setFish(Fish);
    }

    public void SellFish()
    {
        Money += Fish;
        Fish = 0;
        fishText.text = Fish.ToString();
        cashText.text = Money.ToString();
        updateSinkChance();
        activeBoat.GetComponentInChildren<FishPiles>().setFish(Fish);
    }

    public void UpgradeBoat()
    {
        if(boatLevel < upgrades.Length - 1)
        {
            if(Money >= upgrades[boatLevel + 1].cost)
            {
                SellFish();
                Money -= upgrades[boatLevel + 1].cost;
                cashText.text = Money.ToString();
                boatLevel += 1;
                Vector3 spawnPoint = activeBoat.transform.position;
                Quaternion orientation = activeBoat.transform.rotation;
                Destroy(activeBoat.gameObject);

                Transform newBoat = Instantiate<Transform>(upgrades[boatLevel].boat);
                newBoat.position = spawnPoint;
                newBoat.rotation = orientation;

                activeBoat = newBoat.GetComponent<BoatController>();
            }
        }

        if(boatLevel < upgrades.Length - 1)
        {
            upgradeText.text = string.Format("Upgrade - {0}", upgrades[boatLevel + 1].cost);
        }
        else
        {
            upgradeText.text = "Max Level";
        }

    }
    
    private void endGame()
    {
        if(gameOverUI != null)
        {
            Destroy(activeBoat.gameObject);
            this.enabled = false;
            gameOverUI.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("test_scene");
    }

public IEnumerator sendWaves()
    {
        List<Transform> wavePool = new List<Transform>(10);

        while (true)
        {
            yield return new WaitForSeconds(baseWaveTime + Random.value * randWaveTime);

            Transform theBoat = FindObjectOfType<BoatController>().transform;
            Transform newWave;

            // Create a new waves, point it at the player, and push it forward
            for (int i = 0; i < Random.Range(0, numWavesPerSpawn); i++)
            {
                if (wavePool.Count > 0)
                {
                    newWave = wavePool[0];
                    wavePool.RemoveAt(0);
                    newWave.gameObject.SetActive(true);
                }
                else
                {
                    newWave = Instantiate<Transform>(wavePrefab);
                }

                newWave.transform.position = theBoat.position + (Vector3)(Random.insideUnitCircle.normalized * waveSpawnRadius);
                newWave.up = (theBoat.position - newWave.transform.position).normalized;
                newWave.GetComponent<Rigidbody2D>().velocity = newWave.up * waveSpeed;
            }

            // Remove distant waves
            foreach(Wave wave in FindObjectsOfType<Wave>())
            {
                if (Vector3.Distance(wave.transform.position, theBoat.position) > (waveSpawnRadius * 2.0f))
                {
                    wave.gameObject.SetActive(false);
                    wavePool.Add(wave.transform);
                }
            }
        }
    }

    public void increaseSinkChance(float increase)
    {
        sinkChanceIncrease += increase;
        updateSinkChance();
    }
}
