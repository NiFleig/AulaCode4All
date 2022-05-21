using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public PlayerState state;
    public Player player;
    public GameObject[] hearts;

    public GameObject[] outfitsUI;

    void Start()
    {}

    void Update()
    {
        UpdateHeart();
        UpdateEquippedOutfit();
    }

    void UpdateHeart()
    {
        if(state.CurrentHP >= 5)
        {
            hearts[4].SetActive(true);
        }else
        {
            hearts[4].SetActive(false);
        }

        if(state.CurrentHP >= 4)
        {
            hearts[3].SetActive(true);
        }else
        {
            hearts[3].SetActive(false);
        }
        
        if(state.CurrentHP >= 3)
        {
            hearts[2].SetActive(true);
        }else
        {
            hearts[2].SetActive(false);
        }
        
        if(state.CurrentHP >= 2)
        {
            hearts[1].SetActive(true);
        }else
        {
            hearts[1].SetActive(false);
        }

        if(state.CurrentHP >= 1)
        {
            hearts[0].SetActive(true);
        }else
        {
            hearts[0].SetActive(false);
        }
    }

    void UpdateEquippedOutfit()
    {
        if(player.currentState == Player.states.naked)
        {
            outfitsUI[0].SetActive(true);
        }else{
            outfitsUI[0].SetActive(false);
        }

        if(player.currentState == Player.states.bear)
        {
            outfitsUI[1].SetActive(true);
        }else{
            outfitsUI[1].SetActive(false);
        }

        if(player.currentState == Player.states.chicken)
        {
            outfitsUI[2].SetActive(true);
        }else{
            outfitsUI[2].SetActive(false);
        }

        if(player.currentState == Player.states.monkey)
        {
            outfitsUI[3].SetActive(true);
        }else{
            outfitsUI[3].SetActive(false);
        }

        if(player.currentState == Player.states.fish)
        {
            outfitsUI[4].SetActive(true);
        }else{
            outfitsUI[4].SetActive(false);
        }

        if(player.currentState == Player.states.frog)
        {
            outfitsUI[5].SetActive(true);
        }else{
            outfitsUI[5].SetActive(false);
        }
        
    }
}
