using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour, Interactable,ISavable
{
    [SerializeField] QuestBase questToStart;
    [SerializeField] QuestBase questToComplete;
    [SerializeField] Dialog dialog;
    [SerializeField] int index;
    ItemGiver itemGiver;
    QuestGame activeQuest;
    Inventory inventory;
    public static object savedState;
    public static object savedQuest;
    private void Awake()
    {   

        itemGiver = GetComponent<ItemGiver>();
        inventory = FindObjectOfType<Inventory>();
    }
    public IEnumerator Interact(Transform initiator)
    {
        if (questToStart != null)
        {
            Debug.Log("qStart");
            activeQuest = new QuestGame(questToStart);
            if(!activeQuest.CanBeCompleted()) {
            yield return activeQuest.StartQuest();
            }
            questToStart = null;
            if (activeQuest.CanBeCompleted())
            {
                yield return activeQuest.CompleteQuest(initiator);
                activeQuest = null;
            }
            CaptureState();
        }
        else if (activeQuest != null)
        {
            
            if (activeQuest.CanBeCompleted())
            {
                yield return activeQuest.CompleteQuest(initiator);
                activeQuest = null;
            }
            else
            {
                yield return DialogManager.Instance.ShowDialog(activeQuest.Base.InprogessDialogue);
                ChangeScene(index);
            }
        }
        else if (activeQuest == null && questToStart == null && gameObject.CompareTag("Mage"))
        {
            yield return DialogManager.Instance.ShowDialog(dialog);
            inventory.CheckItem();
        }

        else
        {
            yield return DialogManager.Instance.ShowDialog(dialog);
        }
    }
    async Task SomeAsyncTask()
    {
        await Task.Delay(1000);
  
    }
    public void ChangeScene(int index)
    {
        Inventory inventory = Inventory.GetInventory();
        if (inventory != null)
        {
            savedState = inventory.CaptureState();
            inventory.CaptureState();
        }
        SceneManager.LoadScene(index);
    }
    public object CaptureState()
    {
        var saveData = new NPCQuestSaveData();
        saveData.activeQuest = activeQuest?.GetSaveData();
        if (questToStart != null)
        {
            saveData.questToStart = (new QuestGame(questToStart)).GetSaveData();
        }
        else
        {
            Debug.Log("Saved");
            saveData.questToStart = null;
        }
        if (questToComplete != null)
        {
            saveData.questToComplete = (new QuestGame(questToComplete)).GetSaveData();

        }
        savedQuest = saveData;
        return null;

    }

    public void RestoreState(object state)
    {
        questToStart = null;
        Debug.Log("Saved");
        var saveData = state as NPCQuestSaveData;
        if (saveData != null)
        {
            activeQuest = (saveData.activeQuest != null) ? new QuestGame(saveData.activeQuest) : null;
            questToStart = (saveData.questToStart != null) ? new QuestGame(saveData.questToStart).Base : null;
            questToComplete = (saveData.questToComplete != null) ? new QuestGame(saveData.questToComplete).Base : null;

        }
        else
        {
            activeQuest = null;
        }

    }
}
[System.Serializable]
public class NPCQuestSaveData
{
    public QuestSaveData activeQuest;
    public QuestSaveData questToStart;
    public QuestSaveData questToComplete;
}
