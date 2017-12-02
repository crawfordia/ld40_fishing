using UnityEngine;

public class OceanTextureOffset : MonoBehaviour {
    public Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        rend.material.mainTextureOffset = transform.position * 0.025f;
	}
}
