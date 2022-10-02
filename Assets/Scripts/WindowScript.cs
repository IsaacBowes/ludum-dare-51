using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public BoxCollider2D bc2d;
    public List<GameObject> enemies = new List<GameObject>();
    public int enemyCount;
    public bool hasEnemy;
    public GameObject stage1;
    public GameObject stage2;
    public int amountOfClicks;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        //gm = transform.ge
        stage1 = transform.Find("Stage1").gameObject;
        stage2 = stage1.transform.Find("Stage2").gameObject;
        hasEnemy = false;
    }

    void OnMouseDown()
    {
        Debug.Log(name + "+1");
        amountOfClicks++;
    }
}
