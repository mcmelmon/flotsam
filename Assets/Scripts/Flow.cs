using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{

    public float force = 1.5f;

    // Properties
    public Vector3 Direction { get; set; }
    public Vector3 Target { get; set; }
    private Rigidbody Body { get; set; }


    // Unity


    private void Awake() {
        Body = GetComponent<Rigidbody>();
        Direction = Vector3.forward;
    }


    private void Start() {
        StartCoroutine(Evaporate());
        
        // Move toward a random point in the direction headed, so not every flow moves in a straight line
        Vector3 angle = Quaternion.AngleAxis(Random.Range(-100.0f, 100.0f), transform.position) * Direction;
        Target = transform.position + (angle * 250);
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
        Body.AddForce((Target - transform.position).normalized * force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
