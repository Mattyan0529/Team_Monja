using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadDecision_MT : MonoBehaviour
{
    private StatusManager_MT _statusManager;
    private CharacterAnim_MT _characterAnim;

    // �ǋL�F�k
    private MonsterRandomWalk_KH _monsterRandomWalk = default;
    private MonsterSkill_KH _monsterSkill = default;
    private PlayerRangeInJudge_KH _playerRangeInJudge = default;
    private PlayerMove_MT _playerMove = default;
    private PlayerSkill_KH _playerSkill = default;
    private NormalAttack_KH _normalAttack = default;
    private CameraManager_MT _cameraManager = default;

    private bool _isAlive = true;
    private bool _coroutineSwitch = true;
    private float _slowTimeScale = 0.5f; //���񂾂񎞊Ԃ��~�܂鏈���Ŏg��
    
    private Vector3 _deadCameraPosition = new Vector3(0, 6, -2);�@//���񂾂Ƃ��̃J�����̈ʒu
    private Vector3 _deadCameraRotation = new Vector3(90, 180, 0);  // ���񂾂Ƃ��̃J�����̌���

    //�Q�[���I�[�o�[�̉�ʉ摜
    [SerializeField] private GameObject _gameOverImage = default;
    //canvas
    [SerializeField] private GameObject _canvasPlayer = default;

    void Start()
    {
        _statusManager = GetComponent<StatusManager_MT>();
        _characterAnim = GetComponent<CharacterAnim_MT>();
        _cameraManager = GetComponent<CameraManager_MT>();
        // �ǋL�F�k
        _monsterRandomWalk = GetComponent<MonsterRandomWalk_KH>();
        _monsterSkill = GetComponent<MonsterSkill_KH>();
        _playerRangeInJudge = GetComponent<PlayerRangeInJudge_KH>();
        _playerMove = GetComponent<PlayerMove_MT>();
        _playerSkill = GetComponent<PlayerSkill_KH>();
        _normalAttack = GetComponent<NormalAttack_KH>();
    }

    void Update()
    {
        if (IsDeadDecision())
        {
            //�v���C���[�Ȃ玀�񂾂Ƃ��ɃX���[���[�V�����ɂ���
            if(CompareTag("Player") && _coroutineSwitch)
            {
                //�J����������ł��Ȃ�����
                _cameraManager.enabled = false;

                StartCoroutine(GameOverCoroutine());
                _coroutineSwitch = false;
            }

            _characterAnim.NowAnim = "Die";

            if (_isAlive)
            {
                EnemyStop();
            }
        }
    }

    /// <summary>
    /// ����ł��邩
    /// </summary>
    /// <returns></returns>
    public bool IsDeadDecision()
    {
        if (_statusManager.HP <= 0)
        {
            return true;
        }
        else
        {
            _isAlive = true;
            return false;
        }
    }

    /// <summary>
    /// �H�ׂ�ꂽ�����X�^�[�ƃv���C���[�̓������~�߂�@�ǋL�F�k
    /// </summary>
    private void EnemyStop()
    {
        _playerRangeInJudge.enabled = false;
        _monsterRandomWalk.enabled = false;
        _monsterSkill.enabled = false;
        _playerMove.enabled = false;
        _playerSkill.enabled = false;
        _normalAttack.enabled = false;

        _isAlive = false; 
    }

    private IEnumerator GameOverCoroutine()
    {
        //���b�҂̂�
        float slowTime = 1.5f;
        //�J�������擾
        Camera mainCamera = Camera.main;

      
        // �e�I�u�W�F�N�g���ݒ肳��Ă��邩�m�F
        if (_canvasPlayer != null)
        {
            // �e�I�u�W�F�N�g�̑S�Ă̎q�I�u�W�F�N�g���擾
            foreach (Transform child in _canvasPlayer.transform)
            {
                // �q�I�u�W�F�N�g���A�N�e�B�u�ɂ���
                child.gameObject.SetActive(false);
            }
        }

        //�X���[���[�V�����J�n
        Time.timeScale = _slowTimeScale;

        yield return new WaitForSeconds(slowTime);

        //���񂾂Ƃ��̃J�����𒲐�
        mainCamera.transform.localPosition = _deadCameraPosition;
        mainCamera.transform.localRotation = Quaternion.Euler(_deadCameraRotation);

        //���Ԓ�~
        Time.timeScale = 0;

        //�Q�[���I�[�o�[�̉摜���o��
        _gameOverImage.SetActive(true);
    }

}
