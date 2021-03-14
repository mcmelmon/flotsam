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
    public float displacementSeed = 1f;


    // Properties

    private float DisplacementFactor { get; set; }


    private void Awake() {
        Renderer rend = GetComponentInParent<Renderer>();        
        Floats = GetComponentsInParent<Float>().ToList();
        Parent = GetComponentInParent<Flotsam>();
        DisplacementFactor = displacementSeed + Random.Range(1f, 5f);
    }

    void FixedUpdate()
    {
        Parent.Body.AddForceAtPosition(Physics.gravity / Floats.Count, transform.position, ForceMode.Acceleration);
        float waveHeight = Water.Instance.WaveHeight(new Vector3(transform.position.x, Water.Instance.WaterLevel(), transform.position.z)).y;

        if (transform.position.y < waveHeight) {
            float displacementMultiplier = (waveHeight * DisplacementFactor - transform.position.y / DisplacementFactor) + 0.5f;
            Parent.Body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            Parent.Body.AddForce(displacementSeed * -Parent.Body.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
