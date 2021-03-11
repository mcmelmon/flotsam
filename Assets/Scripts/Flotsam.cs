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
}
