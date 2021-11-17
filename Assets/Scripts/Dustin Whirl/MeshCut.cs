using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCut
{
    private static Plane blade;
    private static Mesh victimMesh;

    // Caching
    private static MeshMaker leftSide = new MeshMaker();
    private static MeshMaker rightSide = new MeshMaker();
    private static Triangle triangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    private static List<Vector3> newVerticesCache = new List<Vector3>();
    private static bool[] isLeftSideCache = new bool[3];
    private static int capMatSub = 1;

    // Yeah
    public static GameObject[] Cut(GameObject victim, Vector3 anchorPoint, Vector3 normalDirection, Material capMaterial)
    {
        // set the blade relative to victim
        blade = new Plane(victim.transform.InverseTransformDirection(-normalDirection), victim.transform.InverseTransformPoint(anchorPoint));

        // get the victims mesh
        victimMesh = victim.GetComponent<MeshFilter>().mesh;

        // two new meshes
        leftSide.Clear();
        rightSide.Clear();
        newVerticesCache.Clear();

        var mesh_vertices = victimMesh.vertices;
        var mesh_normals = victimMesh.normals;
        var mesh_uvs = victimMesh.uv;
        if (mesh_uvs != null && mesh_uvs.Length == 0)
        {
            mesh_uvs = new Vector2[mesh_vertices.Length];
            for (int i = 0; i < mesh_uvs.Length; i++)
                mesh_uvs[i] = new Vector2(mesh_vertices[i].x, mesh_vertices[i].z);
        }
        var mesh_tangents = victimMesh.tangents;
        if (mesh_tangents != null && mesh_tangents.Length == 0)
            mesh_tangents = null;

        // go through the submeshes
        for (int submeshIterator = 0; submeshIterator < victimMesh.subMeshCount; submeshIterator++)
        {
            // Triangles
            var indices = victimMesh.GetTriangles(submeshIterator);

            for (int i = 0; i < indices.Length; i += 3)
            {
                for(int j = 0; i < 3; i++)
                {
                    // Vertices
                    triangleCache.vertices[i] = mesh_vertices[indices[i + j]];
                    // Normals
                    triangleCache.normals[i] = mesh_normals[indices[i + j]];
                    // UVs
                    triangleCache.uvs[i] = mesh_uvs[indices[i + j]];
                    // Tangents
                    triangleCache.tangents[i] = (mesh_tangents != null) ? mesh_tangents[indices[i + j]] : Vector4.zero;
                    // Which side are the vertices on
                    isLeftSideCache[i] = blade.GetSide(mesh_vertices[indices[i + j]]); 
                }

                // whole triangle
                if (isLeftSideCache[0] == isLeftSideCache[1] && isLeftSideCache[0] == isLeftSideCache[2])
                    if (isLeftSideCache[0])
                        leftSide.AddTriangle(triangleCache, submeshIterator);
                    else
                        rightSide.AddTriangle(triangleCache, submeshIterator);
                else // cut the triangle
                    Cut_this_Face(ref triangleCache, submeshIterator);
            }
        }

        // The capping Material will be at the end
        Material[] mats = victim.GetComponent<MeshRenderer>().sharedMaterials;
        if (mats[mats.Length - 1].name != capMaterial.name)
        {
            Material[] newMats = new Material[mats.Length + 1];
            mats.CopyTo(newMats, 0);
            newMats[mats.Length] = capMaterial;
            mats = newMats;
        }

        capMatSub = mats.Length - 1; // for later use

        // cap the opennings
        Cap_the_Cut();

        // Left Mesh
        Mesh left_HalfMesh = leftSide.GetMesh();
        left_HalfMesh.name = "Split Mesh Left";

        // Right Mesh
        Mesh right_HalfMesh = rightSide.GetMesh();
        right_HalfMesh.name = "Split Mesh Right";

        // assign the game objects

        victim.name = "left side";
        victim.GetComponent<MeshFilter>().mesh = left_HalfMesh;

        GameObject leftSideObj = victim;

        GameObject rightSideObj = new GameObject("right side", typeof(MeshFilter), typeof(MeshRenderer));
        rightSideObj.transform.position = victim.transform.position;
        rightSideObj.transform.rotation = victim.transform.rotation;
        rightSideObj.GetComponent<MeshFilter>().mesh = right_HalfMesh;

        if (victim.transform.parent != null)
        {
            rightSideObj.transform.parent = victim.transform.parent;
        }

        rightSideObj.transform.localScale = victim.transform.localScale;


        // assign mats
        leftSideObj.GetComponent<MeshRenderer>().materials = mats;
        rightSideObj.GetComponent<MeshRenderer>().materials = mats;

        return new GameObject[] { leftSideObj, rightSideObj };
    }

    #region Cutting
    // Caching
    private static Triangle leftTriangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    private static Triangle rightTriangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    private static Triangle newTriangleCache = new Triangle(new Vector3[3], new Vector2[3], new Vector3[3], new Vector4[3]);
    // Functions
    private static void Cut_this_Face(ref Triangle triangle, int submesh)
    {
        for(int i = 0; i < 3; i++)
            isLeftSideCache[i] = blade.GetSide(triangle.vertices[i]); // true = left

        int leftCount = 0;
        int rightCount = 0;

        for (int i = 0; i < 3; i++)
            if (isLeftSideCache[i]) // Left
            {
                leftTriangleCache.vertices[leftCount] = triangle.vertices[i];
                leftTriangleCache.uvs[leftCount] = triangle.uvs[i];
                leftTriangleCache.normals[leftCount] = triangle.normals[i];
                leftTriangleCache.tangents[leftCount] = triangle.tangents[i];

                leftCount++;
            }
            else // Right
            {
                rightTriangleCache.vertices[rightCount] = triangle.vertices[i];
                rightTriangleCache.uvs[rightCount] = triangle.uvs[i];
                rightTriangleCache.normals[rightCount] = triangle.normals[i];
                rightTriangleCache.tangents[rightCount] = triangle.tangents[i];

                rightCount++;
            }

        // find the new triangles X 3
        // first the new vertices

        // Gives a triangle with the solo point as first
        if (leftCount == 1)
        {
            triangleCache.vertices[0] = leftTriangleCache.vertices[0];
            triangleCache.uvs[0] = leftTriangleCache.uvs[0];
            triangleCache.normals[0] = leftTriangleCache.normals[0];
            triangleCache.tangents[0] = leftTriangleCache.tangents[0];

            triangleCache.vertices[1] = rightTriangleCache.vertices[0];
            triangleCache.uvs[1] = rightTriangleCache.uvs[0];
            triangleCache.normals[1] = rightTriangleCache.normals[0];
            triangleCache.tangents[1] = rightTriangleCache.tangents[0];

            triangleCache.vertices[2] = rightTriangleCache.vertices[1];
            triangleCache.uvs[2] = rightTriangleCache.uvs[1];
            triangleCache.normals[2] = rightTriangleCache.normals[1];
            triangleCache.tangents[2] = rightTriangleCache.tangents[1];
        }
        else // rightCount == 1
        {
            triangleCache.vertices[0] = rightTriangleCache.vertices[0];
            triangleCache.uvs[0] = rightTriangleCache.uvs[0];
            triangleCache.normals[0] = rightTriangleCache.normals[0];
            triangleCache.tangents[0] = rightTriangleCache.tangents[0];

            triangleCache.vertices[1] = leftTriangleCache.vertices[0];
            triangleCache.uvs[1] = leftTriangleCache.uvs[0];
            triangleCache.normals[1] = leftTriangleCache.normals[0];
            triangleCache.tangents[1] = leftTriangleCache.tangents[0];

            triangleCache.vertices[2] = leftTriangleCache.vertices[1];
            triangleCache.uvs[2] = leftTriangleCache.uvs[1];
            triangleCache.normals[2] = leftTriangleCache.normals[1];
            triangleCache.tangents[2] = leftTriangleCache.tangents[1];
        }

        // now to find the intersection points between the solo point and the others
        float distance = 0;
        float normalizedDistance = 0.0f;
        Vector3 edgeVector = Vector3.zero; // contains edge length and direction
        for(int i = 0; i < 2; i++)
        {
            edgeVector = triangleCache.vertices[i + 1] - triangleCache.vertices[0];
            blade.Raycast(new Ray(triangleCache.vertices[0], edgeVector.normalized), out distance);

            normalizedDistance = distance / edgeVector.magnitude;
            newTriangleCache.vertices[i] = Vector3.Lerp(triangleCache.vertices[0], triangleCache.vertices[i + 1], normalizedDistance);
            newTriangleCache.uvs[i] = Vector2.Lerp(triangleCache.uvs[0], triangleCache.uvs[i + 1], normalizedDistance);
            newTriangleCache.normals[i] = Vector3.Lerp(triangleCache.normals[0], triangleCache.normals[i + 1], normalizedDistance);
            newTriangleCache.tangents[i] = Vector4.Lerp(triangleCache.tangents[0], triangleCache.tangents[i + 1], normalizedDistance);
        }

        if (newTriangleCache.vertices[0] != newTriangleCache.vertices[1])
        {
            //tracking newly created points
            newVerticesCache.Add(newTriangleCache.vertices[0]);
            newVerticesCache.Add(newTriangleCache.vertices[1]);
        }
        // make the new triangles
        // one side will get 1 the other will get 2

        if (leftCount == 1)
        {
            // first one on the left
            triangleCache.vertices[0] = leftTriangleCache.vertices[0];
            triangleCache.uvs[0] = leftTriangleCache.uvs[0];
            triangleCache.normals[0] = leftTriangleCache.normals[0];
            triangleCache.tangents[0] = leftTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // check if it is facing the right way
            NormalCheck(ref triangleCache);

            // add it
            leftSide.AddTriangle(triangleCache, submesh);

            // other two on the right
            triangleCache.vertices[0] = rightTriangleCache.vertices[0];
            triangleCache.uvs[0] = rightTriangleCache.uvs[0];
            triangleCache.normals[0] = rightTriangleCache.normals[0];
            triangleCache.tangents[0] = rightTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // check if it is facing the right way
            NormalCheck(ref triangleCache);

            // add it
            rightSide.AddTriangle(triangleCache, submesh);

            // third
            triangleCache.vertices[0] = rightTriangleCache.vertices[0];
            triangleCache.uvs[0] = rightTriangleCache.uvs[0];
            triangleCache.normals[0] = rightTriangleCache.normals[0];
            triangleCache.tangents[0] = rightTriangleCache.tangents[0];

            triangleCache.vertices[1] = rightTriangleCache.vertices[1];
            triangleCache.uvs[1] = rightTriangleCache.uvs[1];
            triangleCache.normals[1] = rightTriangleCache.normals[1];
            triangleCache.tangents[1] = rightTriangleCache.tangents[1];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // check if it is facing the right way
            NormalCheck(ref triangleCache);

            // add it
            rightSide.AddTriangle(triangleCache, submesh);
        }
        else
        {
            // first one on the right
            triangleCache.vertices[0] = rightTriangleCache.vertices[0];
            triangleCache.uvs[0] = rightTriangleCache.uvs[0];
            triangleCache.normals[0] = rightTriangleCache.normals[0];
            triangleCache.tangents[0] = rightTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // check if it is facing the right way
            NormalCheck(ref triangleCache);

            // add it
            rightSide.AddTriangle(triangleCache, submesh);

            // other two on the left
            triangleCache.vertices[0] = leftTriangleCache.vertices[0];
            triangleCache.uvs[0] = leftTriangleCache.uvs[0];
            triangleCache.normals[0] = leftTriangleCache.normals[0];
            triangleCache.tangents[0] = leftTriangleCache.tangents[0];

            triangleCache.vertices[1] = newTriangleCache.vertices[0];
            triangleCache.uvs[1] = newTriangleCache.uvs[0];
            triangleCache.normals[1] = newTriangleCache.normals[0];
            triangleCache.tangents[1] = newTriangleCache.tangents[0];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // check if it is facing the right way
            NormalCheck(ref triangleCache);

            // add it
            leftSide.AddTriangle(triangleCache, submesh);

            // third
            triangleCache.vertices[0] = leftTriangleCache.vertices[0];
            triangleCache.uvs[0] = leftTriangleCache.uvs[0];
            triangleCache.normals[0] = leftTriangleCache.normals[0];
            triangleCache.tangents[0] = leftTriangleCache.tangents[0];

            triangleCache.vertices[1] = leftTriangleCache.vertices[1];
            triangleCache.uvs[1] = leftTriangleCache.uvs[1];
            triangleCache.normals[1] = leftTriangleCache.normals[1];
            triangleCache.tangents[1] = leftTriangleCache.tangents[1];

            triangleCache.vertices[2] = newTriangleCache.vertices[1];
            triangleCache.uvs[2] = newTriangleCache.uvs[1];
            triangleCache.normals[2] = newTriangleCache.normals[1];
            triangleCache.tangents[2] = newTriangleCache.tangents[1];

            // check if it is facing the right way
            NormalCheck(ref triangleCache);

            // add it
            leftSide.AddTriangle(triangleCache, submesh);
        }
    }
    #endregion

    #region Capping
    // Caching
    private static List<int> capUsedIndicesCache = new List<int>();
    private static List<int> capPolygonIndicesCache = new List<int>();
    // Functions
    private static void Cap_the_Cut()
    {

        capUsedIndicesCache.Clear();
        capPolygonIndicesCache.Clear();

        // find the needed polygons
        // the cut faces added new vertices by 2 each time to make an edge
        // if two edges contain the same Vector3 point, they are connected
        for (int i = 0; i < newVerticesCache.Count; i += 2)
        {
            // check the edge
            if (!capUsedIndicesCache.Contains(i)) // if it has one, it has this edge
            {
                //new polygon started with this edge
                capPolygonIndicesCache.Clear();
                capPolygonIndicesCache.Add(i);
                capPolygonIndicesCache.Add(i + 1);

                capUsedIndicesCache.Add(i);
                capUsedIndicesCache.Add(i + 1);

                Vector3 connectionPointLeft = newVerticesCache[i];
                Vector3 connectionPointRight = newVerticesCache[i + 1];
                bool isDone = false;

                // look for more edges
                while (!isDone)
                {
                    isDone = true;

                    // loop through edges
                    for (int index = 0; index < newVerticesCache.Count; index += 2)
                    {   // if it has one, it has this edge
                        if (!capUsedIndicesCache.Contains(index))
                        {
                            Vector3 nextPoint1 = newVerticesCache[index];
                            Vector3 nextPoint2 = newVerticesCache[index + 1];

                            // check for next point in the chain
                            if (connectionPointLeft == nextPoint1 ||
                                connectionPointLeft == nextPoint2 ||
                                connectionPointRight == nextPoint1 ||
                                connectionPointRight == nextPoint2)
                            {
                                capUsedIndicesCache.Add(index);
                                capUsedIndicesCache.Add(index + 1);

                                // add the other
                                if (connectionPointLeft == nextPoint1)
                                {
                                    capPolygonIndicesCache.Insert(0, index + 1);
                                    connectionPointLeft = newVerticesCache[index + 1];
                                }
                                else if (connectionPointLeft == nextPoint2)
                                {
                                    capPolygonIndicesCache.Insert(0, index);
                                    connectionPointLeft = newVerticesCache[index];
                                }
                                else if (connectionPointRight == nextPoint1)
                                {
                                    capPolygonIndicesCache.Add(index + 1);
                                    connectionPointRight = newVerticesCache[index + 1];
                                }
                                else if (connectionPointRight == nextPoint2)
                                {
                                    capPolygonIndicesCache.Add(index);
                                    connectionPointRight = newVerticesCache[index];
                                }

                                isDone = false;
                            }
                        }
                    }
                }// while isDone = False

                // check if the link is closed
                // first == last
                if (newVerticesCache[capPolygonIndicesCache[0]] == newVerticesCache[capPolygonIndicesCache[capPolygonIndicesCache.Count - 1]])
                    capPolygonIndicesCache[capPolygonIndicesCache.Count - 1] = capPolygonIndicesCache[0];
                else
                    capPolygonIndicesCache.Add(capPolygonIndicesCache[0]);

                // cap
                FillCap_Method1(capPolygonIndicesCache);
            }
        }
    }
    private static void FillCap_Method1(List<int> indices)
    {
        // center of the cap
        Vector3 center = Vector3.zero;
        foreach (var index in indices)
            center += newVerticesCache[index];

        center = center / indices.Count;

        // you need an axis based on the cap
        Vector3 upward = Vector3.zero;
        // 90 degree turn
        upward.x = blade.normal.y;
        upward.y = -blade.normal.x;
        upward.z = blade.normal.z;
        Vector3 left = Vector3.Cross(blade.normal, upward);

        Vector3 link = Vector3.zero;
        Vector3 displacement = Vector3.zero;
        Vector2[] newUVs = new Vector2[3];
        for (int i = 0; i < 3; i++)
            newUVs[i] = Vector2.zero;

        // indices should be in order like a closed chain

        // go through edges and eliminate by creating triangles with connected edges
        // each new triangle removes 2 edges but creates 1 new edge
        // keep the chain in order
        for(int j = 0; indices.Count > 2; j = (j + 1) % indices.Count)
        {
            for(int i = 0; i < 3; i++)
            {
                link = newVerticesCache[indices[(j + i) % indices.Count]];
                displacement = link - center;
                newUVs[i] = Vector3.zero;
                newUVs[i].x = 0.5f + Vector3.Dot(displacement, left);
                newUVs[i].y = 0.5f + Vector3.Dot(displacement, upward);

                // add triangle
                newTriangleCache.vertices[i] = link;
                newTriangleCache.uvs[i] = newUVs[i];
                newTriangleCache.normals[i] = -blade.normal;
                newTriangleCache.tangents[i] = Vector4.zero;
            }

            // add to left side
            NormalCheck(ref newTriangleCache);

            leftSide.AddTriangle(newTriangleCache, capMatSub);

            // add to right side
            for(int i = 0; i < 3; i++)
                newTriangleCache.normals[i] = blade.normal;

            NormalCheck(ref newTriangleCache);

            rightSide.AddTriangle(newTriangleCache, capMatSub);

            // adjust indices by removing the middle link
            indices.RemoveAt((j + 1) % indices.Count);
        }
    }

    private static void FillCap_Method2(List<int> indices)
    {

        // center of the cap
        Vector3 center = Vector3.zero;
        foreach (var index in indices)
            center += newVerticesCache[index];

        center = center / indices.Count;

        // you need an axis based on the cap
        Vector3 upward = Vector3.zero;
        // 90 degree turn
        upward.x = blade.normal.y;
        upward.y = -blade.normal.x;
        upward.z = blade.normal.z;
        Vector3 left = Vector3.Cross(blade.normal, upward);

        Vector3 displacement = Vector3.zero;
        Vector2[] newUVs = new Vector2[2];

        for (int i = 0; i < indices.Count - 1; i++)
        {
            for(int j = 0; i < 2; j++)
            {
                displacement = newVerticesCache[indices[i + j]] - center;
                newUVs[i] = Vector3.zero;
                newUVs[i].x = 0.5f + Vector3.Dot(displacement, left);
                newUVs[i].y = 0.5f + Vector3.Dot(displacement, upward);

                newTriangleCache.vertices[i] = newVerticesCache[indices[i + j]];
                newTriangleCache.uvs[i] = newUVs[i];
            }

            newTriangleCache.vertices[2] = center;
            newTriangleCache.uvs[2] = new Vector2(0.5f, 0.5f);

            for (int j = 0; j < 3; j++)
            {
                newTriangleCache.normals[j] = -blade.normal;
                newTriangleCache.tangents[j] = Vector4.zero;
            }

            NormalCheck(ref newTriangleCache);

            leftSide.AddTriangle(newTriangleCache, capMatSub);

            for(int j = 0; j < 3; j++)
                newTriangleCache.normals[j] = blade.normal;

            NormalCheck(ref newTriangleCache);

            rightSide.AddTriangle(newTriangleCache, capMatSub);
        }
    }
    #endregion

    #region Misc.
    private static void NormalCheck(ref Triangle triangle)
    {
        Vector3 crossProduct = Vector3.Cross(triangle.vertices[1] - triangle.vertices[0], triangle.vertices[2] - triangle.vertices[0]);
        Vector3 averageNormal = (triangle.normals[0] + triangle.normals[1] + triangle.normals[2]) / 3.0f;
        float dotProduct = Vector3.Dot(averageNormal, crossProduct);
        if (dotProduct < 0)
        {
            Vector3 temp = triangle.vertices[2];
            triangle.vertices[2] = triangle.vertices[0];
            triangle.vertices[0] = temp;

            temp = triangle.normals[2];
            triangle.normals[2] = triangle.normals[0];
            triangle.normals[0] = temp;

            Vector2 temp2 = triangle.uvs[2];
            triangle.uvs[2] = triangle.uvs[0];
            triangle.uvs[0] = temp2;

            Vector4 temp3 = triangle.tangents[2];
            triangle.tangents[2] = triangle.tangents[0];
            triangle.tangents[0] = temp3;
        }
    }
    #endregion
}
