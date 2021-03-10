using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driifter : MonoBehaviour
{
    public Water water;
    public Rigidbody body;

    struct Wave {
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

    Wave Wave1 { get; set; }
    Wave Wave2 { get; set; }
    Wave Wave3 { get; set; }


    // Unity


    private void Start()
    {
        Wave1 = new Wave(1, 1, 0.6f, 10);
        Wave2 = new Wave(0, 1, 0.5f, 5);
        Wave3 = new Wave(1, 0, 0.1f, 15);
    }


    private void Update()
    {
        
    }

    private void FixedUpdate() {
        SetPositionOnWaves();
    }


    // Private

    private float GetWaterLevel()
    {
        Vector3 sky = new Vector3(transform.position.x, water.transform.position.y + 100, transform.position.z);
        Ray ray = new Ray(sky, Vector3.down);
        RaycastHit hit;

        int layerMask = 1 << 4;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (rend == null || meshCollider == null) return -Mathf.Infinity;

            Texture2D tex = rend.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            return pixelUV.y;
        }

        return -Mathf.Infinity;
    }

    private void SetPositionOnWaves()
    {
        Vector3 gridPoint = transform.position;
        Vector3 position = gridPoint;
        position += GerstnerWave(Wave1, gridPoint);
        position += GerstnerWave(Wave2, gridPoint);
        position += GerstnerWave(Wave3, gridPoint);
        transform.position = position;
    }

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
            a * Mathf.Sin(f) / 10f,
            d.y * (a * Mathf.Cos(f) / 10f)
        );
        return calculated;
	}
}
