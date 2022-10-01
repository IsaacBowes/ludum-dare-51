using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public float timer;
    public float speed;
    //public string tag;
    public GameObject currentStage;

    // Start is called before the first frame update
    void Start()
    {
        //Find random spawn tagged stage1 then set enemy position to spawn position
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Stage1");
        GameObject randStart = spawns[Random.Range(0, spawns.Length)].gameObject;

        stage1 = randStart;
        stage2 = stage1.transform.Find("Stage2").gameObject;
        currentStage = stage1;
        transform.position = stage1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stage2Enter();
        }
    }

    void Stage2Enter()
    {
        transform.position = stage2.transform.position;
    }



}
