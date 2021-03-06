﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

	enum TileType {
		Ground,
		Wall,
		Lava
	}

	[SerializeField]
	IntVector _dims;
	[SerializeField]
	float _tileSize;
	TileType[,] _world;
	List<WorldEntity>[,] _entityMap;

	List<WorldEntity> _entities = new List<WorldEntity>();

	public static WorldManager g;

	public void RegisterEntity (WorldEntity e) {
		_entities.Add(e);
		IntVector l = e.Location;
		_entityMap[l.x,l.y].Add(e);
	}

	public void DeregisterEntity (WorldEntity e) {
		_entities.Remove(e);
		IntVector l = e.Location;
		_entityMap[l.x,l.y].Remove(e);
	}

	void Awake () {
		if (g == null) {
			g = this;
		} else {
			Destroy (this);
		}

		_world = new TileType[_dims.x, _dims.y];
		_entityMap = new List<WorldEntity>[_dims.x, _dims.y];
		for (int x = 0; x < _dims.x; x++) {
			for (int y = 0; y < _dims.y; y++) {
				_entityMap[x,y] = new List<WorldEntity>();
			}

			_world[x,0] = TileType.Lava;
			_world[x,_dims.y-1] = TileType.Lava;
		}

		for (int y = 0; y < _dims.y; y++) {
			_world[0,y] = TileType.Lava;
			_world[_dims.x-1,y] = TileType.Lava;
		}
	}

	void Update () {
		foreach (WorldEntity e in _entities) {
			IntVector l = e.Location;
			_entityMap[l.x,l.y].Remove(e);
			e.Simulate();
			l = e.Location;
			_entityMap[l.x,l.y].Add(e);
		}
	}

	void OnDrawGizmos () {
		if (_world == null) return;
		for (int x = 0; x < _dims.x; x++) {
			for (int y = 0; y < _dims.y; y++) {
				switch (_world[x,y]) {
					case TileType.Ground:
						Gizmos.color = Color.green;
						break;
					case TileType.Wall:
						Gizmos.color = Color.blue;
						break;
					case TileType.Lava:
						Gizmos.color = Color.red;
						break;
					default:
						Gizmos.color = Color.black;
						break;
				}
				Gizmos.DrawCube(new Vector3(x * _tileSize + _tileSize/2f, y * _tileSize + _tileSize/2f, 0f), Vector3.one * _tileSize * 0.9f);
			}
		}

	foreach (WorldEntity e in _entities) {
		IntVector l = e.Location;
		Gizmos.DrawSphere(l.ToVector2() * _tileSize + new Vector2(_tileSize/2f, _tileSize/2f), _tileSize/2f * 0.8f);
	}
	}
}
