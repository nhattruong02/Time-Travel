using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUp : MonoBehaviour,Interactable, ISavable
{

    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject background;
    public bool Used { get; set; } = false;

    public IEnumerator Interact(Transform initiator)
    {
        if (!Used)
        {
            initiator.GetComponent<Inventory>().AddItem(item);
            Used = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            background.SetActive(false);
            yield return DialogManager.Instance.ShowDialogText($"Đã tìm được {item.Name}");
            if (Input.touchCount > 0)
            {
                ChangeScene();
                SceneManager.LoadScene("VillageScreen");
            }
        }
    }
    public object CaptureState()
    {
        return Used;
    }
    public void RestoreState(object state)
    {
        Used = (bool)state;
        if (Used)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void ChangeScene()
    {
        Inventory inventory = Inventory.GetInventory();
        if (inventory != null)
        {
            NPC.savedState = inventory.CaptureState();
            inventory.CaptureState();
        }
        SceneManager.LoadScene("VillageScreen");
    }
}

