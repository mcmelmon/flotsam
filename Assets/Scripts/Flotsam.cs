using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flotsam : MonoBehaviour
{

    public Flow flow;


    // Propertiies


    public Rigidbody Body { get; set; }
    // Unity

    private void Awake() {
        Body = GetComponent<Rigidbody>();
    }

    private void Start() {
        StartCoroutine(GenerateFlow());
    }

    // Private

    private IEnumerator GenerateFlow()
    {
        while (true) {
            yield return new WaitForSeconds(1f);
            Vector3 direction = Quaternion.AngleAxis(Random.Range(-120.0f, 120.0f), transform.position) * (Vector3.back * 3.5f);
            Vector3 point = transform.position + (direction * Random.Range(7,15));
            Flow waterjet = Instantiate(flow, new Vector3(point.x, transform.position.y, point.z), Quaternion.identity);
            waterjet.Flotsam = this;
        }
    }
}
