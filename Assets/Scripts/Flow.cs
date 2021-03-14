using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{

    public float force = 2f;

    // Properties
    public Rigidbody Body { get; set; }

    public Spigot Spigot { get; set; }


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
            yield return new WaitForSeconds(Spigot.SpawnDelay * 2f);
            Debug.Log("Evaporating");
            Destroy(transform.gameObject);
        }
    }
    private void Propel()
    {
        Body.AddForce(Vector3.forward * force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
