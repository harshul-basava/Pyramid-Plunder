using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scorekeeper : MonoBehaviour
{
    private static Scorekeeper scoreKeeperInstance;

    // statistics for the whole game
    public int score;
    public int enemies_killed;

    // statistics for the round
    public int? round_score;
    public int round_enemies_killed;

    public InventoryManagement IM;
    public HealthManager HM;
    public roundEndScreenControl endScreen;
     public deathScreenControl deathScreen;

    public Timer timer;
    public bool newScene;

    // when time is up for each round calculate the final score using:
    // sum of scores in inv
    public int round_inv_score;

    // sum of scores from killing enemies (done throughout game)
    public int round_enemy_score;

    // + health remaining on player + defense + attack
    public int round_health_score;

    void Awake()
    {
        newScene = true;
        SceneManager.sceneLoaded += OnSceneLoaded;

        score = 0;
        round_score = null;

        enemies_killed = 0;
        round_enemies_killed = 0;

        DontDestroyOnLoad(gameObject);

        if (scoreKeeperInstance == null) {
            scoreKeeperInstance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        IM = GameObject.Find("InventoryManager").GetComponent<InventoryManagement>();
        HM = GameObject.Find("HealthManager").GetComponent<HealthManager>();

        endScreen = GameObject.Find("roundEndScreen").GetComponent<roundEndScreenControl>();
        deathScreen = GameObject.Find("deathScreen").GetComponent<deathScreenControl>();

        timer = GameObject.Find("Timer").GetComponent<Timer>();

        if (timer.timeRemaining == 0 && round_score == null && newScene)
        {
            round_score = CalculateFinalRoundScore();

            endScreen.UpdateText(round_inv_score, round_enemies_killed, round_health_score, (int)round_score);

            newScene = false;
        } 
        
        if (HM.playerHealth == 0 && round_score == null && newScene)
        {
            round_score = CalculateFinalRoundScore();

            deathScreen.UpdateText(round_inv_score, round_enemies_killed, round_health_score, (int)round_score);

            newScene = false;
        } 
    }

    private int CalculateFinalRoundScore()
    {
        round_inv_score = IM.CalculateFinalScore();
        round_health_score = (int) HM.playerHealth + (int) HM.playerDefense + (int) HM.playerAttack;
        round_enemy_score = round_enemies_killed * 75;

        return round_inv_score + round_health_score + round_enemy_score;
    }

    public void UpdateTotalScore()
    {
        score += (int) round_score;
        round_score = null;

        enemies_killed += round_enemies_killed;
        round_enemies_killed = 0;

        round_inv_score = 0;
        round_enemy_score = 0;
        round_enemy_score = 0;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        newScene = true;
    }
}
