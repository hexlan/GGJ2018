using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.transform.RotateAround(Vector3.zero, Vector3.forward, 20 * Time.deltaTime);
        }
    }

}
