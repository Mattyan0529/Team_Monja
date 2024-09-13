using UnityEngine;

public class CameraBlock_TH : MonoBehaviour
{
    public Camera camera; // �J�����̎Q��
    public float cameraDistance = 5.0f; // �J�����ƃv���C���[�̋���
    public float smoothSpeed = 10.0f; // �J�����̃X���[�Y�ȓ����̃X�s�[�h
    public LayerMask collisionMask; // �Փ˂����o���郌�C���[�}�X�N�i�ǂȂǁj

    private Vector3 desiredCameraPos; // �J�����̖ڕW�ʒu
    private Vector3 smoothVelocity; // �J�����̃X���[�Y�ȓ�����⏕���邽�߂̕ϐ�

    // �J�����̍ŏ��������w�肵�āA�n�ʂɖ��܂�Ȃ��悤�ɂ���
    public float cameraMinHeight = 1.0f;

    void LateUpdate()
    {
        if (camera == null)
        {
            Debug.LogWarning("�J���������蓖�Ă��Ă��܂���B");
            return;
        }

        // �v���C���[��Transform���擾
        Transform player = transform;

        // �v���C���[����J�����܂ł̕������v�Z
        Vector3 directionToCamera = (camera.transform.position - player.position).normalized;

        // �v���C���[����J�����܂ł̒ʏ�̈ʒu���v�Z
        desiredCameraPos = player.position - directionToCamera * cameraDistance;

        // ���݂̃J������Y������x�ۑ�
        float originalCameraHeight = desiredCameraPos.y;

        // �v���C���[����J�����Ɍ����ă��C�L���X�g�̕����x�N�g�����C��
        Vector3 rayDirection = camera.transform.position - player.position;

        // ���C�L���X�g���������邽�߂ɁADebug.DrawRay��ǉ�
        Debug.DrawRay(player.position, rayDirection, Color.red);

        // �v���C���[����J�����Ɍ����ă��C���΂��A��Q�������邩���m�F
        RaycastHit hit;
        bool hitDetected = Physics.Raycast(player.position, rayDirection, out hit, rayDirection.magnitude, collisionMask);

        if (hitDetected)
        {
            // ��Q���Ƀq�b�g�����ꍇ�A���̎�O�ɃJ������z�u
            float distanceToHit = hit.distance;
            desiredCameraPos = player.position + rayDirection.normalized * (distanceToHit - 0.2f);

            // �q�b�g�����I�u�W�F�N�g�̖��O���R���\�[���ɏo��
            Debug.Log("���C���q�b�g�����I�u�W�F�N�g: " + hit.collider.gameObject.name);
        }
        else
        {
            // ���C�L���X�g���q�b�g���Ȃ������ꍇ�̏���
            Debug.Log("���C�����ɂ��q�b�g���܂���ł����B");
        }

        // �J�����̍����͌��̍������ێ�����
        desiredCameraPos.y = Mathf.Max(desiredCameraPos.y, cameraMinHeight);

        // �X���[�Y�ɃJ������ڕW�ʒu�Ɉړ�������
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, desiredCameraPos, ref smoothVelocity, 1 / smoothSpeed);
    }
}
