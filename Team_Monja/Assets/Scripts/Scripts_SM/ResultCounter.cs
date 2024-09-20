using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResultCounter")]
public class ResultCounter : ScriptableObject
{
    public int killCount; // キルカウント
    public int timeCount; // タイムカウント
}