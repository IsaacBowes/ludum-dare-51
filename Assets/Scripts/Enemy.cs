using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public float timer;
    public float speed;
    public bool finalFlash;
    public bool inStage2;
    public bool inStage1;
    public GameObject currentStage;
    public GameManager gm;
    public WindowScript ws;

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
        ws = gameObject.GetComponentInParent<WindowScript>();
        
        //ws.enemies.Insert(0, gameObject);
        inStage1 = true;
        inStage2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inStage1 && gm.currentTime >= 10.5f)
        {
            Stage2Enter();
        }

        if(inStage2 && finalFlash)
        {

        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stage2Enter();
        }
    }

    void Stage2Enter()
    {
        inStage1 = false;
        inStage2 = true;
        transform.position = stage2.transform.position;
    }



}
