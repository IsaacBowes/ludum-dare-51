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
        stage1 = ws.stage1;
        stage2 = ws.stage2;
        gm = FindObjectOfType<GameManager>();
        attackNoise = GetComponent<AudioSource>();
        
        ws.enemies.Add(gameObject);
        Stage1Enter();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stage2Enter();
        }

        if(transform.position.x >= 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (transform.position.x < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void Stage1Enter()
    {
        float randXPos = Random.Range(stage1.transform.position.x - 1f, stage1.transform.position.x + 1f);
        currentStage = stage1;
        inStage1 = true;
        inStage2 = false;
        transform.position = new Vector3(randXPos, stage1.transform.position.y, stage1.transform.position.z);
        flashes++;
    }

    public void Stage2Enter()
    {
        float randXPos = Random.Range(stage2.transform.position.x - 1f, stage2.transform.position.x + 1f);
        currentStage = stage2;
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
