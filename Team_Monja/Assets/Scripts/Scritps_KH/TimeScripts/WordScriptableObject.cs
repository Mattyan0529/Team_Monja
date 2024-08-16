using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject", fileName ="NewScriptableObject")]
public class WordScriptableObject : ScriptableObject
{
    [SerializeField]
    private string word;

    public string Word
    {
        get { return word; }
    }
}
