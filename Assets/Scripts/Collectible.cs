using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float height = 0.1f;
    private float velocity = 0;

    void Update()
    {
        if (height > 0)
        {
            velocity -= 0.05f * Time.deltaTime;
        }
        else
        {
            velocity += 0.05f * Time.deltaTime;
        }

        height += velocity;

        transform.position += new Vector3(0.0f, height, 0.0f);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 18, 25), transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<HUD>().carrying++;
            Destroy(this.gameObject);
        }
    }
}
