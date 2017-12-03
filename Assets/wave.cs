using UnityEngine;

public class wave : MonoBehaviour {

    public float sinkChanceIncrease;
    
    private void OnTriggerEnter2D()
    {
        GameState game = FindObjectOfType<GameState>();
        game.increaseSinkChance(sinkChanceIncrease);
        Debug.Log("WAVE HIT!");
    }
}
