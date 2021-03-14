using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{

    public float force = 1.5f;

    // Properties
    private Rigidbody Body { get; set; }

    public Flotsam Flotsam { get; set; }


    // Unity


    private void Awake() {
        Body = GetComponent<Rigidbody>();
    }


    private void Start() {
        StartCoroutine(Evaporate());
    }

    void FixedUpdate()
    {
        Propel();
    }


    // Private


    private IEnumerator Evaporate()
    {
        while (true) {
            yield return new WaitForSeconds(10f);
            Destroy(transform.gameObject);
        }
    }
    private void Propel()
    {
        Body.AddForce((Flotsam.transform.position - transform.position).normalized * force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
