using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinimapIconSet : MonoBehaviour
{
    private GameObject _player;

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�̈ʒu�ɃA�C�R���̈ʒu��ݒ�
        this.transform.position = new Vector3(_player.transform.position.x,
            _player.transform.position.y + 3,
            _player.transform.position.z);

        // �v���C���[�̉�]�ɍ��킹�āAx����90�x�Az����180�x�ɌŒ肵����]��ݒ�
        // y���̉�]��180�x�����Z���āA�v���C���[�̌����Ɛ^�t�ɂȂ�Ȃ��悤�ɂ���
        this.transform.rotation = Quaternion.Euler(90, _player.transform.rotation.eulerAngles.y + 180, 180);
    }

    // �v���C���[�I�u�W�F�N�g���擾���郁�\�b�h
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
}
