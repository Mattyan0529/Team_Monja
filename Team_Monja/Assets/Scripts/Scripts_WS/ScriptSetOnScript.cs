using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSetOnScript : MonoBehaviour
{
    //使用可能にするスクリプトを指定
    [SerializeField] private Component SetOnScript;
    //使用不可にするスクリプトを指定
    [SerializeField] private Component SetOffScript;
    //Enterキーで使用可能にするかどうか
    [SerializeField] private bool UseEnterKey;
    //Escapeキーで使用可能にするかどうか
    [SerializeField] private bool UseEscapeKey;
    //左クリックで使用可能にするかどうか
    [SerializeField] private bool UseLeftMouseButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
