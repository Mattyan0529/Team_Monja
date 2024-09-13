using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestEnemyFinder_MT : MonoBehaviour
{
    /// <summary>
    /// �w�肳�ꂽ���X�g���ŁA�ł��߂��I�u�W�F�N�g���擾���郁�\�b�h
    /// </summary>
    /// <param name="objectsInTrigger">�g���K�[���ɂ���I�u�W�F�N�g�̃��X�g</param>
    /// <param name="referencePoint">��r�����_�i�v���C���[�Ȃǁj</param>
    /// <returns>�ł��߂��I�u�W�F�N�g��Collider��Ԃ��B���X�g����܂���null�̏ꍇ��null��Ԃ��B</returns>
    public Collider GetClosestObject(List<Collider> objectsInTrigger, Transform referencePoint)
    {
        // �I�u�W�F�N�g�̃��X�g����null�Ȃ珈�����Ȃ�
        if (objectsInTrigger == null || objectsInTrigger.Count == 0)
        {
            return null;
        }

        Collider closestObject = null; // �ł��߂��I�u�W�F�N�g��ێ�����ϐ�
        float closestDistance = Mathf.Infinity; // �����l�͖�����ɐݒ�i�ǂ̃I�u�W�F�N�g�ł��������������Ȃ�悤�Ɂj

        // ���X�g���̂��ׂẴI�u�W�F�N�g�ɂ��ċ������v�Z
        foreach (Collider obj in objectsInTrigger)
        {
            // ��_�ireferencePoint�j����I�u�W�F�N�g�܂ł̋������v�Z
            float distance = Vector3.Distance(referencePoint.position, obj.transform.position);

            // ���݂̍ł��߂����������������ꍇ�A�ł��߂��I�u�W�F�N�g���X�V
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj; // �ł��߂��I�u�W�F�N�g�Ƃ��Đݒ�
            }
        }

        return closestObject; // �ł��߂��I�u�W�F�N�g��Ԃ�
    }
}
