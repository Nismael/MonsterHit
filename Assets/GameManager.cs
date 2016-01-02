using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject Monster;
    public UnityEngine.UI.Text scoreText;
    public GameObject GameOverHandler;
    private HUDManager HUDManager;

    private List<Gremlin> monsters = new List<Gremlin>();
    private const float SpawnRate = 1.0f;
    private float spawnTimer = 0;
    public int playerHealth = 100;
    private int playerScore = 0;
    private bool bRunning = false;

    void Start ()
    {
        bRunning = true;
        HUDManager = GameObject.Find("HUD").GetComponent<HUDManager>();
    }
	
	void Update ()
    {
        if (!bRunning)
            return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= SpawnRate)
        {
            spawnTimer = 0;

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
            monsters.Add(Gremlin);
        }
    }

    public void OnActionsCompleted(Gremlin monster)
    {
        if (monsters.Find(x => x == monster))
        {
            Destroy(monster.gameObject);
            monsters.Remove(monster);
        }
    }

    public void OnMonsterKilled(Gremlin monster)
    {
        playerScore += 50;
        HUDManager.UpdateScore(playerScore);
    }

    public void OnMonsterAttack(Gremlin monster)
    {
        if (!bRunning)
            return;

        playerHealth -= 100;// monster.Damage;

        if (playerHealth <= 0)
        {
            bRunning = false;
            HUDManager.ShowGameOverScreen();
        }
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame");

        HUDManager.ResetGameOverScreen();
        HUDManager.UpdateScore(0);

        playerScore = 0;
        playerHealth = 100;
        scoreText.text = "0";
        spawnTimer = 0;
        bRunning = true;

        foreach (Gremlin g in monsters)
        {
            Destroy(g.gameObject);
        }

        monsters.Clear();
    }
}
