using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPreventionScript : MonoBehaviour
{
    private Vector3 _monsterPosition;//���̃����X�^�[�̈ʒu
    private Vector3 _monsterRotation;//���̃����X�^�[�̌���
    private Vector3 _monsterScale;//���̃����X�^�[�̑傫��

    private bool rayHit;//CircleCast�i�~�`�̔���j�ɓ����ȕǂ���������
    private float wallLayer = 6;//�����ȕǂ�Layer������
    private LayerMask wallLayerMask = 6;//�����ȕǂ�Layer������

    private float monsterStopPositionX;
    private float monsterStopPositionZ;


    // Start is called before the first frame update
    void Start()
    {
        _monsterPosition = this.transform.position;
        _monsterRotation = this.transform.rotation.eulerAngles;
        _monsterScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RayCircleCast();
    }

    void RayCircleCast()
    {
        //�~�`��RAY�𔭎ˁi�n�_�A���a�A�����A�����A����Ώۂ̃��C���[�}�X�N�A����Ώۂ̍�������A�����j
        rayHit = Physics2D.CircleCast(_monsterPosition, _monsterScale.y, _monsterRotation, 0f, wallLayerMask, _monsterScale.y, 0f);
    }

    private void OnDrawGizmos()
    {   //Gizmos�ŉ~�`RAY��`��
        Gizmos.DrawWireSphere(_monsterScale, _monsterScale.y);
        //�`��F�̕ύX
        Gizmos.color = Color.red;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (rayHit && collision.gameObject.layer == wallLayer)
        {
            Vector3 preventWallPosition = collision.gameObject.transform.position;

            float differenceX = _monsterPosition.x - preventWallPosition.x;
            float differenceZ = _monsterPosition.z - preventWallPosition.z;

            if(differenceX <= differenceZ)//X���W��Z���W�����߂��A�������͑S�������ꍇ
            {
                monsterStopPositionX = _monsterPosition.x;

                //�擾����X���W�����ݒn��X���W��菬����,����X���W���[�ɂ��悤�Ƃ��Ă���ꍇ
                if (monsterStopPositionX < _monsterPosition.x || Input.GetAxis("Horizontal") < 0)
                {
                    _monsterPosition.x = monsterStopPositionX;
                }
                //�擾����X���W�����ݒn��X���W���傫��,����X���W���{�ɂ��悤�Ƃ��Ă���ꍇ
                else if (monsterStopPositionX > _monsterPosition.x || Input.GetAxis("Horizontal") > 0)
                {
                    _monsterPosition.x = monsterStopPositionX;
                }
            }
            else//Z���W��X���W�����߂��ꍇ
            {
                monsterStopPositionZ = _monsterPosition.z;

                //�擾����Z���W�����ݒn��Z���W��菬����,����Z���W���[�ɂ��悤�Ƃ��Ă���ꍇ
                if (monsterStopPositionZ < _monsterPosition.z || Input.GetAxis("Vertical") < 0)
                {
                    _monsterPosition.z = monsterStopPositionZ;
                }
                //�擾����Z���W�����ݒn��Z���W���傫��,����Z���W���{�ɂ��悤�Ƃ��Ă���ꍇ
                else if (monsterStopPositionZ > _monsterPosition.z || Input.GetAxis("Vertical") > 0)
                {
                    _monsterPosition.z = monsterStopPositionZ;
                }
            }

        }
    }
}
