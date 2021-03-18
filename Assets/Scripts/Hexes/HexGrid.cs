using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;
	public HexCell cellPrefab;
    public Text cellLabelPrefab;
    public Color defaultColor = Color.white;
	public Color touchedColor = Color.magenta;
    public Texture2D noiseSource;

    // Properties


    public Canvas Canvas { get; set; }
    public HexCell[] Cells { get; set; }
    public HexMesh Mesh { get; set; }


    // Unity


	void Awake () {
		Cells = new HexCell[height * width];
        Canvas = GetComponentInChildren<Canvas>();
        Mesh = GetComponentInChildren<HexMesh>();

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}

        HexMetrics.noiseSource = noiseSource;
	}

    private void OnEnable () {
		HexMetrics.noiseSource = noiseSource;
	}


    private void Start() {
        Mesh.Triangulate(Cells);
    }


    // Public

    public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		return Cells[index];
	}

    public void Refresh () {
		Mesh.Triangulate(Cells);
	}


    // Private
	
	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z /2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = Cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.Color = defaultColor;

        if (x > 0) {
			cell.SetNeighbor(HexDirection.W, Cells[i - 1]);
		}

        if (z > 0) {
            if ((z & 1) == 0) {
				cell.SetNeighbor(HexDirection.SE, Cells[i - width]);
                if (x > 0) {
					cell.SetNeighbor(HexDirection.SW, Cells[i - width - 1]);
				}
			} else {
				cell.SetNeighbor(HexDirection.SW, Cells[i - width]);
				if (x < width - 1) {
					cell.SetNeighbor(HexDirection.SE, Cells[i - width + 1]);
				}
			}
		}

        Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(Canvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.Coordinates.ToStringOnSeparateLines();

        cell.UIRect = label.rectTransform;
	}
}