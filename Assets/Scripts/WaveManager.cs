using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    List<Vector3> waveVertices = new List<Vector3>();

        public float amplitude = 1f;
        public float length = 2f;
        public float speed = 1f;
        public float offset = 0f;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Debug.Log("WaveManager exists!");
            Destroy(this);
        }
    }
    void Start()
    {

    }

    void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float _xPosition)
    {
        return amplitude * Mathf.Sin((_xPosition / length) + offset);
    }
}
