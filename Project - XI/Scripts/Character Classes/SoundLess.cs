using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLess : MonoBehaviour
{
    private string characterClassName, characterClassDescription;
    //Stats
    private double defense, attack, resilience, health;
    void Start()
    {
        resilience = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string CharacterClassName
    {
        get => characterClassName;
        set => characterClassName = value;
    }
    public string CharacterClassDescription
    {
        get => characterClassDescription;
        set => characterClassDescription = value;
    }
    public double Defense
    {
        get => defense;
        set => defense = value;
    }
    public double Attack
    {
        get => attack;
        set => attack = value;
    }
    public double Resilience
    {
        get => resilience;
        set => resilience = value;
    }
    public double Health
    {
        get => health;
        set => health = value;
    }
}