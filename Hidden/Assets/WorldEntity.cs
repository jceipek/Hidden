using UnityEngine;
using System.Collections;

public class WorldEntity : MonoBehaviour {

	[SerializeField]
	IntVector _location;

	public IntVector Location {
		get { return _location; }
		set { _location = value; }
	}

	private bool _registered = false;

	public void RegisterMe () {
		if (!_registered) {
			WorldManager.g.RegisterEntity(this);
			_registered = true;
		}
	}

	public void DeregisterMe () {
		if (_registered) {
			WorldManager.g.DeregisterEntity(this);
			_registered = false;
		}
	}

	void Start () {
		RegisterMe();
	}

	public delegate void SimulatorDelegates();
    public SimulatorDelegates Simulators;

	public void Simulate() {
		if (Simulators != null) {
			Simulators();
		}
	}
}