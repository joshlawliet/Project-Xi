using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass 
{
    private string characterClassName, characterClassDescription;
    //Stats
    private int defense, attack, resilience, health;

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
    public int Defense 
    { 
        get => defense; 
        set => defense = value; 
    }
    public int Attack 
    { 
        get => attack; 
        set => attack = value; 
    }
    public int Resilience 
    { 
        get => resilience; 
        set => resilience = value; 
    }
    public int Health 
    { 
        get => health; 
        set => health = value; 
    }
}
