using UnityEngine;

public class MonsterRandomWalk_KH : MonoBehaviour
{
    private Vector3 _monsterRotation = new Vector3(0f, 0f, 0f);
    private Rigidbody _rigidbody = default;
    //松本
    CharacterAnim_MT _characterAnim = default;


    private float _updateTime = 2f;
    private float _elapsedTime = 0f;

    private float _speed = 4000f;
    private float _maxSpeed = 2f;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        //松本
        _characterAnim = GetComponent<CharacterAnim_MT>();
        MonsterWalk();
    }

    void Update()
    {
        UpdateTime();
        MonsterWalk();
    }

    /// <summary>
    /// モンスターを前方に動かす
    /// </summary>
    private void MonsterWalk()
    {
        // いずれかの方向で最大速度に達している場合は加速しない
        if (_rigidbody.velocity.x > _maxSpeed | _rigidbody.velocity.z > _maxSpeed) return;
        if (_rigidbody.velocity.x < -_maxSpeed | _rigidbody.velocity.z < -_maxSpeed) return;

        // 加速
        _rigidbody.AddForce(transform.forward * _speed * Time.deltaTime, ForceMode.Force);

        //松本
        _characterAnim.NowAnim = "Move";
    }

    /// <summary>
    /// モンスターの方向を変える
    /// </summary>
    private void UpdateRotation()
    {
        // 方向をランダムで決める
        _monsterRotation = new Vector3(0f, Random.Range(0f, 360f), 0f);

        // 方向を更新
        _rigidbody.velocity = Vector3.zero;
        gameObject.transform.rotation = Quaternion.Euler(_monsterRotation);
    }

    /// <summary>
    /// 定期的に方向を変える
    /// </summary>
    private void UpdateTime()
    {
        // 時間加算
        _elapsedTime += Time.deltaTime;

        // 規定時間に達していた場合
        if (_elapsedTime > _updateTime)
        {
            UpdateRotation();
            _elapsedTime = 0f;
        }
    }
}
