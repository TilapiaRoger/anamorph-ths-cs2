using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Every property should have 3 elements
public class Triangle
{
	public Vector3[] vertices;
	public Vector2[] uvs;
	public Vector3[] normals;
	public Vector4[] tangents;

	public Triangle(Vector3[] vertices = null, Vector2[] uvs = null, Vector3[] normals = null, Vector4[] tangents = null)
	{
		this.vertices = vertices;
		this.uvs = uvs;
		this.normals = normals;
		this.tangents = tangents;
	}
}
