using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Data", menuName = "Powerup", order = 1)]
public class PowerUp : ScriptableObject
{
    public string powerName;
    public Sprite sprite;
}
