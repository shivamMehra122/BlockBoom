using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{

    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public static bool gameover = false;
    public static bool bossD=false;

    [SerializeField]
    Player player;
    [SerializeField]
    Monster monster;
    [SerializeField]
    GameObject wave;

    [SerializeField]
    PowerUp powerUp;

    [SerializeField]
    Text score;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    Boss boss;

    [SerializeField]
    Boss bossDiamond;

    [SerializeField]
    public GameObject panel;

    [SerializeField]
    Text panelScoreText;

    [SerializeField]
    Text highScoreText;

    int coins = 0;

    int monsterWaveCount = 3;
    int wavesLeft;
    float monsterSpeed = 2f;

    string textRead;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    Text pauseCurrentScore;

    [SerializeField]
    Text pauseHighScore;

    [SerializeField]
    GameObject fadePanel;

    int monsterCount = 5;
    int bossCount=0;

    public void onPauseMenuPress()
    {
        gameover = true;
        monsterStopOnOver();
        fadePanel.SetActive(true);
        pausePanel.SetActive(true);
        //Time.timeScale = 0;
        pauseCurrentScore.text = "Score: " + textRead;
        pauseHighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        pauseButton.SetActive(false);
    }

    public void onResumebuttonPress()
    {
        gameover = false;
        GameObject[] monsterFreeze = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in monsterFreeze)
        {
            obj.GetComponent<Monster>().enabled = true;
        }
        fadePanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pauseButton.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameover = false;
        //player.GetComponent<AndroidTouch>().enabled = true;
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Player p = Instantiate(player) as Player;
        p.onGainCoin += handleGainCoin;
        p.onPlayerDied += handlePlayerDeath;

        Boss.onBossDied += handleBossDeath;
        wavesLeft = monsterWaveCount;
        //Instantiate(powerUp);

        startMonsterGeneration();
        //generateBoss();

    }

    void handleBossDeath()
    {
        monsterSpeed++;
        monsterWaveCount++;
        wavesLeft = monsterWaveCount;
        startMonsterGeneration();
        //bossD = false;
        if (monsterCount == 5)
            monsterCount++;
        
    }

    void handlePlayerDeath()
    {
        Debug.Log("player died");
        monsterStopOnOver();
        //player.GetComponent<AndroidTouch>().enabled = false;
        fadePanel.SetActive(true);
        panel.SetActive(true);
        gameover = true;
        //Time.timeScale = 0;
        pauseButton.SetActive(false);
        panelScoreText.text = "Score: "+textRead;

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

       if(coins> PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", coins);
            highScoreText.text = "High Score: "+coins.ToString();
        }

    }

    void monsterStopOnOver()
    {
        GameObject[] monsterFreeze = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in monsterFreeze)
        {
            obj.GetComponent<Monster>().enabled = false;
        }
    }

    void handleGainCoin()
    {
        coins++;
        score.text = coins.ToString();
        textRead = score.text;
    }

    void startMonsterGeneration()
    {
        if (this != null)
        {
            InvokeRepeating("generateWave", 2, 3);
        }
    }

    void generateBoss()
    {
        Vector2 bosspos = new Vector2(0, topRight.y);
        if (bossCount == 2)
        {
            bossD = true;
            Instantiate(bossDiamond, bosspos, Quaternion.identity, transform);
            bossCount = 0;
        }
        else
        {
            Instantiate(boss, bosspos, Quaternion.identity, transform);
            bossCount++;
        }
    }

    void generateWave()
    {
        if (wavesLeft == 0)
        {
            CancelInvoke();
            Invoke("generateBoss", 2f);
            return;
        }

        wavesLeft--;

        GameObject monsterWave = Instantiate(wave, Vector2.zero, Quaternion.identity, transform);

        for(int i=0; i< monsterCount; i++)
        {
            float x = (i + 0.5f) / monsterCount;
            Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * x, Screen.height));
            if (monster != null)
            {
                pos += Vector2.up * monster.transform.localScale.y;
            }
            Monster monsterGO = Instantiate(monster, pos, Quaternion.identity, monsterWave.transform) as Monster;
            monsterGO.setSpeed(monsterSpeed);
        }
    }
}
