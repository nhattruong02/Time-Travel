using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class ItemGiver : MonoBehaviour,ISavable
{
    [SerializeField] Item item;
    [SerializeField] int count ;
    [SerializeField] Dialog dialog;
    bool used = false;
    string dialogText = "";
    public IEnumerator GiveItem(PlayerController player)
    {
        /*        yield return DialogManager.Instance.ShowDialog(dialog);
        */
        player.GetComponent<Inventory>().AddItem(item, count);
        used = true;
        /*        string dialogText = $"Bạn nhận được {item.Name}";
        */
        if (count > 1)
        {
            dialogText = $"Bạn nhận được {count} {item.Name}";
            yield return DialogManager.Instance.ShowDialogText(dialogText);
        }
    }
    public bool CanBeGiven()
    {
        return item != null && !used;
    }

    public object CaptureState()
    {
        return used;
    }

    public void RestoreState(object state)
    {
        used = (bool)state;
    }
}
