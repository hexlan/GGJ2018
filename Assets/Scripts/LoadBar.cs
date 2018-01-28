using UnityEngine;
using UnityEngine.UI;

public class LoadBar : MonoBehaviour {

    public GameObject parent;
    public Image loadBar;
	
	// Update is called once per frame
	void Update ()
    {
        var canvas = GetComponent<Canvas>();
        transform.position = parent.transform.position + Vector3.up * 30;

        if(parent.GetComponent<HUD>().load)
        {
            canvas.enabled = true;
            loadBar.fillAmount = ((parent.GetComponent<HUD>().loadTime - 2.5f) * -1) / 2.5f;
        }
        else
        {
            canvas.enabled = false;
        }
	}
}