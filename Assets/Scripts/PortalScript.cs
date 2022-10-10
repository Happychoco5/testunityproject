using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Transform[] points;

    [Serializable]
    public struct EnemyType
    {
        public GameObject enemy;
        public int enemyAmount;
    }

    [Serializable]
    public struct WaveType
    {
        public string name;
        [Tooltip("Only put a value in leader ID if there is a leader in this wave. Default: 0. \n This should always have a value if it is the last wave.")]
        public int leaderID;

        public int enemyMinLevel;
        public int enemyMaxLevel;

        //public int maxAmountEnemies;
        public List<EnemyType> enemies;
    }

    //A list of enemy amounts so we can adjust the values in this script
    List<int> enemyAmounts;


    //This is the wave types. They can be altered in the inspector to edit or change waves.
    public WaveType[] waveTypes;

    public int maxWaves;//controls how many waves will exist in the level
    public int curWave;//current wave number

    public Transform spawnPoint;

    public int enemiesAlive;
    public int currentNumberEnemies;//current number of raiders in player dungeon
    public int maxEnemiesInWave;//Max number of raiders that can be in the dungeon at a time

    public bool canStart;

    // Start is called before the first frame update
    void Start()
    {
        //we want the wave number to be also set at 0 at start
        curWave = 0;
        canStart = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartWave()
    {
        if(canStart == true)
        {
            if (enemiesAlive <= 0)
            {
                StartCoroutine(WaveStartTimer(10));

                maxEnemiesInWave = 0;
                for (int i = 0; i < waveTypes[curWave].enemies.Count; i++)
                {
                    maxEnemiesInWave += waveTypes[curWave].enemies[i].enemyAmount;
                }

                enemyAmounts = new List<int>();
                for (int i = 0; i < waveTypes[curWave].enemies.Count; i++)
                {
                    enemyAmounts.Add(waveTypes[curWave].enemies[i].enemyAmount);
                }

                canStart = false;
            }
            else
            {
                Debug.Log("All enemies are not currenlty dead! We can't start the next wave yet.");
            }
        }

    }


    private IEnumerator WaveStartTimer(int i)
    {
        /*while (GameManager.instance.isPuased)
        {
            yield return null;
        }*/
        if (i <= 0)
        {
            //StartSpawning
            StartCoroutine(SpawnRaider());
        }
        else
        {
            yield return new WaitForSeconds(1);
            i--;
            StartCoroutine(WaveStartTimer(i));
            //GameManager.instance.mainInventory.transform.parent.Find("Wave Timer").GetComponent<Text>().text = "Next Wave: " + i.ToString();
            Debug.Log(i);
        }
    }

    private IEnumerator SpawnRaider()
    {
        /*while (GameManager.instance.isPuased)
        {
            yield return null;
        }*/

        if (currentNumberEnemies >= maxEnemiesInWave)
        {
            curWave++;
            currentNumberEnemies = 0;
            canStart = true;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            SpawnEnemy();
            currentNumberEnemies++;
            StartCoroutine(SpawnRaider());
        }
    }
    void SpawnEnemy()
    {
        if (waveTypes[curWave].enemies.Count != 0)
        {
            int randomNum = UnityEngine.Random.Range(0, waveTypes[curWave].enemies.Count);

            GameObject enemy = Instantiate(waveTypes[curWave].enemies[randomNum].enemy, spawnPoint.position, waveTypes[curWave].enemies[randomNum].enemy.transform.rotation);
            enemy.GetComponent<BaseEnemyScript>().myPortal = this;

            enemyAmounts[randomNum]--;
            if (enemyAmounts[randomNum] <= 0)
            {
                waveTypes[curWave].enemies.Remove(waveTypes[curWave].enemies[randomNum]);
                Debug.Log("reached end of array");
            }
            enemiesAlive++;
        }
    }
}
