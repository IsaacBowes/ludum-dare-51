using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public float timer;
    public float speed;
    public LayerMask currentLayer;
    public GameObject currentStage;

    // Start is called before the first frame update
    void Start()
    {
        //Find random spawn tagged stage1 then set enemy position to spawn position
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Stage1");
        GameObject randStart = spawns[Random.Range(0, spawns.Length)].gameObject;

        stage1 = randStart;
        currentStage = stage1;
        transform.position = stage1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
