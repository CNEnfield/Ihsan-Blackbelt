using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RemainingEnemies : MonoBehaviour
{
    public int enemyCount;
    public TMP_Text countText;
    public int level;
    // Start is called before the first frame update
    void Awake()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        countText.text = "Remaining Enemies: " + enemyCount;
        if (enemyCount == 0)
        {
            Debug.Log("ALL ENEMIES DEAD");
            SceneManager.LoadScene(level);
        }

    }
}
