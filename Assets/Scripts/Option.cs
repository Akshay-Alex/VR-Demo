using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Option : MonoBehaviour
{
    public int optionIndex;
    public TextMeshProUGUI optionText;
    public MCQManager mCQManager;
    public void InitializeOption(int index, string text,MCQManager manager)
    {
        optionIndex = index;
        optionText.text = text;
        mCQManager = manager;
    }
    [ContextMenu("select option")]
    public void OptionSelected()
    {
        mCQManager.OptionSelected(optionIndex);
    }
}
