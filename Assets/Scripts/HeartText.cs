using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartText : MonoBehaviour
{
    public Text heartText;
    public DamagebleCharacter damageableCharacter;

    void Update()
    {
        if (heartText != null && damageableCharacter != null)
        {
            heartText.text =  damageableCharacter.Health.ToString();
        }
    }
}
