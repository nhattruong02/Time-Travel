using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPersecond;
    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;    
    }
    Dialog dialog;
    int currentLine = 0;
    bool isTyping;
    public IEnumerator ShowDialogText(string text,bool waitforInput = true)
    {
        dialogBox.SetActive(true);
        yield return TypeDialog(text);
        if (waitforInput)
        {
            yield return new WaitUntil(() => Input.touchCount > 0);
            dialogBox.SetActive(false);
        }
    }
    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }
    public void HandleUpdate()
    {
        if (Input.touchCount > 0 && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count) {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                OnHideDialog?.Invoke();
            }
        }
    }
    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach(var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/ lettersPersecond);
        }
        isTyping = false;
    }

}
