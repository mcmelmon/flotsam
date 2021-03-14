using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spigot : MonoBehaviour
{
    public Flow flow;
    public float delayBase = 10f;
    
    
    // Properties


    public float SpawnDelay { get; set; }


    // Unity


    private void Awake() {
        SpawnDelay = delayBase + Random.Range(0f, 5f);
    }
    
    void Start()
    {
        StartCoroutine(SpawnFlow());
    }


    // Private

    private IEnumerator SpawnFlow()
    {
        while (true) {
            Flow spawn = Instantiate(flow, transform.position, Quaternion.identity);
            spawn.Spigot = this;
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}
