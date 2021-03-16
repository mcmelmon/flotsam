using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

    // Properties

    private MeshCollider Collider { get; set; }
    private List<Color> Colors { get; set; }
    private Mesh Mesh { get; set; }

    private List<int> Triangles { get; set; }
    private List<Vector3> Vertices { get; set; } 


    // Unity

	void Awake () {
		GetComponent<MeshFilter>().mesh = Mesh = new Mesh();
        Colors = new List<Color>();
        Collider = gameObject.AddComponent<MeshCollider>();
		Vertices = new List<Vector3>();
		Triangles = new List<int>();
	}


    // Public


	public void Triangulate (HexCell[] cells) {
        Colors.Clear();
		Mesh.Clear();
		Vertices.Clear();
		Triangles.Clear();
		
        for (int i = 0; i < cells.Length; i++) {
			Triangulate(cells[i]);
		}

		Mesh.vertices = Vertices.ToArray();
		Mesh.triangles = Triangles.ToArray();
        Mesh.colors = Colors.ToArray();
		Mesh.RecalculateNormals();
		Collider.sharedMesh = Mesh;
	}


    // Private

    void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = Vertices.Count;
		Vertices.Add(v1);
		Vertices.Add(v2);
		Vertices.Add(v3);
		Triangles.Add(vertexIndex);
		Triangles.Add(vertexIndex + 1);
		Triangles.Add(vertexIndex + 2);
	}

    void AddTriangleColor (Color color) {
		Colors.Add(color);
		Colors.Add(color);
		Colors.Add(color);
	}

	void Triangulate (HexCell cell) {
		Vector3 center = cell.transform.localPosition;
		for (int i = 0; i < 6; i++) {
			AddTriangle(
				center,
				center + HexMetrics.corners[i],
				center + HexMetrics.corners[i + 1]
			);
            AddTriangleColor(cell.color);
		}
	}
}