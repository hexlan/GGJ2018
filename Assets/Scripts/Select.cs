using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public static int ready = 0;
    public string player;

    void Update()
    {
        if (Input.GetButtonDown("Fire_P" + player) && !GetComponent<Projector>().enabled)
        {
            GetComponent<Projector>().enabled = true;
            ready++;
            if(ready >= 4)
            {
                var music = GameObject.FindGameObjectWithTag("Jukebox");
                music.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene(2);
            }
        }
    }
}
