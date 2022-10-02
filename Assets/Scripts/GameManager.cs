using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public float currentTime = 0;
    public GameObject enemy;
    public bool gameOver;
    public bool created;
    public float timeRemaining = 0;
    public bool timerIsRunning = false;
    public TextMeshPro timeText;
    public Light2D lighting;
    public AudioClip[] lightningCrash;
    public AudioSource aS;
    public bool isPlaying;
    public List<WindowScript> windowScripts = new();

    private void Start()
    {
        lighting = GetComponent<Light2D>();
        aS = GetComponent<AudioSource>();
        // Starts the timer automatically
        timerIsRunning = true;
        isPlaying = false;
        Instantiate(enemy);
        

    }
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        lightning();

        //Timer Count up
        if (timerIsRunning)
        {
            currentTime += Time.deltaTime;
            //DisplayTime(currentTime);

            if (gameOver == true)
            {
                Debug.Log("Time has stopped!");
                timeRemaining = timeRemaining;
                timerIsRunning = false;
            }
        }
    }
    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //float milliSeconds = (timeToDisplay % 1) * 1000;
        //timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void lightning()
    {
        if (currentTime < 10f && currentTime >= 0f)
        {
            lighting.intensity = 0;

        }
        if (currentTime >= 10f && currentTime < 10.5f)
        {
            lighting.intensity = .1f;
            if (!isPlaying)
                StartCoroutine(playSound());
        }
        if(currentTime >= 10.5f && currentTime < 11f)
        {
            lighting.intensity = 0f;
        }
        if (currentTime >= 11f && currentTime < 11.5f)
        {
            lighting.intensity = .1f;
            if(!isPlaying)
                StartCoroutine(playSound());
        }
        if (currentTime >= 11.5f)
        {
            lighting.intensity = .1f;
            currentTime = 0f;
        }
    }


    IEnumerator playSound()
    {
        AudioClip rand = lightningCrash[Random.Range(0, lightningCrash.Length)];
        isPlaying = true;
        aS.clip = rand;
        aS.Play();       //plays big hand audio
        yield return new WaitForSeconds(aS.clip.length);
        AudioClip rand2 = lightningCrash[Random.Range(0, lightningCrash.Length)];
        aS.clip = rand2;
        aS.Play();       //plays small hand audio after big hand audio
        yield return new WaitForSeconds(aS.clip.length);
        isPlaying = false;
    }
}
