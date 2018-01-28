using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        var playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            if (!playerMovement.isInvulnerable && !playerMovement.isDamaged)
            {
                var carrying = collision.gameObject.GetComponent<HUD>().carrying;
                carrying -= 2;
                collision.gameObject.GetComponent<HUD>().carrying = Mathf.Max(0, carrying);
                playerMovement.isDamaged = true;
                playerMovement.initialAngle = collision.gameObject.transform.rotation;
                collision.gameObject.GetComponent<HUD>().lives--;
            }

        }
    }
}