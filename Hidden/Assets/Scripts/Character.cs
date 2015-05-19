﻿using UnityEngine;
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
    private bool _move;

    [SerializeField]
    Direction _facing;

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

    void Update()
    {
        _input = new IntVector(Vector2.zero);
        if (Input.GetKeyDown(_leftKey))
        {
            _input.x -= 1;
            _direction = Direction.West;
            _move = true;
        }
        if (Input.GetKeyDown(_rightKey))
        {
            _input.x += 1;
            _direction = Direction.East;
            _move = true;
        }
        if (Input.GetKeyDown(_upKey))
        {
            _input.y += 1;
            _direction = Direction.North;
            _move = true;
        }
        if (Input.GetKeyDown(_downKey))
        {
            _input.y -= 1;
            _direction = Direction.South;
            _move = true;
        }
    }

    private void Simulate()
    {
        if (_move)
        {
            switch (WorldManager.g.CanMove(_worldEntity.Location, _direction))
            {
                case MoveResult.Move:
                    Move();
                    print("move");
                    break;
                case MoveResult.Stuck:
                    Stuck();
                    print("stuck");
                    break;
                case MoveResult.Push:
                    Push();
                    print("push");
                    break;
                default:
                    break;
            }
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
            _move = false;
        }
        else if (_input.y != 0)
        {
            vec.y += _input.y;
            _input.y = 0;
            _move = false;
        }
        _worldEntity.Location = vec;
    }
    private void Stuck()
    {
        //play stuck animation
        _move = false;
    }
    private void Move()
    {
        //play move animaition

        IntVector vec = _worldEntity.Location;
        if (_input.x != 0)
        {
            vec.x += _input.x;
            _input.x = 0;
            _move = false;
        }
        else if (_input.y != 0)
        {
            vec.y += _input.y;
            _input.y = 0;
            _move = false;
        }
        _worldEntity.Location = vec;
    }
}