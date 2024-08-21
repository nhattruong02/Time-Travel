using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
public enum GameState { FreeRoam,Dialog}
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] InventoryUI inventoryUI;
    GameState state;
    private void Awake()
    {
        ItemDB.Init();
    }
    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.FreeRoam;
            }
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog) {
            DialogManager.Instance.HandleUpdate();
        }
    }
    public void OpenBag()
    {
        inventoryUI.gameObject.SetActive(true);
    }
    public void CloseBag()
    {
        inventoryUI.gameObject.SetActive(false);
    }
}
