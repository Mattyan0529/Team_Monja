using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Option")]
public class Scriptableobject_SM : ScriptableObject
{
    public bool isVibrationEnabled; // コントローラーのバイブ機能
    public bool isMapRotationEnabled; // マップの回転状態
}
