using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float minimumSpeed = 1f;
    public float maximumSpeed = 10f;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(Evaporate());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        Flow();
    }


    // private

    void Flow() 
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0, 360), transform.eulerAngles.z);
        float speed = Random.Range(minimumSpeed, maximumSpeed);
        Vector3 force = transform.forward;
        force = new Vector3(force .x, 1, force .z);
        body.AddForce(force * speed );
    }

    IEnumerator Evaporate()
    {
        while (true) {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);

        }
    }
}
