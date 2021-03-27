using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flotsam : MonoBehaviour
{

    public Flow flow;
    public HexGrid riverbed;


    // Propertiies


    public Rigidbody Body { get; set; }
    // Unity

    private void Awake() {
        Body = GetComponent<Rigidbody>();
    }

    private void Start() {
        PlaceAboveWater();
        StartCoroutine(GenerateFlow());
    }

    // Private

    private IEnumerator GenerateFlow() {
        while (true) {
            yield return new WaitForSeconds(1f);
            Vector3 direction = Quaternion.AngleAxis(Random.Range(-120.0f, 120.0f), transform.position) * (Vector3.back * 3.5f);
            Vector3 point = transform.position + (direction * Random.Range(7,15));
            Flow waterjet = Instantiate(flow, new Vector3(point.x, transform.position.y, point.z), Quaternion.identity);
            waterjet.Flotsam = this;
        }
    }

    private void PlaceAboveWater() {
        HexCell center = riverbed.GetCenterOfMap();
        List<HexCell> underwaterCells = riverbed.UnderwaterCells().OrderBy(c => (c.transform.position - center.transform.position).sqrMagnitude).ToList();
        HexCell start = underwaterCells.First();
        transform.position = new Vector3(start.transform.position.x, transform.position.y, start.transform.position.z);
    }
}
