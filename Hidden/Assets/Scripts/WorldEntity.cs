using UnityEngine;
using System.Collections;

public class WorldEntity : MonoBehaviour
{
    //simulate priority later.

    public EntityType entityType;

    private EntityCollidingType _collidingType;
    public EntityCollidingType CollidingType
    {
        get { return _collidingType; }
        set { _collidingType = value; }
    }
    [HideInInspector]
    public bool isPushed;
    [HideInInspector]
    public Direction pushedDirection;

    [SerializeField]
    IntVector _location;

    public IntVector Location
    {
        get { return _location; }
        set { _location = value; }
    }

    private bool _registered = false;

    public void RegisterMe()
    {
        if (!_registered)
        {
            WorldManager.g.RegisterEntity(this);
            _registered = true;
        }
    }

    public void DeregisterMe()
    {
        if (_registered)
        {
            WorldManager.g.DeregisterEntity(this);
            _registered = false;
        }
    }

    void Start()
    {
        RegisterMe();
    }
    public void Pushed(Direction direction)
    {
        isPushed = true;
        pushedDirection = direction;
    }

    public delegate void SimulatorDelegates();
    public SimulatorDelegates Simulators;

    public void Simulate()
    {
        if (Simulators != null)
        {
            Simulators();
        }
    }
}