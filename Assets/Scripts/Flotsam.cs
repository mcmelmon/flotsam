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


    private void FixedUpdate() {
        Propel();
    }

    // Priate


    private void Propel()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Body.AddForce(forward * 3f);
    }
}
