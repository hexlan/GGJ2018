using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
           
    }
}