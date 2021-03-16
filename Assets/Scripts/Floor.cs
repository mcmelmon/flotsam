using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Floor : MonoBehaviour {
    public int xSize, ySize;

    private Mesh Mesh { get; set; }
    private Vector3[] Vertices { get; set; }

    private void Awake() {
        Vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        GetComponent<MeshFilter>().mesh = Mesh = new Mesh();
        Generate();    
    }


    private void OnDrawGizmos() {
        if (Vertices == null || Vertices.Length == 0) return;

        Gizmos.color = Color.black;

        for (int i = 0; i < Vertices.Length; i++) {
            Gizmos.DrawSphere(Vertices[i], 0.1f);
        }
    }


    // Private


    private void Generate() {
        Vector2[] uv = new Vector2[Vertices.Length];
        Vector4[] tangents = new Vector4[Vertices.Length];
		Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				Vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
			}
		}
		Mesh.vertices = Vertices;
        Mesh.uv = uv;
        Mesh.tangents = tangents;

		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}
		Mesh.triangles = triangles;
        Mesh.RecalculateNormals();
    }
}