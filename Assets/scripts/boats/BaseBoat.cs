using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Assets.scripts.boats
{
    public class BaseBoat
    {
        public const int FISH_PER_TILE = 4;

        public float stabilityIncrease;
        public int fishCapacity;
        public int cols, rows;

        public BaseBoat(float stability, int cols, int rows)
        {
            stabilityIncrease = stability;
            this.cols = cols;
            this.rows = rows;
            fishCapacity = cols * rows * FISH_PER_TILE;
        }
    }
}