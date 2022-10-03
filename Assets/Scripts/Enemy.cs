using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public float timer;
    public float speed;
    public int flashes;
    public bool finalFlash;
    public bool inStage2;
    public bool inStage1;
    public GameObject currentStage;
    public GameManager gm;
    public WindowScript ws;
    public AudioSource attackNoise;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        //Find random spawn tagged stage1 then set enemy position to spawn position
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Stage1");
        GameObject randStart = spawns[Random.Range(0, spawns.Length)];
        stage1 = randStart;
        stage2 = stage1.transform.Find("Stage2").gameObject;
        currentStage = stage1;
        transform.position = stage1.transform.position;
        gm = FindObjectOfType<GameManager>();
        attackNoise = GetComponent<AudioSource>();
        ws = gameObject.GetComponentInParent<WindowScript>();
        transform.SetParent(ws.transform);
        
        //ws.enemies.Insert(0, gameObject);
         
        if (ws.enemies.Count >= ws.maxEnemies)
        {
            randStart = spawns[Random.Range(0, spawns.Length)];
            stage1 = randStart;
            Debug.Log("Full");
        }
        else
            stage1 = randStart;
        
        ws.enemies.Add(gameObject);
        Stage1Enter();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stage2Enter();
        }
    }

    public void Stage1Enter()
    {
        float randXPos = Random.Range(stage1.transform.position.x - 1, stage1.transform.position.x + 1);
        inStage1 = true;
        inStage2 = false;
        transform.position = new Vector3(randXPos, stage1.transform.position.y, stage1.transform.position.z);
        flashes++;
    }

    public void Stage2Enter()
    {
        float randXPos = Random.Range(stage2.transform.position.x - 1, stage2.transform.position.x + 1);
        inStage1 = false;
        inStage2 = true;
        transform.position = new Vector3(randXPos, stage2.transform.position.y, stage2.transform.position.z);
        flashes++;
    }

    public void GameOver()
    {
        attackNoise.PlayOneShot(clip);
        Debug.Log(gameObject + " got inside the house");
    }


}
