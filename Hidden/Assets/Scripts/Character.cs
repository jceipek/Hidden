using UnityEngine;
using System.Collections;
using InControl;


public class Character : MonoBehaviour
{

    [SerializeField]
    KeyCode _leftKey;
    [SerializeField]
    KeyCode _rightKey;
    [SerializeField]
    KeyCode _upKey;
    [SerializeField]
    KeyCode _downKey;

    private WorldEntity _worldEntity;
    private IntVector _input;

    private Direction _direction;
    private bool _bMove;

    [SerializeField]
    int _iCharacterID;

    //[SerializeField]
    //Direction _facing;

    public void Cache()
    {
        _worldEntity = GetComponent<WorldEntity>();
    }

    void Awake()
    {
        Cache();
        _worldEntity.CollidingType = EntityCollidingType.Pushable;
        if(_iCharacterID == 1)
        {
            _worldEntity.entityType = EntityType.Character1;
        }
        else if (_iCharacterID == 2)
        {
            _worldEntity.entityType = EntityType.Character2;
        }
    }

    void OnEnable()
    {
        _worldEntity.Simulators += Simulate;
    }

    void OnDisable()
    {
        _worldEntity.Simulators -= Simulate;
    }


    void Update()
    {
        //_input = new IntVector(Vector2.zero);
        if (Input.GetKeyDown(_leftKey))
        {
            _input.x -= 1;
            _direction = Direction.West;
            _bMove = true;
        }
        if (Input.GetKeyDown(_rightKey))
        {
            _input.x += 1;
            _direction = Direction.East;
            _bMove = true;
        }
        if (Input.GetKeyDown(_upKey))
        {
            _input.y += 1;
            _direction = Direction.North;
            _bMove = true;
        }
        if (Input.GetKeyDown(_downKey))
        {
            _input.y -= 1;
            _direction = Direction.South;
            _bMove = true;
        }

        if (_worldEntity.isPushed)
        {
            Pushed(_worldEntity.pushedDirection);
        }
    }

    private void Pushed(Direction direction)
    {
        //play pushed animation
    }
    private void Simulate()
    {
        if (_bMove)
        {
            switch (WorldManager.g.CanMove(_worldEntity.Location, _direction))
            {
                case MoveResult.Move:
                    Move();
                    break;
                case MoveResult.Stuck:
                    Stuck();
                    break;
                case MoveResult.Push:
                    Push();
                    break;
                default:
                    break;
            }
            _bMove = false;
        }       
    }
    private void Push()
    {
        //play push animation
        IntVector vec = _worldEntity.Location;
        if (_input.x != 0)
        {
            vec.x += _input.x;
            _input.x = 0;
        }
        else if (_input.y != 0)
        {
            vec.y += _input.y;
            _input.y = 0;       
        }
        _worldEntity.Location = vec;
    }
    private void Stuck()
    {
        //play stuck animation
        _input.x = 0;
        _input.y = 0; 
        
    }
    private void Move()
    {
        //play move animaition

        IntVector vec = _worldEntity.Location;
        if (_input.x != 0)
        {
            vec.x += _input.x;
            _input.x = 0;
        }
        else if (_input.y != 0)
        {
            vec.y += _input.y;
            _input.y = 0;            
        }
        _worldEntity.Location = vec;
    }
}