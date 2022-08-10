// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 07/08/2016
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameManagement gameMan;
    public Text waveCountdownText_Horizontal;
    public Text waveCountdownText_Vertical;
    public Transform spawnPoint;
    public Wave[] waves;

    public float timeBetweenWaves = 5f;
    public float countdown = 10f;

    public static int enemiesAlive;
    public int prevWaveIndex;
    public int waveIndex;

    void Start()
    {
        enemiesAlive = 0;
        waveIndex = 0;
        prevWaveIndex = waveIndex - 1;
    }

    void Update()
    {
        if (enemiesAlive > 0)
            return;

        if (waveIndex == waves.Length)
        {
            StartCoroutine(DelayedWin());
            enabled = false;
            return;
        }

        if (countdown <= 0f &&
            waveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave());
            prevWaveIndex++;
            countdown = timeBetweenWaves;
            return;
        }
        
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // TODO: Enable if device flips

        if (Screen.width >= Screen.height)
        {
            waveCountdownText_Horizontal.text = string.Format("{0:00.00}", countdown);
        }
        else
        {
            waveCountdownText_Horizontal.text = string.Format("{0:00.00}", countdown);
        }
    } 

    IEnumerator SpawnWave()
    {
        PlayerStatistics.Rounds++;

        Wave wave = waves[waveIndex];

        enemiesAlive = wave.count;

        if (wave.isBadass)
        {
            Debug.Log("Increase waypoint height in editor.");
        }
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator DelayedWin()
    {
        yield return new WaitForSeconds(3f);
        
        if (!GameManagement.IsGameOver)
            gameMan.WinLevel();
    }
}
