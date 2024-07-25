using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    // Start is called before the first frame update
    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;

}
