using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGate_MT : MonoBehaviour
{
    [SerializeField] private GameObject _canvasObjBoss = default;
    [SerializeField] private GameObject _canvasObjPlayer = default;
    [SerializeField] private GameObject _gateObj = default;
    [SerializeField] private GameObject _pressF = default;
    [SerializeField] private VideoPlayerController_MT _bossVideo;
    // �X�J�C�{�b�N�X�p�̃}�e���A��
    [SerializeField] private Material _afterSkyboxMaterial;

    [SerializeField] private DragPlayerToBoss_KH _damonHand;
    // �ǋL�F�k
    private TimeManager_KH _timeManager = default;
    private BackGroundMusicManagement_KH _backGroundMusicManagement = default;
    private GameObject[] _enemies = default;

    private Canvas _canvasBoss;
    private Collider _collider;

    private bool isClosed = false;
    private bool isDeleteEnemy = false;

    public bool IsClosed { get => isClosed; set => isClosed = value; }

    private void Start()
    {
        _canvasBoss = _canvasObjBoss.GetComponent<Canvas>();
        _collider = _gateObj.GetComponent<Collider>();

        // �ǋL�F�k
        _timeManager = _canvasObjPlayer.GetComponentInChildren<TimeManager_KH>();
        _backGroundMusicManagement = GameObject.FindGameObjectWithTag("ResidentScripts").GetComponent<BackGroundMusicManagement_KH>();
    }

    private void Update()
    {
        if (isDeleteEnemy)
        {
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].gameObject.SetActive(false);
            }

            isDeleteEnemy = false;
        }
    }


    /// <summary>
    /// �{�X�����ɓ����悤�ɂȂ�
    /// </summary>
    public void OpenGate()
    {
        _collider.isTrigger = true;
        isClosed = true;
        _pressF.SetActive(false);

        // �ǋL�F�k
        _timeManager.IsInCastle = true;
    }

    public void CloseGate()
    {
        isClosed = true;
        _collider.isTrigger = false;
        _pressF.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {

        if ((other.gameObject.CompareTag("Player") && !_timeManager.IsInCastle ) && !_damonHand.Isdrag)
        {
            _pressF.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) || Input.GetAxis("Submit") > 0)
            {

                OpenGate();
                _backGroundMusicManagement.PlayBossMusic();

                isDeleteEnemy = true;
                //������Đ�
                _bossVideo.PlayVideo();
            }
        }
    }
    //�{�X�����ɓ�����
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player")�@&& isClosed) && other.isTrigger == false)
        {
            _timeManager.IsInCastle = true;
            _collider.isTrigger = false;
            _pressF.SetActive(false);
            _canvasBoss.enabled = true;
            ChangeSkybox(_afterSkyboxMaterial);
            //�J�������{�X�̕����Ɍ�����
            Camera.main.transform.parent.rotation = Quaternion.Euler(0,0,0);
        }

    }
    // �X�J�C�{�b�N�X��ύX���郁�\�b�h
    private void ChangeSkybox(Material newMaterial)
    {
        if (newMaterial != null)
        {
            RenderSettings.skybox = newMaterial;
            // �������̔��˂��X�V�������ꍇ�͈ȉ��̍s��ǉ�
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("�V�����X�J�C�{�b�N�X�̃}�e���A�����w�肳��Ă��܂���I");
        }
    }

}
