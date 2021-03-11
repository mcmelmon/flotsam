using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Float : MonoBehaviour
{

    // Properties
    public List<Float> Floats { get; set; }
    public Flotsam Parent { get; set; }
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    // Properties

    public float DepthBeforeSubmerged { get; set; }
    public float DisplacementAmount { get; set; }

    private void Awake() {
        Renderer rend = GetComponentInParent<Renderer>();        
        Floats = GetComponentsInParent<Float>().ToList();
        Parent = GetComponentInParent<Flotsam>();
        DepthBeforeSubmerged = rend.bounds.size.y;
        DisplacementAmount = DepthBeforeSubmerged / 2f;
    }

    void FixedUpdate()
    {
        Parent.Body.AddForceAtPosition(Physics.gravity / Floats.Count, transform.position, ForceMode.Acceleration);
        float waveHeight = Water.Instance.WaveHeight(new Vector3(transform.position.x, Water.Instance.WaterLevel(), transform.position.z)).y;

        if (transform.position.y < waveHeight) {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / DepthBeforeSubmerged) * DisplacementAmount;
            Parent.Body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            Parent.Body.AddForce(displacementMultiplier * -Parent.Body.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            Parent.Body.AddTorque(displacementMultiplier * -Parent.Body.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
