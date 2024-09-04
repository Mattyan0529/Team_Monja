using UnityEngine;

public class GhostFloat : MonoBehaviour
{
    // —h‚ê‚éU•‚Æ‘¬“x‚ğİ’è‚·‚é
    [SerializeField] private float _amplitude = 0.5f; // U•
    [SerializeField] private float _frequency = 1f; // ü”g”

    private Vector3 _startPos;

    void Start()
    {
        // ‰ŠúˆÊ’u‚ğ•Û‘¶‚·‚é
        _startPos = transform.position;
    }

    void Update()
    {
        // Y²•ûŒü‚Ì—h‚ê‚ğŒvZ‚·‚é
        float yOffset = Mathf.Sin(Time.time * _frequency) * _amplitude;

        // V‚µ‚¢ˆÊ’u‚ğİ’è‚·‚é
        transform.position = new Vector3(_startPos.x, _startPos.y + yOffset, _startPos.z);
    }
}
