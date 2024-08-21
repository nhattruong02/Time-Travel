using RPGM.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGame 
{
    public QuestBase Base { get; set; }
    public QuestStatus Status { get; set; }
    public QuestGame(QuestBase _base)
    {
        Base = _base;
    }
    public QuestGame(QuestSaveData saveData)
    {
        Base = QuestDB.GetQuestByName(saveData.name);
        Status = saveData.status;
    }
    public QuestSaveData GetSaveData()
    {
        var saveData = new QuestSaveData()
        {
            name = Base.name,
            status = Status
        };
            return saveData;
    }
    public IEnumerator StartQuest()
    {
        Status = QuestStatus.Started;
        yield return DialogManager.Instance.ShowDialog(Base.StartDialogue);
/*        var questList = QuestList.GetQuestList();
        questList.AddQuest(this);*/
    }
    public IEnumerator CompleteQuest(Transform player)
    {
        Status = QuestStatus.Completed;
        yield return DialogManager.Instance.ShowDialog(Base.CompletedDialogue);
        var inventory = Inventory.GetInventory();
        if(Base.RequiredItem != null)
        {
            inventory.RemoveItem(Base.RequiredItem);
        }
        if(Base.RewardItem != null)
        {
            inventory.AddItem(Base.RewardItem);
/*            yield return DialogManager.Instance.ShowDialogText($"Ban da nhan {Base.RewardItem.Name}");
*/        }
    }
    public bool CanBeCompleted()
    {
        var inventory = Inventory.GetInventory();
        if(Base.RequiredItem != null)
        {
            if(!inventory.HasItem(Base.RequiredItem))
                return false;
        }
        return true;
    }
}
[System.Serializable]
public class QuestSaveData
{
    public string name;
    public QuestStatus status;
}
public enum QuestStatus { None, Started, Completed}