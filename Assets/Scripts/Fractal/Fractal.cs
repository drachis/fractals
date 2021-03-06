﻿using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour
{
	public Mesh mesh;
	public Material material;
	public int maxDepth;
	public Mesh[] meshes;
	public float childScale;
	public float maxRotationSpeed;
	public float spawnProbability;
	public float maxTwist;

	private float rotationSpeed;
	private int depth;
	private Material[,] materials;
	private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
	private static Quaternion[] childOrientations = {
        Quaternion.identity, 
        Quaternion.Euler (0f, 0f, -90f),
        Quaternion.Euler (0f, 0f, 90f),
        Quaternion.Euler (90f, 0f, 0f),
        Quaternion.Euler (-90f, 0f, 0f)
    };


	void Start ()
	{
		rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);
		transform.Rotate (Random.Range (-maxTwist, maxTwist), 0f, 0f);
		if (materials == null) {
			InitializeMaterials ();
		}
		gameObject.AddComponent<MeshFilter> ().mesh = meshes [Random.Range (0, meshes.Length)];
		gameObject.AddComponent<MeshRenderer> ().material = materials [depth, Random.Range (0, 2)];
		if (depth < maxDepth) {
			StartCoroutine (CreateChildren ());
		}
	}

	private void InitializeMaterials ()
	{ 
		materials = new Material[maxDepth + 1, 2];
		for (int i = 0; i <= maxDepth; i ++) {
			float t = Mathf.Pow ((float)i / (maxDepth - 1f), 2f);
			materials [i, 0] = new Material (material);
			materials [i, 0].color = Color.Lerp (Color.yellow, Color.green, t);
			materials [i, 1] = new Material (material);
			materials [i, 1].color = Color.Lerp (Color.white, Color.cyan, t);
			                                     
		}
		materials [maxDepth, 0].color = Color.magenta;
		materials [maxDepth, 1].color = Color.green;
	}

	private IEnumerator CreateChildren ()
	{
		for (int i = 0; i < childOrientations.Length; i++) {
			if (Random.value < spawnProbability) {
				yield return new WaitForSeconds (Random.Range (0.1f, 0.5f));
				new GameObject ("Fractal Child").AddComponent<Fractal> ().Initialize (this, i);
			}
		}
	}

	private void Initialize (Fractal parent, int childIndex)
	{
		meshes = parent.meshes;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		spawnProbability = parent.spawnProbability;
		maxRotationSpeed = parent.maxRotationSpeed;
		maxTwist = parent.maxTwist;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections [childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations [childIndex];
	}


	void FixedUpdate ()
	{
		transform.Rotate (0f, rotationSpeed, 0f);
	}
}
