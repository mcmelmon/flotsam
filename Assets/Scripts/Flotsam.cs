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
    public Vector3 Target { get; set; }


    // Unity

    private void Awake() {
        Body = GetComponent<Rigidbody>();
    }

    private void Start() {
        PlaceAboveWater();
        
    }

    void FixedUpdate()
    {
        Propel();
    }

    // Private

    private void PlaceAboveWater() {
        HexCell center = riverbed.GetCenterOfMap();
        List<HexCell> underwaterCells = riverbed.UnderwaterCells().OrderBy(c => (c.transform.position - center.transform.position).sqrMagnitude).ToList();
        HexCell start = underwaterCells.First();
        transform.position = new Vector3(start.transform.position.x, transform.position.y, start.transform.position.z);
    }

    private void Propel()
    {
        Body.AddForce((Target - transform.position).normalized * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }


    private IEnumerator SelectTarget()
    {
        while (true) {
            Vector3 angle = Quaternion.AngleAxis(Random.Range(-180.0f, 180.0f), transform.position) * Vector3.forward;
            Target = transform.position + (angle * 50);
            yield return new WaitForSeconds(10f);
        }
    }
}
