using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public PlayerState state;
    public GameObject[] hearts;

    void Start()
    {}

    void Update()
    {
        UpdateHeart();
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
}
