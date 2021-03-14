using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flotsam : MonoBehaviour
{

    // Propertiies


    public Rigidbody Body { get; set; }
    // Unity

    private void Awake() {
        Body = GetComponent<Rigidbody>();
    }

    private void Update() {
        Vector3 backward = transform.TransformDirection(-Vector3.forward) * 100;
        Debug.DrawRay(transform.position, backward, Color.green);
    }
}
