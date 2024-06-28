using UnityEngine;

public class MoveCompass_KH : MonoBehaviour
{
    // �ړI�n�ƂȂ�I�u�W�F�N�g
    [Header("�{�X������Ă�")]
    [SerializeField]
    private GameObject _destinationObj = default;

    [SerializeField]
    private GameObject _residentScript = default;

    private PlayerManager_KH _playerManager = default;

    // �ő�p�x�i�Ђ�����Ԃ��ĉ�ʊO�ɍs���Ȃ��悤�Ɂj
    private float _maxAngle = 70f;

    void Start()
    {
        _playerManager = _residentScript.GetComponent<PlayerManager_KH>();
    }

    void Update()
    {
        UpdateOrientation();
    }

    /// <summary>
    /// ���ʎ��΂̊p�x��ς���
    /// </summary>
    private void UpdateOrientation()
    {
        GameObject player = _playerManager.Player;

        // �����̈ʒu
        Vector3 myPos = player.transform.position;
        // �ړI�n
        Vector3 target = _destinationObj.transform.position;

        // ���ʎ��΂�������p�x
        Vector3 direction = target-myPos;

        direction.y = 0f;

        if (direction == Vector3.zero) return;        // �����Ă��Ȃ��Ƃ��͏��������Ȃ��i_difference��0��������G���[�ɂȂ�j

        // Y���̉�]��Z���̉�]�Ƃ��Ĕ��f����(�v���C���[�̌����Ă���������l������)
        Quaternion newDirection = Quaternion.LookRotation(direction, Vector3.up);

        float angle = player.transform.rotation.eulerAngles.y - newDirection.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
