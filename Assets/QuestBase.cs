using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Quest/Create a new quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] string nameQ;
    [SerializeField] string description;

    [SerializeField] Dialog startDialogue;
    [SerializeField] Dialog inprogessDialogue;
    [SerializeField] Dialog completedDialogue;

    [SerializeField] Item requiredItem;
    [SerializeField] Item rewardItem;
    public string Name => nameQ;
    public string Description => description;
    public Dialog StartDialogue => startDialogue;
    public Dialog InprogessDialogue => inprogessDialogue?.Lines?.Count > 0 ? inprogessDialogue : startDialogue;
    public Dialog CompletedDialogue => completedDialogue;
    public Item RequiredItem => requiredItem;
    public Item RewardItem => rewardItem;
}
