using UnityEngine;

public class DragPlayerToBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetWayPoint = default;
    private GameObject _player = default;
    private GameObject _nearPlayerArea = default;
    private Transform[] _wayPoints = default;
    private int[,] _nextWayPointTable = default;
    private Transform _nextWayPoint = default;
    private Transform _currentWayPoint = default;

    private SearchWayPointTwoDimensionalArray _searchWayPointTwoDimensionalArray = default;
    private EnemyMove _enemyMove;

    private float _speed = 100f;
    private float _followStopDistance = 0.5f;
    private float _shortestDistance = default;

    // ���ړ����Ă����ԂȂ�false �ړ����I���T���҂����ǂ���
    private bool _isSearch = true;
    // ���C���̃E�F�C�|�C���g�ɓ͂��邽�߂̃t���O�i�ŏ��̈��̒T�����ǂ����j
    private bool _isFirst = true;
    // �v���C���[�����������Ԃ��ǂ���
    private bool _isdrag = false;

    public bool IsDrag
    {
        set { _isdrag = value; }
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _nearPlayerArea = GameObject.FindGameObjectWithTag("NearPlayerArea");
        _searchWayPointTwoDimensionalArray =
            _nearPlayerArea.gameObject.GetComponent<SearchWayPointTwoDimensionalArray>();
        _enemyMove = GetComponent<EnemyMove>();
    }

    private void Update()
    {
        if (!_isdrag) return;

        if (_isFirst)
        {
            PlayerToMainWayPoint();
        }
        else
        {
            DragPlayer();
        }
    }

    /// <summary>
    /// �v���C���[���{�X�ֈ�������Ƃ��̍ŏ��̂P��̂݌Ăяo��
    /// </summary>
    public void PlayerToMainWayPoint()
    {
        SearchNearMainWayPoint();
        _isFirst = false;
    }

    /// <summary>
    /// PlayerToMainWayPoint���ɌĂяo���Ă���
    /// </summary>
    public void DragPlayer()
    {
        if (_isSearch)
        {
            SearchTargetWayPoint(_currentWayPoint);
        }
        else
        {
            MoveCharactor();
        }
    }

    /// <summary>
    /// �m�[�h�e�[�u������T�����āA����WayPoint��Ԃ�
    /// </summary>
    public void SearchTargetWayPoint(Transform myWayPoint)
    {
        _nextWayPointTable = _searchWayPointTwoDimensionalArray.NextWayPointTable;

        int targetIndex = 0;
        int myIndex = 0;

        // wayPoints���̃v���C���[�ɋ߂�WayPoint�̓Y������T��
        for (int i = 0; i < _wayPoints.Length; i++)
        {
            if (_targetWayPoint.name == _wayPoints[i].gameObject.name)
            {
                // �m�[�h�e�[�u����2��ڂ���Ȃ̂�+1
                targetIndex = i + 1;
            }

            if (myWayPoint.gameObject.name == _wayPoints[i].gameObject.name)
            {
                // �m�[�h�e�[�u����2�s�ڂ���Ȃ̂�+1
                myIndex = i + 1;
            }
        }

        int nextWayPointIndex = _nextWayPointTable[myIndex, targetIndex];

        if (nextWayPointIndex == 0) return;

        // �z���0�I���W�������m�[�h�e�[�u����1�I���W���Ȃ̂�-1����
        Transform nextWayPoint = _wayPoints[nextWayPointIndex - 1];

        _nextWayPoint = nextWayPoint;
        _isSearch = false;
    }

    /// <summary>
    /// ���ݒn���炢���΂�߂�WayPoint��T���i�ŏ��j
    /// </summary>
    private void SearchNearMainWayPoint()
    {
        bool isFirst = true;

        GameObject wayPoint = _enemyMove.WayPoint;
        _wayPoints = new Transform[wayPoint.transform.childCount];

        for (int i = 0; i < wayPoint.transform.childCount; i++)
        {
            _wayPoints[i] = wayPoint.transform.GetChild(i).transform;
        }

        // WayPoint�ЂƂ��ƌ��ݒn���ׁA�����΂�߂�WayPoint��target�Ƃ���
        foreach (Transform child in _wayPoints)
        {
            // �ŏ��͂Ƃ肠��������WayPoint������
            if (isFirst)
            {
                _nextWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_nextWayPoint.transform.position, gameObject.transform.position);
                isFirst = false;
            }

            // ����WayPoint�ƌ��ݒn�̋���
            float thisWayPointDistance = Vector3.Distance
                (child.transform.position, gameObject.transform.position);

            if (Mathf.Abs(_shortestDistance) > Mathf.Abs(thisWayPointDistance))
            {
                _nextWayPoint = child;
                _shortestDistance = Vector3.Distance
                    (_nextWayPoint.transform.position, gameObject.transform.position);
            }
        }

        _isSearch = false;
    }

    /// <summary>
    /// �L�����N�^�[�𓮂���
    /// </summary>
    private void MoveCharactor()
    {
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, _nextWayPoint.transform.position, _speed * Time.deltaTime);

        // �ړI�n�̕����Ɍ����悤�ɏC��(��]��Y���̂�)
        Vector3 directionVector = _nextWayPoint.position - gameObject.transform.position;
        Quaternion directionQuaternion = Quaternion.LookRotation(directionVector, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, directionQuaternion.eulerAngles.y, 0f);

        Vector3 nowPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetPos = new Vector3(_nextWayPoint.transform.position.x, 0f, _nextWayPoint.transform.position.z);

        // ������x�߂��Ȃ����玟�̖ړI�n��
        if (Vector3.SqrMagnitude(targetPos - nowPos) < Mathf.Pow(_followStopDistance, 2))
        {
            _currentWayPoint = _nextWayPoint;
            _isSearch = true;
        }

    }
}
