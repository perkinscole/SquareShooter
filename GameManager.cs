using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //SERIALIZED FIELDS///////////////////////////
    [SerializeField]
    private Transform[] spawnPoints; //enemy spawn points
    [SerializeField]
    private GameObject enemy;// enemy prefab
    [SerializeField]
    private GameObject[] players; //player array
    [SerializeField]
    private Text enemyKilledText; //UI for number enemies killed
    [SerializeField]
    private Text gameOverText;//If round lost
    [SerializeField]
    private Text winningText;//if round won
    [SerializeField]

    //PRIVATE FIELDS////////////////////////////////
    private Text waveText; //Displays wave number
    private int enemiesKilled;
    private float time;//keeps track of time for wave deployment
    private float waitTime; //time betwen waves
    private int goalScore; //how many enemies to kill to win
    private const float WAVE_TIME_DECREMENT = 2.0f;
    private const int MIN_WAIT = 1; //minimum wait time
    private const int RELOAD_TIME = 3; //time to restart game
    


    // Initialize variables and spawn first set of enemies.
    void Start()
    {
        goalScore = 100;
        enemiesKilled = 0;
        time = 0.0f;
        waitTime = 10.0f;
        SpawnEnemies(10);
        enemyKilledText.text = "Enemies Killed: " + enemiesKilled ;
        waveText.text = "Next Wave: " + (int) (waitTime - time);
    }


    /*      Update Method
     * Steps during update
     *  1. Check if the game over,
     *      1a.if it is end game.
     *  2. Else check if enough time is elapsed to spawn a new wave.
     *      2a. If it has, spawn a wave and decrase time between waves.
     *  3. Increase time, update text.
     *  */
    void Update()
    {
       if(CheckGameOver())
        {
            EndGame();
        }
   //check if itme to spawn
       if (time >= waitTime)
        {
            time =0;

            if (waitTime > MIN_WAIT) 
                    waitTime -= WAVE_TIME_DECREMENT;
            if (waitTime < MIN_WAIT)//limit the spawn time to 1.0s
                waitTime =(float) MIN_WAIT;
          
            SpawnEnemies(10);
        }



        time += Time.deltaTime;
        waveText.text = "Next Wave: " + (int) (waitTime - time);
    }


    /*    CheckGameOver
     *    Returns
     *    true if: Players dont exist.
     *    true if: Number of enemies killed is greater than goal (100).
     *    false if: neither condition is met.
     *    */
    private bool CheckGameOver()
    {
        if (players[0]== null  && players[1] == null)
        {
            return true;
        }else if(enemiesKilled >= goalScore)
        {
            return true;
        }
        return false;
    }

    /* End Game
     * 1. Checks to see if it is a win or a loss condition and sets the appropiate
     * end screen.
     * 2. Then starts a Coroutine to pause the game before reloading.
     * */
    private void EndGame()
    {
        if (enemiesKilled >= goalScore)
            winningText.gameObject.SetActive(true);
        else
            gameOverText.gameObject.SetActive(true);

        StartCoroutine(WaitToReload());//pause the game before restarting 
    }

    /* ChooseSpawnPoint
     * Returns a random spawn point from spawnPoints array.
     */
    private Transform ChooseSpawnPoint()
    {
        int num = (int)Random.Range(0, spawnPoints.Length);
        return spawnPoints[num];

    }

    /* SpawnEnenmies
     * @ param num: the number of enemies to spawn.
     * 1. Loops through the number of enemies to spawn and spaws one
     *      at a random spawn location using ChooseSpawnPoints().
     * 2. Sets the enemies targets using the players array/
     */
    private void SpawnEnemies(int num)
    {
        for(int i = 0; i < num; i++)
        {
           GameObject obj = Instantiate(enemy, ChooseSpawnPoint().position, Quaternion.identity);
            EnemyMovement em =  obj.GetComponent<EnemyMovement>();
            em.SetPlayers(players);
        }

    }

    /* EnemyKilled
     * Updates the enemy kill count and text
     */
    public void EnemyKilled()
    {
        enemiesKilled++;
        enemyKilledText.text = "Enemies Killed: " + enemiesKilled;
    }

    
    /*Wait to Reload
     * Waits the RELOAD_TIME to restart the game
     */
   private IEnumerator WaitToReload()
    {
          Time.timeScale = 0.1f;
    float pauseEndTime = Time.realtimeSinceStartup + RELOAD_TIME;
    while (Time.realtimeSinceStartup < pauseEndTime)
    {
        yield return 0;
    }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
