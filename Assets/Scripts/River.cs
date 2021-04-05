using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class River : MonoBehaviour
{
    public Flotsam flotsam;
    public Flow flow;


    HexGrid Grid { get; set; }
    List<HexCell> MainFlow { get; set; }
    List<HexCell> TopFlow { get; set; }
    List<HexCell> BottomFlow {get; set; }


    private void Awake() {
        Grid = GetComponent<HexGrid>();
    }

    private void Start() {
        SetFlows();
        StartCoroutine(GenerateFlow());
    }


    private IEnumerator GenerateFlow() {
        while (true) {
            yield return new WaitForSeconds(12f);

            foreach (var cell in MainFlow) {
                Vector3 cellPosition = cell.transform.position;
                Vector3 flotsamPosition = flotsam.transform.position;
                if (cellPosition.x < flotsamPosition.x - 10 && Vector3.Distance(cellPosition, flotsamPosition) < 100) {
                    Flow water1 = Instantiate(flow, new Vector3(cell.transform.position.x, Water.Instance.WaterLevel() + 30, cell.transform.position.z), Quaternion.identity);
                    Flow water2 = Instantiate(flow, new Vector3(cell.transform.position.x + 30, Water.Instance.WaterLevel(), cell.transform.position.z - 30), Quaternion.identity);
                    water1.Direction = water2.Direction = Vector3.right;
                }
            }

            foreach (var cell in TopFlow) {
                Vector3 cellPosition = cell.transform.position;
                Vector3 flotsamPosition = flotsam.transform.position;
                if (Vector3.Distance(cellPosition, flotsamPosition) < 200) {
                    Flow water1 = Instantiate(flow, new Vector3(cell.transform.position.x, Water.Instance.WaterLevel() + 30, cell.transform.position.z), Quaternion.identity);
                    Flow water2 = Instantiate(flow, new Vector3(cell.transform.position.x + 30, Water.Instance.WaterLevel(), cell.transform.position.z - 30), Quaternion.identity);
                    water1.Direction = water2.Direction = Vector3.back;
                }
            }
        }
    }

    private void SetFlows() {
        MainFlow = Grid.Cells().Where(c => c.coordinates.Z > 1 && c.coordinates.Z < Grid.cellCountZ - 1).ToList();
        TopFlow = Grid.Cells().Where(c => c.coordinates.Z == Grid.cellCountZ - 1).ToList();
        BottomFlow = Grid.UnderwaterCells().Where(c => c.coordinates.Z == 1).ToList();
    }
}
