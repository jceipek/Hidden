using UnityEngine;
using System.Collections;
using InControl;


public class Character : MonoBehaviour {

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

	public enum Facing {
		North,
		South,
		East,
		West
	}

	[SerializeField]
	Facing _facing;

	public void Cache () {
		_worldEntity = GetComponent<WorldEntity>();
	}

	void Awake () {
		Cache();
	}

	void OnEnable () {
		_worldEntity.Simulators += Simulate;
	}

	void OnDisable () {
		_worldEntity.Simulators += Simulate;
	}

	void Update () {
		_input = new IntVector(Vector2.zero);
		if (Input.GetKeyDown(_leftKey)) {
			_input.x -= 1;
		}
		if (Input.GetKeyDown(_rightKey)) {
			_input.x += 1;
		}
		if (Input.GetKeyDown(_upKey)) {
			_input.y += 1;
		}
		if (Input.GetKeyDown(_downKey)) {
			_input.y -= 1;
		}
	}

	private void Simulate () {
		IntVector vec = _worldEntity.Location;
		if (_input.x != 0) {
			vec.x += _input.x;
			_input.x = 0;
		} else if (_input.y != 0) {
			vec.y += _input.y;
			_input.y = 0;
		}
		_worldEntity.Location = vec;
	}
}