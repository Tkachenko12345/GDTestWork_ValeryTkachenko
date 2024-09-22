using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public Player Player;
    public List<Enemie> Enemies;
    public GameObject Lose;
    public GameObject Win;

    private int _currWave;
    public int currWave
    {
        get => _currWave;
        set 
        {
            _currWave = value;
            OnChangingWave?.Invoke(value);
        }
    }
    public event Action<int> OnChangingWave;

    [SerializeField] private LevelConfig Config;
    public int WavesAmount { get => Config.Waves.Length; }

    private void Awake()
    {
        Instance = this;
        GameCycleEventsProvider.OnVictory += EnableWinUI;
        GameCycleEventsProvider.OnDeath += GameOver;
        currWave = 0;
    }

    private void Start()
    {
        SpawnWave();
    }

    private void OnDestroy()
    {
        GameCycleEventsProvider.OnVictory -= EnableWinUI;
        GameCycleEventsProvider.OnDeath -= GameOver;
    }

    public void AddEnemie(Enemie enemie)
    {
        Enemies.Add(enemie);
    }

    public void RemoveEnemie(Enemie enemie, ShouldCheckForFinishingWave shouldCheckForFinishingWave = ShouldCheckForFinishingWave.Yes)
    {
        Enemies.Remove(enemie);
        if(shouldCheckForFinishingWave == ShouldCheckForFinishingWave.Yes && Enemies.Count == 0)
        {
            SpawnWave();
        }
    }

    public void EnableWinUI()
    {
        Win.SetActive(true);
    }

    public void GameOver()
    {
        Lose.SetActive(true);
    }

    private void SpawnWave()
    {
        if (currWave >= WavesAmount)
        {
            GameCycleEventsProvider.Win();
            return;
        }

        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity);
        }
        currWave++;

    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

}
