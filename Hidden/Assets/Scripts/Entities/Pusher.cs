using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour
{
    //set in the XML
    public int iID;
//to associate with the trigger
    public bool isControlled;
    public Direction direction;
    public int range;
    public float fTimeInterval = 0.5f;

    public bool isTriggered;

    private WorldEntity _worldEntity;
	
    public void Cache()
    {
        _worldEntity = GetComponent<WorldEntity>();
    }

    void Awake()
    {
        Cache();
        _worldEntity.CollidingType = EntityCollidingType.Pushable;
    }

    void OnEnable()
    {
        _worldEntity.Simulators += Simulate;
    }

    void OnDisable()
    {
        _worldEntity.Simulators -= Simulate;
    }
    private void Simulate()
    {
        if (!isControlled)
        {

        }
        else if (isTriggered)
        {
        }
        else
        {
			
        }
    }
    private void Move()
    {

    }
    private Direction DirectionFlip(Direction direction)
    {
    	switch (direction){
    		case Direction.East:
    		return Direction.West;
    		break;
    		case Direction.West:
    		return Direction.East;
    		break;
    		case Direction.North:
    		return Direction.South;
    		break;
    		case Direction.South:
    		return Direction.North;
    		break;
    		default:
    		return Direction.North;//error
    		break;
    	}
    }
}
