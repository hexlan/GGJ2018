using UnityEngine;

public class HUD : MonoBehaviour {

    public Vector3 respawnPosition;
    public Vector3 respawnRotation;

    public int lives = 3;
    public int score = 0;
    public int carrying = 0;

    public bool load = false;
    public float loadTime = 2.5f;

    private bool inZone = false;
    private Collider currentZone = null;
	
	void Update ()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        if(inZone && !playerMovement.isDamaged && !playerMovement.isInvulnerable)
        {
            if(currentZone.GetComponent<ZoneParent>().parentPlayer == gameObject)
            {
                if (carrying > 0)
                {
                    load = true;
                    loadTime -= Time.deltaTime;
                    if (loadTime <= 0)
                    {
                        loadTime = 2.5f;
                        score += carrying;
                        carrying = 0;
                    }
                }
                else
                {
                    load = false;
                }
            }
            else if(currentZone.GetComponent<ZoneParent>().parentPlayer.GetComponent<HUD>().score > 0)
            {
                load = true;
                loadTime -= Time.deltaTime;
                if (loadTime <= 0)
                {
                    loadTime = 2.5f;
                    currentZone.GetComponent<ZoneParent>().parentPlayer.GetComponent<HUD>().score -= 2;
                    carrying += 2;
                }
            }
            else
            {
                load = false;
            }
        }
        else
        {
            load = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zone")
        {
            Debug.Log("ENTER ZONE");
            inZone = true;
            currentZone = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone")
        {
            Debug.Log("EXIT ZONE");
            inZone = false;
            currentZone = null;
        }
    }
}
