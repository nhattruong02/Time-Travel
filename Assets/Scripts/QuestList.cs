using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestList : MonoBehaviour,ISavable
{
    List<QuestGame> quests = new List<QuestGame>();
    private void Start()
    {
        quests = new List<QuestGame>();
    }
    public void AddQuest(QuestGame quest)
    {
        if (!quests.Contains(quest))
        {
            quests.Add(quest);
        }
    }
    public static QuestList GetQuestList()
    {
        return FindObjectOfType<PlayerController>().GetComponent<QuestList>();
    }

    public object CaptureState()
    {
        return quests.Select(q => q.GetSaveData()).ToList();

    }

    public void RestoreState(object state)
    {
        var saveData = state as List<QuestSaveData>;
        if(saveData != null)
        {
            quests = saveData.Select(q => new QuestGame(q)).ToList();
        }
    }
}
