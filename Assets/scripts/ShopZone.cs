using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopZone : MonoBehaviour {
    [SerializeField]
    private RectTransform storeUI;

    private void Start()
    {
        storeUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D()
    {
        storeUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D()
    {
        storeUI.gameObject.SetActive(false);
    }
}
