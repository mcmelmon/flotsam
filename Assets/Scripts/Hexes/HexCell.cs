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

            if (UIRect != null) {
                Vector3 uiPosition = UIRect.localPosition;
                uiPosition.z = elevation * -HexMetrics.elevationStep;
                UIRect.localPosition = uiPosition;
            }
        }}
    public HexCell[] Neighbors { get; set; }

    public RectTransform UIRect { get; set; }


    // Unity

    private void Awake() {
        Neighbors = new HexCell[6];
        Elevation = 0;
    }

    // Public

    public HexEdgeType GetEdgeType (HexDirection direction) {
		return HexMetrics.GetEdgeType(
			Elevation, Neighbors[(int)direction].Elevation
		);
	}

    public HexEdgeType GetEdgeType (HexCell otherCell) {
		return HexMetrics.GetEdgeType(
			elevation, otherCell.elevation
		);
	}

    public HexCell GetNeighbor (HexDirection direction) {
		return Neighbors[(int)direction];
	}

    public void SetNeighbor (HexDirection direction, HexCell cell) {
		Neighbors[(int)direction] = cell;
        cell.Neighbors[(int)direction.Opposite()] = this;
	}
}