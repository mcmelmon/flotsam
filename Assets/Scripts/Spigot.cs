using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spigot : MonoBehaviour
{

    public WaterDrop drop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Drip());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // private

    IEnumerator Drip() 
    {
        while (true) {
            Instantiate(drop, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.2f);
        }
    }
}
