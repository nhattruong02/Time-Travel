
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDB : MonoBehaviour
{
    static Dictionary<string, QuestBase> quests;
    public static void Init()
    {
        quests = new Dictionary<string, QuestBase>();
        var questList = Resources.LoadAll<QuestBase>("");
        foreach (var quest in questList)
        {
            if (quests.ContainsKey(quest.Name))
            {
                Debug.Log($"{quest.Name}");
                continue;
            }
            quests[quest.Name] = quest;
        }
    }
    public static QuestBase GetQuestByName(string name)
    {
        if (!quests.ContainsKey(name))
        {
            Debug.Log($"Not found {name}");
            return null;
        }
        return quests[name];
    }
}
