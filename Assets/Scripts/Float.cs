using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Float : MonoBehaviour
{

    // Properties
    public List<Float> Floats { get; set; }
    public Flotsam Parent { get; set; }
    public float displacementSeed = 1f;


    // Properties

    private float DisplacementFactor { get; set; }
    private float WaterDrag { get; set; }

    private void Awake() {
        Renderer rend = GetComponentInParent<Renderer>();        
        Floats = GetComponentsInParent<Float>().ToList();
        Parent = GetComponentInParent<Flotsam>();
    }

    private void Start() {
        DisplacementFactor = displacementSeed + Random.Range(1f, 5f);
        WaterDrag = Parent.Body.drag;
    }

    void FixedUpdate()
    {
        Parent.Body.AddForceAtPosition(Physics.gravity / Floats.Count, transform.position, ForceMode.Acceleration);
        float waterLevel = Water.Instance.WaterLevel();
        float waveHeight = Water.Instance.WaveHeight(new Vector3(transform.position.x, waterLevel, transform.position.z)).y - waterLevel;
        float floatHeight = transform.position.y - waterLevel;

        if (floatHeight < waveHeight) {
            float displacementMultiplier = (waveHeight * DisplacementFactor - floatHeight / DisplacementFactor) + 0.5f;
            Parent.Body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            Parent.Body.AddForce(displacementSeed * -Parent.Body.velocity * WaterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
