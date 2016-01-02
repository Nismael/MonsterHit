using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject Monster;
    public GameObject GameOverHandler;
    private HUDManager HUDManager;

    private const float SpawnRate = 1.0f;
    private const int MaxHealth = 100;

    private List<Gremlin> Monsters = new List<Gremlin>();
    private float SpawnTimer = 0;
    private int PlayerHealth = 0;
    private int PlayerScore = 0;
    private bool Running = false;

    void Start()
    {
        HUDManager = GameObject.Find("HUD").GetComponent<HUDManager>();
        PrepareGame();
    }
	
	void Update ()
    {
        if (!Running)
            return;

        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnRate)
        {
            SpawnTimer = 0;

            Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0, 1));
            position.x = Mathf.Lerp(-position.x, position.x, Random.value);
            position.z = -1;

            var monsterInstance = (GameObject)Instantiate(Monster, position, Quaternion.identity);
            var Gremlin = monsterInstance.GetComponent<Gremlin>();

            Gremlin.Speed = Mathf.Lerp(2, 4, Random.value);
            Gremlin.Damage = 10;
            Gremlin.OnActionsCompleted += OnActionsCompleted;
            Gremlin.OnMonsterKilled += OnMonsterKilled;
            Gremlin.OnMonsterAttack += OnMonsterAttack;
            Gremlin.Walk();
            Monsters.Add(Gremlin);
        }
    }

    private void PrepareGame()
    {
        PlayerScore = 0;
        PlayerHealth = 100;
        SpawnTimer = 0;
        HUDManager.UpdateScore(0);
        HUDManager._HealthBar.UpdateHealth(1);
        Running = true;
    }

    public void OnActionsCompleted(Gremlin monster)
    {
        if (Monsters.Find(x => x == monster))
        {
            Destroy(monster.gameObject);
            Monsters.Remove(monster);
        }
    }

    public void OnMonsterKilled(Gremlin monster)
    {
        PlayerScore += 50;
        HUDManager.UpdateScore(PlayerScore);
    }

    public void OnMonsterAttack(Gremlin monster)
    {
        if (!Running)
            return;

        PlayerHealth -= monster.Damage;
        HUDManager._HealthBar.UpdateHealth(PlayerHealth/100f);

        if (PlayerHealth <= 0)
        {
            Running = false;
            HUDManager.ShowGameOverScreen();
        }
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame");

        HUDManager.ResetGameOverScreen();
        PrepareGame();

        foreach (Gremlin g in Monsters)
        {
            Destroy(g.gameObject);
        }

        Monsters.Clear();
    }
}
