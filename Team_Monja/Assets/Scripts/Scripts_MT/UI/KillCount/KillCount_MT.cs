using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount_MT : MonoBehaviour
{

    private int _allCount = 0;
    private int _evilMageCount = 0;
    private int _lizardWarriorCount = 0;
    private int _mushroomCount = 0;
    private int _orcCount = 0;
    private int _skeletonCount = 0;
    private int _slimeCount = 0;
    private int _bossCount = 0;

    public void KillCountUP(string name)
    {
        switch (name)
        {
            case "EvliMage":
                _evilMageCount++;
                _allCount++;
                Debug.Log(_evilMageCount);
                break;
            case "LizardWarrior":
                _lizardWarriorCount++;
                _allCount++;
                Debug.Log(_lizardWarriorCount);
                break;
            case "Mushroom":
                _mushroomCount++;
                _allCount++;
                Debug.Log(_mushroomCount);
                break;
            case "Orc":
                _orcCount++;
                _allCount++;
                Debug.Log(_orcCount);
                break;
            case "Skeleton":
                _skeletonCount++;
                _allCount++;
                Debug.Log(_skeletonCount);
                break;
            case "Slime":
                _slimeCount++;
                _allCount++;
                Debug.Log(_slimeCount);
                break;
            case "Boss":
                _bossCount++;
                _allCount++;
                Debug.Log(_bossCount);
                break;
            case "":
                break;
        }
    }

}
