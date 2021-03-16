using UnityEngine;

public class HexCell : MonoBehaviour {

    public HexCoordinates coordinates;
    public Color color;

    [SerializeField]
	HexCell[] neighbors;


    // Unity

    private void Awake() {
        neighbors = new HexCell[6];
    }

    // Public


    public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

    public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
	}
}