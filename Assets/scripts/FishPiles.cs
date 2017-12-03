using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPiles : MonoBehaviour {

    public GameObject[] fishes;

	// Use this for initialization
	void Start ()
    {
        hideFishes(true);
    }

    public void hideFishes(bool hide)
    {
        foreach (GameObject fish in fishes)
        {
            fish.SetActive(!hide);
        }
    }
	
	public void setFish(int fish)
    {
        hideFishes(true);
        for(int i = 0; i < fish && i < fishes.Length - 1; i++)
        {
            fishes[i].SetActive(true);
        }
    }
}
