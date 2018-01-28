using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
        }
    }

}
