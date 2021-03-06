﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridBehaviour : MonoBehaviour {

	private GameObject _gridBehaviourPiecesHolder;

	protected GameObject PiecesHolder() {
		_gridBehaviourPiecesHolder = _gridBehaviourPiecesHolder ? _gridBehaviourPiecesHolder : GameObject.Find("piecesHolder");
		return _gridBehaviourPiecesHolder;
	}

	protected PieceCtrl CurrentPiece() {
		foreach (Transform child in PiecesHolder().transform) {
			var pieceCtrl = child.GetComponent<PieceCtrl>();
			if (pieceCtrl && pieceCtrl.IsCurrent()) {
				return pieceCtrl;
			}
		}
		return null;
	}

	protected List<Vector3> FullCoords() {
		var result = new List<Vector3>();

		foreach (Transform child in PiecesHolder().transform) {
			var pieceCtrl = child.GetComponent<PieceCtrl>();
			if (pieceCtrl.IsFull()) {
				result.AddRange(pieceCtrl.PartPositions());
			}
		}
		
		return result;
	}

	protected bool IsCoordFree(Vector3 coord) {
		return FullCoords().All(fullCoord => {
			return Vector3.Distance(coord, fullCoord) > CollisionThreshold();
		});
	}

	public float CollisionThreshold() {
		return PiecesHolder().transform.GetChild(0).GetComponent<PieceCtrl>().Height() * 0.5f;
	}

	protected bool IsWithinGridBounds(Vector3 coord) {
		return GetComponent<GridCtrl>().GridBoundaries.Contains(coord);
	}

	protected bool IsWithinSpawnBounds(Vector3 coord) {
		return GetComponent<GridCtrl>().SpawnBoundaries.Contains(coord);
	}
}
