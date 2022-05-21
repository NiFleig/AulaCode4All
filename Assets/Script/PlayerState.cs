using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public int CurrentHP;
    public int MaxHP;

    private Player player;

    public bool noSuitEquip = true;
    public bool bearEquip = false;
    public bool chickenEquip = false;
    public bool monkeyEquip = false;
    public bool fishEquip = false;
    public bool frogEquip = false;

    public GameObject noSuit;
    public GameObject bearSuit;
    public GameObject chickenSuit;
    public GameObject monkeySuit;
    public GameObject fishSuit;

    public GameObject pauseUI;

    public DoubleJump dJump;
    public WallJump wJump;

    void Start()
    {
        player = GetComponent<Player>();
        dJump = chickenSuit.GetComponent<DoubleJump>();
        wJump = monkeySuit.GetComponent<WallJump>();


    }

    void Update()
    {
        UpdateEquippedSuit();

        CheckPause();
    }

    public void UpdateHP(int amount)
    {
        if (CurrentHP + amount > MaxHP)
        {
            CurrentHP = MaxHP;
            return;
        }

        CurrentHP += amount;

        if (CurrentHP <= 0)
            StartCoroutine(Death());
    }

    public void UpdateEquippedSuit()
    {
        if (bearEquip == true && player.currentState == Player.states.bear)
        {
            noSuit.SetActive(false);
            bearSuit.SetActive(true);
            chickenSuit.SetActive(false);
            monkeySuit.SetActive(false);
            fishSuit.SetActive(false);
        }

        if (chickenEquip == true && player.currentState == Player.states.chicken)
        {
            noSuit.SetActive(false);
            bearSuit.SetActive(false);
            chickenSuit.SetActive(true);
            monkeySuit.SetActive(false);
            fishSuit.SetActive(false);
        }
        else
        {
            dJump.djump = true;
        }

        if (monkeyEquip == true && player.currentState == Player.states.monkey)
        {
            noSuit.SetActive(false);
            bearSuit.SetActive(false);
            chickenSuit.SetActive(false);
            monkeySuit.SetActive(true);
            fishSuit.SetActive(false);
        }
        else
        {
            wJump.wSlide = false;
        }

        if (fishEquip == true && player.currentState == Player.states.fish)
        {
            noSuit.SetActive(false);
            bearSuit.SetActive(false);
            chickenSuit.SetActive(false);
            monkeySuit.SetActive(false);
            fishSuit.SetActive(true);
        }

    }

    public void CheckPause()
    {
        if (player.pause == true)
        {
            player.currentState = Player.states.pause;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }


    public IEnumerator Death()
    {
        player.currentState = Player.states.death;
        player.playerAnim.SetBool("IsDead", true);
        player.mainAnim.SetBool("IsDead", true);

        Time.timeScale = 0f;
        player.playerAnim.updateMode = AnimatorUpdateMode.UnscaledTime;

        //yield return StartCoroutine(WaitForRealSeconds(2));

        yield return StartCoroutine(DeathDelay(1f));
        GameMaster.KillPlayer(this);
        player.playerAnim.SetBool("IsDead", false);
        player.mainAnim.SetBool("IsDead", false);
        player.currentState = Player.states.naked;

        

        CurrentHP = MaxHP;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("death");
    }

    public IEnumerator DeathDelay(float spawnDelay)
    {
        yield return StartCoroutine(WaitForRealSeconds(spawnDelay));
        Time.timeScale = 1f;
        player.playerAnim.updateMode = AnimatorUpdateMode.Normal;
        //Debug.Log("deathdelay");
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
        //Debug.Log("wait");
    }
}
