using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag != "Player")
        {
            Debug.Log(collision.gameObject.tag);
            foreach (ContactPoint contact in collision.contacts)
            {
                //transform.rotation = Quaternion.Euler(Vector3.Reflect(transform.rotation.eulerAngles, contact.normal));
            }
        }
    }
}
