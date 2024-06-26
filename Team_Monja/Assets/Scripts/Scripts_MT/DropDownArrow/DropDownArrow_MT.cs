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
        //親オブジェクトから取得
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
        //自動で回転
        transform.Rotate(0, 0, _rotateSpeed);
        //上下にふわふわ浮く処理
        transform.position = new Vector3(transform.position.x,
            _nowPositionY + Mathf.PingPong(Time.time, _pingPongValue),
            transform.position.z);
    }

}
