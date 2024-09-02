using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPreventionScript : MonoBehaviour
{
    private Vector3 _monsterPosition;//このモンスターの位置
    private Vector3 _monsterRotation;//このモンスターの向き
    private Vector3 _monsterScale;//このモンスターの大きさ

    private bool rayHit;//CircleCast（円形の判定）に透明な壁が入ったか
    private float wallLayer = 6;//透明な壁のLayerを入れる
    private LayerMask wallLayerMask = 6;//透明な壁のLayerを入れる

    private float monsterStopPositionX;
    private float monsterStopPositionZ;


    // Start is called before the first frame update
    void Start()
    {
        _monsterPosition = this.transform.position;
        _monsterRotation = this.transform.rotation.eulerAngles;
        _monsterScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RayCircleCast();
    }

    void RayCircleCast()
    {
        //円形のRAYを発射（始点、半径、向き、距離、判定対象のレイヤーマスク、判定対象の高さ上限、下限）
        rayHit = Physics2D.CircleCast(_monsterPosition, _monsterScale.y, _monsterRotation, 0f, wallLayerMask, _monsterScale.y, 0f);
    }

    private void OnDrawGizmos()
    {   //Gizmosで円形RAYを描画
        Gizmos.DrawWireSphere(_monsterScale, _monsterScale.y);
        //描画色の変更
        Gizmos.color = Color.red;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (rayHit && collision.gameObject.layer == wallLayer)
        {
            Vector3 preventWallPosition = collision.gameObject.transform.position;

            float differenceX = _monsterPosition.x - preventWallPosition.x;
            float differenceZ = _monsterPosition.z - preventWallPosition.z;

            if(differenceX <= differenceZ)//X座標がZ座標よりも近い、もしくは全く同じ場合
            {
                monsterStopPositionX = _monsterPosition.x;

                //取得したX座標が現在地のX座標より小さい,かつX座標をーにしようとしている場合
                if (monsterStopPositionX < _monsterPosition.x || Input.GetAxis("Horizontal") < 0)
                {
                    _monsterPosition.x = monsterStopPositionX;
                }
                //取得したX座標が現在地のX座標より大きい,かつX座標を＋にしようとしている場合
                else if (monsterStopPositionX > _monsterPosition.x || Input.GetAxis("Horizontal") > 0)
                {
                    _monsterPosition.x = monsterStopPositionX;
                }
            }
            else//Z座標がX座標よりも近い場合
            {
                monsterStopPositionZ = _monsterPosition.z;

                //取得したZ座標が現在地のZ座標より小さい,かつZ座標をーにしようとしている場合
                if (monsterStopPositionZ < _monsterPosition.z || Input.GetAxis("Vertical") < 0)
                {
                    _monsterPosition.z = monsterStopPositionZ;
                }
                //取得したZ座標が現在地のZ座標より大きい,かつZ座標を＋にしようとしている場合
                else if (monsterStopPositionZ > _monsterPosition.z || Input.GetAxis("Vertical") > 0)
                {
                    _monsterPosition.z = monsterStopPositionZ;
                }
            }

        }
    }
}
