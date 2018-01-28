using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public class LightBeam
    {
        GameObject beam;
        
        public float life;
        public float beamSpeed;

        public LightBeam(GameObject beam, float beamLife, float beamSpeed)
        {
            this.beam = beam;
            this.life = beamLife;
            this.beamSpeed = beamSpeed;

            beam.GetComponent<Rigidbody>().velocity = beam.transform.rotation * new Vector3(this.beamSpeed, 0.0f, 0.0f);
        }

        public void Update()
        {
            //Debug.Log(beam.GetComponent<Rigidbody>().velocity);
            life -= Time.deltaTime;
        }

        public void Kill()
        {
            Destroy(beam);
        }
    }

    public GameObject beamSource;
    public float beamSpeed = 20.0f;
    public float beamLife = 1.5f;
    public float fireDelay = 1.5f;
    public string player;

    private List<LightBeam> beams;

    private float nextFire = 0;

    void Start()
    {
        beams = new List<LightBeam>();
    }

    void Update()
    {
        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        var playerMovement = GetComponent<PlayerMovement>();
        if(Input.GetButtonDown("Fire_P"+player) && nextFire <= 0 && !playerMovement.isDamaged && !playerMovement.isInvulnerable)
        {
            nextFire = fireDelay;
            var quaternion = Quaternion.Euler(new Vector3(0.0f, transform.rotation.eulerAngles.y - 90, 0.0f));
            var beam = Instantiate(beamSource, transform.position + Vector3.up * 7.75f + quaternion * new Vector3(25.0f, 0.0f, 0.0f), quaternion);
            beams.Add(new LightBeam(beam, beamLife, beamSpeed));
        }

        for(var i = 0; i < beams.Count; i++)
        {
            beams[i].Update();
            if(beams[i].life <= 0)
            {
                beams[i].Kill();
                beams.RemoveAt(i);
                i--;
            }
        }
    }
}
