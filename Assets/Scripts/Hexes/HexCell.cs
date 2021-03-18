using UnityEngine;

public class HexCell : MonoBehaviour {
    int elevation;

    // Properties

    public Color Color { get; set; }
    public HexCoordinates Coordinates { get; set; }
    public int Elevation {
        get { return elevation; } 
        set {
            elevation = value;
			Vector3 position = transform.localPosition;
			position.y = value * HexMetrics.elevationStep;
			transform.localPosition = position;
        }}
    public HexCell[] Neighbors { get; set; }


    // Unity

    private void Awake() {
        Neighbors = new HexCell[6];
        Elevation = 0;
    }

    // Public


    public HexCell GetNeighbor (HexDirection direction) {
		return Neighbors[(int)direction];
	}

    public void SetNeighbor (HexDirection direction, HexCell cell) {
		Neighbors[(int)direction] = cell;
        cell.Neighbors[(int)direction.Opposite()] = this;
	}
}