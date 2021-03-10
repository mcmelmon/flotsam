using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driifter : MonoBehaviour
{
    public Rigidbody body;
    public float depthBeforeSubmerged = 1f;
    public float displacementAount = 3f;
    public int corners = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);

        body.AddForceAtPosition(Physics.gravity / corners, transform.position, ForceMode.Acceleration);

        if (transform.position.y < waveHeight) {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAount;
            body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            body.AddForce(displacementMultiplier * -body.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            body.AddTorque(displacementMultiplier * -body.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
