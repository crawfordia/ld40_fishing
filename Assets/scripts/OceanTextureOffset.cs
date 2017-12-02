using UnityEngine;

public class OceanTextureOffset : MonoBehaviour {

    public GameObject boat;
    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        rend.material.mainTextureOffset = boat.transform.position * 0.1f;
	}
}
