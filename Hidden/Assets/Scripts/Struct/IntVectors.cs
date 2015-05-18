using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public struct IntVector {
	public int x;
	public int y;

	public override string ToString(){
		return x+", "+y;
	}

	public override int GetHashCode(){
		return x.GetHashCode() ^ y.GetHashCode();
	}

	public override bool Equals( object ob ){
		if( ob is IntVector ) {
			IntVector other = (IntVector) ob;
			return (other.x == x && other.y == y);
		}
		else {
			return false;
		}
	}

	public static bool operator ==(IntVector a, IntVector b) {
	  return (a.x == b.x && a.y == b.y);
	}

	public static bool operator !=(IntVector x, IntVector y) {
	  return !(x == y);
	}

	public static IntVector operator +(IntVector a, IntVector b) {
	  return new IntVector (a.x+b.x, a.y+b.y);
	}

	public static IntVector operator -(IntVector a, IntVector b) {
	  return new IntVector (a.x-b.x, a.y-b.y);
	}

	public Vector2 ToVector2 () {
		return new Vector2(x,y);
	}

    public int this[int index]
    {
        get
        {
        	switch (index) {
        		case 0:
        			return x;
        		case 1:
        			return y;
        		default:
        			throw new Exception("Can't access "+index);
        	}
        }

        set
        {
        	switch (index) {
        		case 0:
        			x = value;
        			break;
        		case 1:
        			y = value;
        			break;
        		default:
        			throw new Exception("Can't access "+index);
        	}
        }
    }

	public IntVector(int nx, int ny) {
		x = nx;
		y = ny;
	}

	public IntVector(Vector2 v) {
		x = Mathf.RoundToInt(v.x);
		y = Mathf.RoundToInt(v.y);
	}
}
