using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public GameObject beamSource;
    public float beamSpeed = 20.0f;
    public float fireRate;
    public float tilt;

    private List<GameObject> beams;

    private float nextFire;

    void Start()
    {
        beams = new List<GameObject>();
    }

    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles);

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {

            beams.Add(Instantiate(beamSource, transform.position, Quaternion.Euler(new Vector3(0.0f, transform.rotation.eulerAngles.y - 90, 0.0f))));
        }

        foreach (var beam in beams)
        {
            beam.transform.position += beam.transform.rotation * new Vector3(beamSpeed, 0.0f, 0.0f);
        }
    }
}
