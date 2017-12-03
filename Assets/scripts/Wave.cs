using UnityEngine;

public class Wave : MonoBehaviour {

    public float sinkChanceIncrease;
    
    private void OnTriggerEnter2D()
    {
        GameState game = FindObjectOfType<GameState>();
        game.increaseSinkChance(sinkChanceIncrease);
    }
}
