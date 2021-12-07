using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMaker
{
	// Mesh Values
	private List<Vector3> vertices = new List<Vector3>();
	private List<Vector3> normals = new List<Vector3>();
	private List<Vector2> uvs = new List<Vector2>();
	private List<Vector4> tangents = new List<Vector4>();
	private List<List<int>> subIndices = new List<List<int>>();

	public int VertCount
	{
		get
		{
			return vertices.Count;
		}
	}

	// Clears all arrays
	public void Clear()
	{
		vertices.Clear();
		normals.Clear();
		uvs.Clear();
		tangents.Clear();
		subIndices.Clear();
	}

	public void AddTriangle(Triangle triangle, int submesh)
	{
		AddTriangle(triangle.vertices, triangle.uvs, triangle.normals, triangle.tangents, submesh);
	}

	// Adds a new triangle to the return of GetMesh()
	public void AddTriangle(Vector3[] vertices, Vector2[] uvs, Vector3[] normals, int submesh = 0)
	{
		AddTriangle(vertices, uvs, normals, null, submesh);
	}

	// Same as above, but with tangents
	public void AddTriangle(Vector3[] vertices, Vector2[] uvs, Vector3[] normals, Vector4[] tangents, int submesh = 0)
	{
		int vertCount = this.vertices.Count;

		for (int i = 0; i < 3; i++)
		{
			this.vertices.Add(vertices[i]);
			this.normals.Add(normals[i]);
			this.uvs.Add(uvs[i]);
			if (tangents != null)
				this.tangents.Add(tangents[i]);
		}

		if (subIndices.Count < submesh + 1)
			for (int i = subIndices.Count; i < submesh + 1; i++)
				subIndices.Add(new List<int>());

		for (int i = 0; i < 3; i++)
			subIndices[submesh].Add(vertCount + i);
	}

	// Cleans up Double Vertices
	public void RemoveDoubles()
	{
		int dubCount = 0;

		Vector3 vertex = Vector3.zero;
		Vector3 normal = Vector3.zero;
		Vector2 uv = Vector2.zero;
		Vector4 tangent = Vector4.zero;

		int iterator = 0; ;
		while (iterator < VertCount)
		{
			vertex = vertices[iterator];
			normal = normals[iterator];
			uv = uvs[iterator];

			// look backwards for a match
			for (int backward_iterator = iterator - 1; backward_iterator >= 0; backward_iterator--)
			{
				if (vertex == vertices[backward_iterator] &&
					normal == normals[backward_iterator] &&
					uv == uvs[backward_iterator])
				{
					dubCount++;
					DoubleFound(backward_iterator, iterator);
					iterator--;
					break; // there should only be one
				}
			}
			iterator++;
		} // while

		Debug.LogFormat("Doubles found {0}", dubCount);

	}
	// Go through all indices an replace them
	private void DoubleFound(int first, int duplicate)
	{
		for (int h = 0; h < subIndices.Count; h++)
			for (int i = 0; i < subIndices[h].Count; i++)
				if (subIndices[h][i] > duplicate) // knock it down
					subIndices[h][i]--;
				else if (subIndices[h][i] == duplicate) // replace
					subIndices[h][i] = first;

		vertices.RemoveAt(duplicate);
		normals.RemoveAt(duplicate);
		uvs.RemoveAt(duplicate);

		if (tangents.Count > 0)
			tangents.RemoveAt(duplicate);
	}

	// Creates and returns a new mesh
	public Mesh GetMesh()
	{
		Mesh shape = new Mesh();
		shape.name = "Generated Mesh";
		shape.SetVertices(vertices);
		shape.SetNormals(normals);
		shape.SetUVs(0, uvs);
		shape.SetUVs(1, uvs);

		if (tangents.Count > 1)
			shape.SetTangents(tangents);

		shape.subMeshCount = subIndices.Count;

		for (int i = 0; i < subIndices.Count; i++)
			shape.SetTriangles(subIndices[i], i);

		return shape;
	}

#if UNITY_EDITOR
	/// <summary>
	/// Creates and returns a new mesh with generated lightmap uvs (Editor Only)
	/// </summary>
	public Mesh GetMesh_GenerateSecondaryUVSet()
	{

		Mesh shape = GetMesh();

		// for light mapping
		UnityEditor.Unwrapping.GenerateSecondaryUVSet(shape);

		return shape;
	}

	/// <summary>
	/// Creates and returns a new mesh with generated lightmap uvs (Editor Only)
	/// </summary>
	public Mesh GetMesh_GenerateSecondaryUVSet(UnityEditor.UnwrapParam param)
	{

		Mesh shape = GetMesh();

		// for light mapping
		UnityEditor.Unwrapping.GenerateSecondaryUVSet(shape, param);

		return shape;
	}
#endif
}