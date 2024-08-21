using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static string previousSceneName;

    public void LoadPreviousScene()
    {
        // Load scene trước đó bằng tên đã lưu
        SceneManager.LoadScene(previousSceneName);
    }

    public void BackFail()
    {
        SceneManager.LoadScene("VillageScreen");
    }
    public void PlayAgain()
    {
        LoadPreviousScene();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("VillageScreen");
    }

    private void Start()
    {
        try
        {
            Inventory inventory = Inventory.GetInventory();
            inventory.RestoreState(NPC.savedState);
            NPC npc = FindAnyObjectByType<NPC>().GetComponent<NPC>();
            npc.RestoreState(NPC.savedQuest);
        }
        catch (NullReferenceException) { }
    }

}
