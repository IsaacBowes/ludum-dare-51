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
    public List<Enemy> enemyScripts = new();
    public int amountToSpawn;
    public int clickAmmo;
    public int amountOfCycles;


    private void Start()
    {
        lighting = GetComponent<Light2D>();
        aS = GetComponent<AudioSource>();
        // Starts the timer automatically
        timerIsRunning = true;
        isPlaying = false;
        Spawn(amountToSpawn);
        

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
        Lightning();

        //Timer Count up
        if (timerIsRunning)
        {
            currentTime += Time.deltaTime;
            //DisplayTime(currentTime);

            if (gameOver == true)
            {
                Debug.Log("Time has stopped! You Lose");
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

    public void Lightning()
    {
        if (currentTime < 10f && currentTime >= 0f)
        {
            lighting.intensity = 0;
            foreach (WindowScript ws in windowScripts)
            {
                ws.CheckForEnemies();
            }
        }
        if (currentTime >= 10f && currentTime < 10.25f)
        {
            lighting.intensity = .1f;
            if (!isPlaying)
                StartCoroutine(playSound());
        }
        if(currentTime >= 10.25f && currentTime < 10.85f)
        {
            lighting.intensity = 0f;
        }
        if (currentTime >= 10.85f && currentTime < 11f)
        {
            lighting.intensity = .1f;
            if(!isPlaying)
                StartCoroutine(playSound());
        }
        if (currentTime >= 11f)
        {
            lighting.intensity = 0f;
            currentTime = 0f;
            foreach (Enemy e in enemyScripts)
            {
                bool flash = true;
                if (e.inStage1 && flash)
                {
                    Debug.Log(e.gameObject + " is approaching");
                    e.Stage2Enter();
                    flash = false;
                }
                if (e.inStage2 && !e.finalFlash && flash)
                {
                    Debug.Log(e.gameObject + " is ready to strike");
                    e.finalFlash = true;
                    flash = false;
                }
                if (e.inStage2 && e.finalFlash && flash)
                {
                    e.GameOver();
                    flash = false;
                }
            }
            foreach(WindowScript ws in windowScripts)
            {
                ws.amountOfClicks = 0;
            }

            Spawn(amountToSpawn);
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

    void Spawn(int num)
    {
        WindowScript randWS = windowScripts[Random.Range(0, windowScripts.Count)];
        Debug.Log("Attempting to spawn " + num + " enemies at" + randWS);

        for(int i = 0; i < num; i++)
        {
            Debug.Log("Entered Loop");
            if (randWS.enemyCount >= randWS.maxEnemies)
            {
                Debug.Log(randWS + " is full, finding another window");
            }
            else if (randWS.enemyCount < randWS.maxEnemies)
            {
                GameObject clone = Instantiate(enemy, randWS.transform);
                enemyScripts.Add(clone.GetComponent<Enemy>());
                Debug.Log("Enemy Created at: " + randWS);
                //clone.GetComponent<Enemy>().Stage1Enter();
            }
        }

    }

}
