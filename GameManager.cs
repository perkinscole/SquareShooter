using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject[] players;
    private int enemiesKilled;
    private float time;
    private float waitTime;
    private int spawnCounter;
    [SerializeField]
    private Text enemyKilledText;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text winningText;
    [SerializeField]
    private Text waveText;
    

   
    bool gamePaused;


    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        spawnCounter = 1;
        time = 0.0f;
        waitTime = 10.0f;
        SpawnEnemies(10);
        enemyKilledText.text = "Enemies Killed: " + enemiesKilled ;
        waveText.text = "Next Wave: " + (int) (waitTime - time);
    }

    // Update is called once per frame
    void Update()
    {
       if(checkGameOver())
        {
            EndGame();
        }
   
       if (time >= waitTime)
        {
            time =0;
            if (waitTime > 2.0f) 
                    waitTime -= 2;
            if (waitTime < 2.0f)
                waitTime = 1.0f;
            spawnCounter++;
            //to do fix that
            SpawnEnemies(10);
        }



        time += Time.deltaTime;
        waveText.text = "Next Wave: " + (int) (waitTime - time);
    }

    private bool checkGameOver()
    {
        if (players[0]== null  && players[1] == null)
        {
            return true;
        }else if(enemiesKilled >= 100)
        {
            return true;
        }
        return false;
    }


    private void EndGame()
    {
        if (enemiesKilled >= 100)
            winningText.gameObject.SetActive(true);
        else
            gameOverText.gameObject.SetActive(true);

        StartCoroutine(DoNothing());

       
    }

    private Transform ChooseSpawnPoint()
    {
        int num = (int)Random.Range(0, spawnPoints.Length);
        return spawnPoints[num];

    }

    private void SpawnEnemies(int num)
    {
        for(int i = 0; i < num; i++)
        {
           GameObject obj = Instantiate(enemy, ChooseSpawnPoint().position, Quaternion.identity);
            EnemyMovement em =  obj.GetComponent<EnemyMovement>();
            em.setPlayers(players);
        }

    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        enemyKilledText.text = "Enemies Killed: " + enemiesKilled;
    }

    

   private IEnumerator DoNothing()
    {
          Time.timeScale = 0.1f;
    float pauseEndTime = Time.realtimeSinceStartup + 3;
    while (Time.realtimeSinceStartup < pauseEndTime)
    {
        yield return 0;
    }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
