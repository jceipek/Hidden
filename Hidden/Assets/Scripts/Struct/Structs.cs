using UnityEngine;
using System.Collections;

public struct SavableLevel
    {
        public string sLevelName;
        public int iLevelType;
        public IntVector vDim;
        public IntVector vChar1StartPos, vChar2StartPos;
        public Tile[] tMap;
    }

    public struct Tile
    {
        public IntVector vTilePosition;
        public int iTileType;
        public TileType tileType;
    }

    public enum TileType
    {
        Empty
        ,Floor
        ,Wall
    }
    public enum MoveResult
    {
        Move
        ,Push
        ,Stuck
    }
    public enum EntityCollidingType
    {
        Empty
        ,Colliding
        ,Pushable
    }
    public enum Direction {
        North,
        South,
        East,
        West
    }

public class Structs : MonoBehaviour {
	
}
