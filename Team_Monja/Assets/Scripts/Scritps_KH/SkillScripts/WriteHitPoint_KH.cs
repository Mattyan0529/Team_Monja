using UnityEngine;

public class WriteHitPoint_KH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ���̃L�����N�^�[��HP���X�V����i���炷�j
    /// </summary>
    /// <param name="afterAttackedHitPoint">�U�����ꂽ���HP</param>
    public void UpdateHitPoint(StatusManager_MT attackdStatus, int afterAttackedHitPoint)
    {
        attackdStatus.HP = afterAttackedHitPoint;
    }
}
