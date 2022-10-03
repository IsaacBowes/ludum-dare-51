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
        maxEnemies = 3;
        amountOfClicks = 0;
    }

    void OnMouseDown()
    {
        gm.clickAmmo--;
        if (gm.clickAmmo >= 1)
        {
            Debug.Log(name + "+1");
            amountOfClicks++;
        }
        else if (gm.clickAmmo <= 0)
            gm.clickAmmo = 0;
    }

    private void Update()
    {
        enemyCount = enemies.Count;
        clicks.text = amountOfClicks.ToString();
    }

    public void CheckForEnemies()
    {
        if (amountOfClicks >= 1)
        {
            if (amountOfClicks >= enemyCount && enemyCount >= 1 || amountOfClicks <= enemyCount && enemyCount >= 1)
            {
                foreach (GameObject enemy in enemies)
                {
                    Debug.Log("11111");
                    if (enemy.GetComponent<Enemy>().currentStage == stage2)
                    {
                        gm.clickAmmo = gm.clickAmmo + 1;
                        enemies.Remove(enemy);
                        gm.enemyScripts.Remove(enemy.GetComponent<Enemy>());
                        Destroy(enemy);
                        Debug.Log("Destroying enemies " + enemy);
                        break;
                    }
                    if (enemy.GetComponent<Enemy>().currentStage == stage1)
                    {
                        gm.clickAmmo = gm.clickAmmo + 1;
                        enemies.Remove(enemy);
                        gm.enemyScripts.Remove(enemy.GetComponent<Enemy>());
                        Destroy(enemy);
                        Debug.Log("Destroying enemies " + enemy);
                        break;
                    }
                }
            }
            else if (amountOfClicks < enemyCount && enemyCount >= 1)
            {
                foreach (GameObject enemy in enemies)
                {
                    Debug.Log("22222");
                    if (enemy == stage2)
                    {
                        Debug.Log("Defeated by: " + enemy);
                        gm.gameOver = true;
                    }
                    if (enemy.GetComponent<Enemy>().currentStage == stage1)
                    {
                        break;
                    }
                }

            }
        }
    }
}
