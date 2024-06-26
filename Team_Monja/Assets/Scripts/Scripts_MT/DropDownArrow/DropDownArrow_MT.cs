using UnityEngine;

public class DropDownArrow_MT : MonoBehaviour
{
    private float _rotateSpeed = 1.5f;
    private float _pingPongValue = 0.3f;
    private float _nowPositionY = default;
    private bool _meshEnable = false;

    CharacterDeadDecision_MT characterDeadDecision;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //�e�I�u�W�F�N�g����擾
        characterDeadDecision = GetComponentInParent<CharacterDeadDecision_MT>();

        _nowPositionY = this.transform.position.y;
    }


    void Update()
    {
        if (characterDeadDecision.IsDeadDecision())
        {
            if (CompareTag("Player"))
            {
                meshRenderer.enabled = true;
                MoveArrow();
            }
            return;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }


    private void MoveArrow()
    {
        //�����ŉ�]
        transform.Rotate(0, 0, _rotateSpeed);
        //�㉺�ɂӂ�ӂ핂������
        transform.position = new Vector3(transform.position.x,
            _nowPositionY + Mathf.PingPong(Time.time, _pingPongValue),
            transform.position.z);
    }

}
