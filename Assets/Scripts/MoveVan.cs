using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveVan : MonoBehaviour
{
    private float time = 0.5f;
    public Text start;

	void Update () {
        if (transform.position.x > 77.5)
        {
            transform.position = new Vector3(transform.position.x - 50 * Time.deltaTime, transform.position.y, transform.position.z);
        } else
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                time = 0.5f;
                start.enabled = !start.enabled;
            }
        }

        if(Input.GetButtonDown("Fire_P1") || Input.GetButtonDown("Fire_P2") || Input.GetButtonDown("Fire_P3") || Input.GetButtonDown("Fire_P4"))
        {
            SceneManager.LoadScene(1);
        }
	}
}
