using UnityEngine;

public class FishingSpot : MonoBehaviour {

    [SerializeField]
    int initFish;

    [SerializeField]
    float fishChance;

    public int fishLeft;

	// Use this for initialization
	void Start () {
        Init();
	}

    public int FishOnBobber()
    {
        if (Random.value <= fishChance)
        {
            return Random.Range(0, fishLeft + 1);
        }
        else
        {
            return 0;
        }
    }

    public void TakeFish(int numFish)
    {
        fishLeft -= numFish;

        if(fishLeft <= 0)
        {
            transform.position = Random.insideUnitCircle.normalized 
                * FindObjectOfType<GameState>().fishingRadius 
                * Random.Range(1.0f, 2.0f);
            Init();
        }
    }

    public void Init()
    {
        fishLeft = initFish;
    }
}
