using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundlessClass : BaseClass
{
    public void Soundless()
    {
        CharacterClassName = "Soundless";
        CharacterClassDescription = "This class is not affected by sound waves";
        Health = 220;
        Attack = 15;
        Defense = 10;
    }
}
