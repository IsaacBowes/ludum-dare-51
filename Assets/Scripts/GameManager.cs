using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float currentTime = 0;
    public GameObject[] enemyPrefabs;
    public bool gameOver;
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
    public int clickAmmo = 3;
    public int amountOfCycles;
    public Text ammo;


    private void Start()
    {
        lighting = GetComponent<Light2D>();
        aS = GetComponent<AudioSource>();
        // Starts the timer automatically
        timerIsRunning = true;
        isPlaying = false;
        Spawn(amountToSpawn);
        clickAmmo = 3;
        amountToSpawn = 1;
        amountOfCycles = 0;
        currentTime = 0f;

    }
    void Update()
    {
        ammo.text = clickAmmo.ToString();

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
        }
        if (currentTime >= 10f && currentTime < 10.25f)
        {
            foreach (Enemy e in enemyScripts)
            {
                e.gameObject.SetActive(true);
            }
            lighting.intensity = .1f;
            if (!isPlaying)
                StartCoroutine(playSound());
        }
        if(currentTime >= 10.25f && currentTime < 10.85f)
        {
            foreach (Enemy e in enemyScripts)
            {
                e.gameObject.SetActive(false);
            }
            lighting.intensity = 0f;
        }
        if (currentTime >= 10.85f && currentTime < 11f)
        {
            foreach(Enemy e in enemyScripts)
            {
                e.gameObject.SetActive(true);
            }
            lighting.intensity = .1f;
            if(!isPlaying)
                StartCoroutine(playSound());
        }
        if (currentTime >= 11f)
        {
            lighting.intensity = 0f;
            currentTime = 0f;
            clickAmmo = clickAmmo + 1;
            foreach (WindowScript ws in windowScripts)
            {
                ws.CheckForEnemies();
            }
            foreach (Enemy e in enemyScripts)
            {
                bool flash = true;
                if (e.inStage1 && flash)
                {
                    Debug.Log(e.gameObject + " is approaching");
                    e.Stage2Enter();
                    flash = false;
                    e.gameObject.SetActive(false);
                }
                if (e.inStage2 && !e.finalFlash && flash)
                {
                    Debug.Log(e.gameObject + " is ready to strike");
                    e.finalFlash = true;
                    flash = false;
                    e.gameObject.SetActive(false);
                }
                if (e.inStage2 && e.finalFlash && flash)
                {
                    e.GameOver();
                    flash = false;
                    SceneManager.LoadScene("EndScreen");
                }
                e.gameObject.SetActive(false);
            }
            foreach(WindowScript ws in windowScripts)
            {
                ws.amountOfClicks = 0;
            }
            amountOfCycles++;
            if (amountOfCycles == 1)
            {
                amountToSpawn = 1;
            }
            if (amountOfCycles == 2)
            {
                amountToSpawn = 1;
            }
            if (amountOfCycles == 3)
            {
                amountToSpawn = 2;
            }
            if (amountOfCycles == 4)
            {
                amountToSpawn = 2;
            }
            if (amountOfCycles == 5)
            {
                amountToSpawn = 3;
            }
            if (amountOfCycles == 10)
            {
                amountToSpawn = 4;
            }
            if (amountOfCycles == 15)
            {
                amountToSpawn = 5;
            }
            if (amountOfCycles == 20)
            {
                amountToSpawn = 6;
            }
            if (amountOfCycles == 25)
            {
                amountToSpawn = 7;
            }
            if (amountOfCycles == 30)
            {
                amountToSpawn = 8;
            }
            if (amountOfCycles == 40)
            {
                amountToSpawn = 9;
            }
            if (amountOfCycles == 50)
            {
                amountToSpawn = 10;
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
        for(int i = 0; i < num; i++)
        {
            WindowScript randWS = windowScripts[Random.Range(0, windowScripts.Count)];
            Debug.Log("Attempting to spawn " + num + " enemies at" + randWS);
            if (randWS.enemyCount >= randWS.maxEnemies)
            {
                Debug.Log(randWS + " is full, finding another window");
            }
            else if (randWS.enemyCount < randWS.maxEnemies)
            {
                GameObject randObj = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                GameObject clone = Instantiate(randObj, randWS.transform);
                enemyScripts.Add(clone.GetComponent<Enemy>());
                Debug.Log("Enemy Created at: " + randWS);
                clone.GetComponent<Enemy>().ws = randWS;
            }
        }

    }

    

}
