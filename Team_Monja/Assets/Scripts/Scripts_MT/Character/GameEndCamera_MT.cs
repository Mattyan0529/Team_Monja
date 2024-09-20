using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndCamera_MT : MonoBehaviour
{

    private StatusManager_MT _statusManagerPlayer = default;
    private StatusManager_MT _statusManagerBoss = default;
    private CameraManager_MT _cameraManager = default;

    //canvas
    [SerializeField] private GameObject _canvasPlayer = default;
    //�Q�[���I�[�o�[�̃L�����o�X
    [SerializeField] private GameObject _gameOverCanvas = default;
    //boss�̃L�����o�X
    [SerializeField] private GameObject _bossCanvas = default;



    private float _slowTimeScale = 0.3f; //�X���[���[�V�����̎��̎��ԑ��x
    private Vector3 _deadCameraPosition = new Vector3(0, 0, 0);�@//���񂾂Ƃ��̃J�����̈ʒu
    private Vector3 _deadCameraRotation = new Vector3(0, 0, 0);  // ���񂾂Ƃ��̃J�����̌���
    private bool _isCoroutineActive = false;//�R���[�`�������쒆���ǂ���
    private bool _isGameClear = false;//�Q�[���N���A
    private bool _isGameOver = false;


    public bool IsGameOver
    {
        get { return _isGameOver; }
    }


    private void Start()
    {
        _cameraManager = GameObject.FindWithTag("CameraPos").GetComponent<CameraManager_MT>();
        //�{�X�����������牺�̍s�̃R�����g����
        _statusManagerBoss = GameObject.FindWithTag("Boss").GetComponent<StatusManager_MT>();
    }

    private void Update()
    {
        //�듮��h�~
        if (_statusManagerPlayer.HP > 0 && _isCoroutineActive && !_isGameClear)
        {
            ResetGameOverCorouine();
        }
    }

    private void ResetGameOverCorouine()
    {
        StopCoroutine(GameOverCoroutine());
        _cameraManager.enabled = true;
        Time.timeScale = 1;
    }

    public void SetPlayer(GameObject player)
    {
        _statusManagerPlayer = player.GetComponent<StatusManager_MT>();
    }

    public IEnumerator GameOverCoroutine()
    {
        _isCoroutineActive = true;

        _isGameOver = true;

        float slowTime = 1.75f;

        _deadCameraPosition = new Vector3(0, 6, 0);
        _deadCameraRotation = new Vector3(90, 180, 0);

        _cameraManager.enabled = false;

        _bossCanvas.SetActive(false);

        if (_canvasPlayer != null)
        {
            foreach (Transform child in _canvasPlayer.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        Time.timeScale = _slowTimeScale;

        yield return new WaitForSeconds(slowTime);

        Camera.main.transform.localPosition = _deadCameraPosition;
        Camera.main.transform.localRotation = Quaternion.Euler(_deadCameraRotation);

        Time.timeScale = 0.0001f;

        _gameOverCanvas.SetActive(true);


        _isCoroutineActive = false;
    }

    public IEnumerator GameClearCoroutine()
    {
        _isCoroutineActive = true;
        //�Q�[���N���A��bool��true
        _isGameClear = true;

        //���b�҂̂�
        float slowTime = 1f;

        //�J����������ł��Ȃ�����
        _cameraManager.enabled = false;

        _bossCanvas.SetActive(false);

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

        //���Ԓ�~
        Time.timeScale = 0;

        //�Q�[���N���A�V�[���Ɉړ�
        SceneManager.LoadScene("GameClearScene");

        _isCoroutineActive = false;
    }


}
