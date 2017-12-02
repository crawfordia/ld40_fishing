using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [HideInInspector]
    public int Fish;

    [SerializeField]
    private Text fishText;

    // Use this for initialization
    void Start()
    {
        Fish = 0;
        fishText.text = Fish.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
