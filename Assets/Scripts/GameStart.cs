using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetButtonDown("Fire_P1") || Input.GetButtonDown("Fire_P2") || Input.GetButtonDown("Fire_P3") || Input.GetButtonDown("Fire_P4"))
        {
            SceneManager.LoadScene(1);
        }
    }
}