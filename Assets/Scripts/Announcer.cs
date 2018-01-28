using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Announcer : MonoBehaviour
{
    public AudioClip information;
    public AudioClip countDown;
    public AudioClip twoMinutes;
    public AudioClip oneMinute;
    public AudioClip fifteenSeconds;
    public AudioClip finalCountdown;

    public AudioClip blueVictory1;
    public AudioClip blueVictory2;
    public AudioClip blueVictory3;
    public AudioClip redVictory1;
    public AudioClip greenVictory1;
    public AudioClip yellowVictory1;

    public AudioClip blueLoss1;
    public AudioClip blueLoss2;
    public AudioClip redLoss1;
    public AudioClip greenLoss1;
    public AudioClip yellowLoss1;

    public AudioClip Tie1;
    public AudioClip Tie2;
    public AudioClip Tie3;
    public AudioClip Tie4;
    public AudioClip Tie5;

    public HUD redHUD;
    public HUD blueHUD;
    public HUD greenHUD;
    public HUD yellowHUD;

    public Texture2D fadeTexture;

    public bool pause = true;

    enum State
    {
        fadeIn,
        info,
        count_down,
        count_down_wait,
        play,
        two_minutes,
        one_minute,
        fifteen_seconds,
        final_countdown,
        winner,
        loser,
        fadeOut
    }

    private float timer = 180;
    public Text time;
    State currentState = State.fadeIn;

    private float fadeAlpha = 1.0f;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        if (currentState == State.fadeIn)
        {
            fadeAlpha += -0.8f * Time.deltaTime;
            fadeAlpha = Mathf.Clamp01(fadeAlpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeAlpha);
            GUI.depth = -1000;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);

            if (fadeAlpha <= 0.0)
            {
                currentState = State.info;
            }
        }
    }

    void Update()
    {
        var minutes = "" + (int)(timer / 60);
        var seconds = "" + (int)(timer % 60);

        if (seconds.Length == 1) seconds = "0" + seconds;

        time.text = minutes + ":" + seconds;

        switch (currentState)
        {
            case State.info:
                audio.PlayOneShot(information);
                currentState = State.count_down;
                break;
            case State.count_down:
                if (!audio.isPlaying)
                {
                    audio.PlayOneShot(countDown);
                    currentState = State.count_down_wait;
                }
                break;
            case State.count_down_wait:
                if (!audio.isPlaying)
                {
                    pause = false;
                    var jukebox = GameObject.FindGameObjectWithTag("Jukebox");
                    jukebox.GetComponent<AudioSource>().clip = jukebox.GetComponent<MenuScreenMusic>().gameMusic;
                    jukebox.GetComponent<AudioSource>().Play();
                    currentState = State.play;
                }
                break;
            case State.play:
                timer -= Time.deltaTime;

                if (timer > 121 && timer < 125) currentState = State.two_minutes;
                if (timer > 61 && timer < 65) currentState = State.one_minute;
                if (timer > 16 && timer < 20) currentState = State.fifteen_seconds;
                if (timer > 6 && timer < 10) currentState = State.final_countdown;

                break;
            case State.two_minutes:
                timer -= Time.deltaTime;
                if (timer < 121)
                {
                    audio.PlayOneShot(twoMinutes);
                    currentState = State.play;
                }
                break;
            case State.one_minute:
                timer -= Time.deltaTime;
                if (timer < 61)
                {
                    audio.PlayOneShot(oneMinute);
                    currentState = State.play;
                }
                break;
            case State.fifteen_seconds:
                timer -= Time.deltaTime;
                if (timer < 16)
                {
                    audio.PlayOneShot(fifteenSeconds);
                    currentState = State.play;
                }
                break;
            case State.final_countdown:
                timer -= Time.deltaTime;

                if (timer < 6)
                {
                    audio.PlayOneShot(finalCountdown);
                    currentState = State.winner;
                }

                break;
            case State.winner:
                timer -= Time.deltaTime;
                if (timer < 0) timer = 0;
                if (!audio.isPlaying)
                {
                    GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioSource>().Stop();
                    pause = true;
                    if (greenHUD.score > blueHUD.score && greenHUD.score > redHUD.score && greenHUD.score > yellowHUD.score)
                    {
                        greenHUD.gameObject.transform.position = new Vector3(51, 476.9f, -159);
                        greenHUD.gameObject.transform.rotation = Quaternion.Euler(new Vector3(225, -90, -90));
                        // Play Green Win
                        audio.PlayOneShot(greenVictory1);
                        currentState = State.loser;
                    }
                    else if (blueHUD.score > greenHUD.score && blueHUD.score > redHUD.score && blueHUD.score > yellowHUD.score)
                    {
                        blueHUD.gameObject.transform.position = new Vector3(51, 476.9f, -159);
                        blueHUD.gameObject.transform.rotation = Quaternion.Euler(new Vector3(225, -90, -90));
                        // Play Blue Win
                        var rng = Random.value * 3;
                        if (rng == 0)
                        {
                            audio.PlayOneShot(blueVictory1);
                        }
                        else if (rng == 1)
                        {
                            audio.PlayOneShot(blueVictory2);
                        }
                        else
                        {
                            audio.PlayOneShot(blueVictory3);
                        }
                        currentState = State.loser;
                    }
                    else if (redHUD.score > greenHUD.score && redHUD.score > blueHUD.score && redHUD.score > yellowHUD.score)
                    {
                        redHUD.gameObject.transform.position = new Vector3(51, 476.9f, -159);
                        redHUD.gameObject.transform.rotation = Quaternion.Euler(new Vector3(225, -90, -90));
                        // Play Red Win
                        audio.PlayOneShot(redVictory1);
                        currentState = State.loser;
                    }
                    else if (yellowHUD.score > greenHUD.score && yellowHUD.score > blueHUD.score && yellowHUD.score > redHUD.score)
                    {
                        yellowHUD.gameObject.transform.position = new Vector3(51, 476.9f, -159);
                        yellowHUD.gameObject.transform.rotation = Quaternion.Euler(new Vector3(225, -90, -90));
                        // Play Yellow Win
                        audio.PlayOneShot(yellowVictory1);
                        currentState = State.loser;
                    }
                    else
                    {
                        var rng = Random.value * 5;
                        if (rng == 0)
                        {
                            audio.PlayOneShot(Tie1);
                        }
                        else if (rng == 1)
                        {
                            audio.PlayOneShot(Tie2);
                        }
                        else if (rng == 2)
                        {
                            audio.PlayOneShot(Tie3);
                        }
                        else if (rng == 3)
                        {
                            audio.PlayOneShot(Tie4);
                        }
                        else
                        {
                            audio.PlayOneShot(Tie5);
                        }

                        currentState = State.fadeOut;
                    }
                }
                break;
            case State.loser:
                if (!audio.isPlaying)
                {
                    if (greenHUD.score <= blueHUD.score && greenHUD.score <= redHUD.score && greenHUD.score <= yellowHUD.score)
                    {
                        // Play Green Lost
                        audio.PlayOneShot(greenLoss1);
                    }
                    else if (blueHUD.score <= greenHUD.score && blueHUD.score <= redHUD.score && blueHUD.score <= yellowHUD.score)
                    {
                        // Play Blue Lost
                        var rng = Random.value * 2;
                        if (rng == 0)
                        {
                            audio.PlayOneShot(blueLoss1);
                        }
                        else
                        {
                            audio.PlayOneShot(blueLoss2);
                        }
                    }
                    else if (redHUD.score <= greenHUD.score && redHUD.score <= blueHUD.score && redHUD.score <= yellowHUD.score)
                    {
                        // Play Red Lost
                        audio.PlayOneShot(redLoss1);
                    }
                    else if (yellowHUD.score <= greenHUD.score && yellowHUD.score <= blueHUD.score && yellowHUD.score <= redHUD.score)
                    {
                        // Play Yellow Lost
                        audio.PlayOneShot(yellowLoss1);
                    }
                    currentState = State.fadeOut;
                }
                break;
            case State.fadeOut:
                if (!audio.isPlaying)
                {
                    var jukebox = GameObject.FindGameObjectWithTag("Jukebox");
                    jukebox.GetComponent<AudioSource>().clip = jukebox.GetComponent<MenuScreenMusic>().menuMusic;
                    jukebox.GetComponent<AudioSource>().Play();
                    currentState = State.play;
                    Select.ready = 0;
                    SceneManager.LoadScene(0);
                }
                break;
        }
    }
}