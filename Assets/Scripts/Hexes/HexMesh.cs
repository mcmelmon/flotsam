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


	private void AddQuad (Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
		int vertexIndex = Vertices.Count;
		Vertices.Add(v1);
		Vertices.Add(v2);
		Vertices.Add(v3);
		Vertices.Add(v4);
		Triangles.Add(vertexIndex);
		Triangles.Add(vertexIndex + 2);
		Triangles.Add(vertexIndex + 1);
		Triangles.Add(vertexIndex + 1);
		Triangles.Add(vertexIndex + 2);
		Triangles.Add(vertexIndex + 3);
	}

	private void AddQuadColor (Color c1, Color c2, Color c3, Color c4) {
		Colors.Add(c1);
		Colors.Add(c2);
		Colors.Add(c3);
		Colors.Add(c4);
	}

	private void AddQuadColor (Color c1, Color c2) {
		Colors.Add(c1);
		Colors.Add(c1);
		Colors.Add(c2);
		Colors.Add(c2);
	}

    private void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = Vertices.Count;
		Vertices.Add(v1);
		Vertices.Add(v2);
		Vertices.Add(v3);
		Triangles.Add(vertexIndex);
		Triangles.Add(vertexIndex + 1);
		Triangles.Add(vertexIndex + 2);
	}

	private void AddTriangleColor (Color color) {
		Colors.Add(color);
		Colors.Add(color);
		Colors.Add(color);
	}

	private void AddTriangleColor (Color c1, Color c2, Color c3) {
		Colors.Add(c1);
		Colors.Add(c2);
		Colors.Add(c3);
	}

	private void Triangulate (HexCell cell) {
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++) {
			Triangulate(d, cell);
		}
	}

	private void Triangulate (HexDirection direction, HexCell cell) {
		Vector3 center = cell.transform.localPosition;
		Vector3 v1 = center + HexMetrics.GetFirstSolidCorner(direction);
		Vector3 v2 = center + HexMetrics.GetSecondSolidCorner(direction);

		AddTriangle(center, v1, v2);
		AddTriangleColor(cell.Color);

		if (direction <= HexDirection.SE) {
			TriangulateConnection(direction, cell, v1, v2);
		}
	}

	private void TriangulateConnection (
		HexDirection direction, HexCell cell, Vector3 v1, Vector3 v2
	) {
		HexCell neighbor = cell.GetNeighbor(direction);
		if (neighbor == null) {
			return;
		}
		
		Vector3 bridge = HexMetrics.GetBridge(direction);
		Vector3 v3 = v1 + bridge;
		Vector3 v4 = v2 + bridge;

		AddQuad(v1, v2, v3, v4);
		AddQuadColor(cell.Color, neighbor.Color);

		HexCell nextNeighbor = cell.GetNeighbor(direction.Next());
		if (direction <= HexDirection.E && nextNeighbor != null) {
			AddTriangle(v2, v4, v2 + HexMetrics.GetBridge(direction.Next()));
			AddTriangleColor(cell.Color, neighbor.Color, nextNeighbor.Color);
		}

	}
}