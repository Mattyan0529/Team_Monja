using UnityEngine;

public class MoveCompass_KH : MonoBehaviour
{
    // �ړI�n�ƂȂ�I�u�W�F�N�g
    [Header("�{�X������Ă�")]
    [SerializeField]
    private GameObject _destinationObj = default;

    private GameObject _player;

    // �ő�p�x�i�Ђ�����Ԃ��ĉ�ʊO�ɍs���Ȃ��悤�Ɂj
    private float _maxAngle = 70f;


    void Update()
    {
        UpdateOrientation();
    }


    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    /// <summary>
    /// ���ʎ��΂̊p�x��ς���
    /// </summary>
    private void UpdateOrientation()
    {
        // �����̈ʒu
        Vector3 myPos = _player.transform.position;
        // �ړI�n
        Vector3 target = _destinationObj.transform.position;

        // ���ʎ��΂�������p�x
        Vector3 direction = target-myPos;

        direction.y = 0f;

        if (direction == Vector3.zero) return;        // �����Ă��Ȃ��Ƃ��͏��������Ȃ��i_difference��0��������G���[�ɂȂ�j

        // Y���̉�]��Z���̉�]�Ƃ��Ĕ��f����(�v���C���[�̌����Ă���������l������)
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle =_player.transform.rotation.eulerAngles.y - newDirection.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
