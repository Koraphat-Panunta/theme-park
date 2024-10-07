using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Dart Stay");
       
        if (other.TryGetComponent<PlayerController>(out PlayerController p))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                p.gameObject.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(-24, 0, 0) * other.transform.forward * 15, ForceMode.Impulse);
            }
        }

    }
}
