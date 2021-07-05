using System;
using UnityEngine;

public class MeshDeformerInput : MonoBehaviour {
	
	[SerializeField] private float force = 10f;
	[SerializeField] private float forceOffset = 0.1f;

	private Camera _cameraMain;

	private void Awake()
	{
		_cameraMain = Camera.main;
	}

	private void Update () {
		if (Input.GetMouseButton(0)) {
			HandleInput();
		}
	}

	private void HandleInput () {
		Ray inputRay = _cameraMain.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(inputRay, out hit)) {
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
			if (deformer) {
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce(point, force);
			}
		}
	}
}