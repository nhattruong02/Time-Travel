using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "FinalQuest/Create a new quest")]
public class FinalQuest : MonoBehaviour
{
    [SerializeField] string nameQ;
    [SerializeField] string description;

    [SerializeField] Dialog startDialogue;
    [SerializeField] Dialog inprogessDialogue;
    [SerializeField] Dialog completedDialogue;

    [SerializeField] List<Item> requiredItem;
    [SerializeField] Item rewardItem;
    public string Name => nameQ;
    public string Description => description;
    public Dialog StartDialogue => startDialogue;
    public Dialog InprogessDialogue => inprogessDialogue?.Lines?.Count > 0 ? inprogessDialogue : startDialogue;
    public Dialog CompletedDialogue => completedDialogue;
    public List<Item> RequiredItem => requiredItem;
    public Item RewardItem => rewardItem;
}
