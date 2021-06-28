// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  06/24/2021

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TD_SBF_WaveSpawner : MonoBehaviour
{
    public TD_SBF_GameManagement gameMan;
    public Text waveCountdownText_H;
    public Text waveCountdownText_V;
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
            waveCountdownText_H.text = string.Format("{0:00.00}", countdown);
        else
            waveCountdownText_V.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        TD_SBF_PlayerStatistics.Rounds++;

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

        // TODO: Follow up eventually; VS say "meh" but it's needed and works(?)
        AstarPath.active.Scan();
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, 
            new Vector3(
                spawnPoint.position.x + Random.Range(-5f, 5f),
                spawnPoint.position.y + Random.Range(-3f, 3f),
                spawnPoint.position.z),
            spawnPoint.rotation);
    }

    IEnumerator DelayedWin()
    {
        yield return new WaitForSeconds(3f);

        if (!TD_SBF_GameManagement.IsGameOver)
            gameMan.WinLevel();
    }
}
