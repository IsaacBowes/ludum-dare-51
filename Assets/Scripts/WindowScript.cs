using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowScript : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public int enemyCount;
    public int maxEnemies;
    public GameObject stage1;
    public GameObject stage2;
    public int amountOfClicks;
    public GameManager gm;
    public Text clicks;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        stage1 = transform.Find("Stage1").gameObject;
        stage2 = stage1.transform.Find("Stage2").gameObject;
    }

    void OnMouseDown()
    {
        Debug.Log(name + "+1");
        amountOfClicks++;
    }

    private void Update()
    {
        enemyCount = enemies.Count;
    }

    public void CheckForEnemies()
    {
        if (amountOfClicks >= enemyCount)
        {
            foreach(GameObject enemy in enemies)
            {
                if (enemy.GetComponent<GameObject>() == stage2)
                {
                    Destroy(enemy);
                }
                if (enemy.GetComponent<GameObject>() == stage1)
                {
                    Destroy(enemy);
                }
                else 
                    break;
            }
        }
        else if (amountOfClicks < enemyCount && enemyCount >= 1)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<GameObject>() == stage2)
                {
                    gm.gameOver = true;
                }
                if (enemy.GetComponent<GameObject>() == stage1)
                {
                    break;
                }
            }

        }
    }
}
