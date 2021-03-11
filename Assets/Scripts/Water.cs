using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public Material material;
    public struct Wave {
        public float x;
        public float y;
        public float z;
        public float w;
    
        public Wave(float _x, float _y, float _z, float _w) {
            this.x = _x;
            this.y = _y;
            this.z = _z;
            this.w = _w;
        }
    }

    // Properties

    public static Water Instance { get; set; }

    public List<Wave> Waves { get; set; }


    // Unity


    private void Awake () {
        if (Instance != null){
            Debug.LogError("More than one water instance!");
            Destroy(this);
            return;
        }
        Instance = this;
        Waves = new List<Wave>() {
            new Wave(0.2f, 1.0f, 0.2f, 40), 
            new Wave(0.0f, 0.5f, 0.1f, 30), 
            new Wave(0.6f, 1.0f, 0.3f, 50)
        };
    }

    void Start()
    {

    }


    void Update()
    {
        
    }


    // public

    public float WaterLevel()
    {
        return transform.position.y;
    }


    public Vector3 WaveHeight(Vector3 _point)
    {
        Vector3 level = _point;

        foreach (var wave in Waves) {
            level += GerstnerWave(wave, _point);
        }

        return level;
    }


    // Private

    private Vector3 GerstnerWave (Wave wave, Vector3 p) 
    {
        float steepness = wave.z;
        float wavelength = wave.w;
        float k = 2 * Mathf.PI / wavelength;
        float c = Mathf.Sqrt(9.8f / k);
        Vector2 d = new Vector2(wave.x, wave.y).normalized;
        float f = k * (Vector2.Dot(d, new Vector2(p.x, p.z).normalized) - c * Time.timeSinceLevelLoad); 
        float a = steepness / k;

        Vector3 calculated = new Vector3(
            d.x * (a * Mathf.Cos(f)) / 10f,
            a * Mathf.Sin(f) / 20f,
            d.y * (a * Mathf.Cos(f) / 10f)
        );
        return calculated;
	}
}
