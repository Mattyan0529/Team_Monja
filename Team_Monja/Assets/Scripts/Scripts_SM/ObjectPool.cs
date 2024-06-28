using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�v�[���N���X
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject prefab; // �v�[������I�u�W�F�N�g��Prefab

    [SerializeField]
    int poolSize = 10; // �����v�[���T�C�Y

    List<GameObject> pool; // �v�[���̃��X�g

    void Awake()
    {
        pool = new List<GameObject>();
        // �����v�[�����쐬����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // �I�u�W�F�N�g���v�[������擾����
    GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // �v�[��������Ȃ��ꍇ�͐V�����I�u�W�F�N�g�𐶐����Ēǉ�����
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    // �I�u�W�F�N�g���v�[���ɖ߂�
    void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    // �G�t�F�N�g��\�����A��莞�Ԍ�Ƀv�[���ɖ߂�
    void ShowEffect(Vector3 position, Quaternion rotation, Vector3 scale, float duration)
    {
        GameObject effect = GetObject();
        effect.transform.position = position;
        effect.transform.rotation = rotation;
        effect.transform.localScale = scale;
        StartCoroutine(ReturnEffectToPool(effect, duration));
    }

    // ��莞�Ԍ�ɃG�t�F�N�g���v�[���ɖ߂��R���[�`��
    IEnumerator ReturnEffectToPool(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObject(effect);
    }

    // �O������Ăяo����郁�\�b�h�F�G�t�F�N�g��\������
    public void ShowEffectPublic(Vector3 position, Quaternion rotation, Vector3 scale, float duration)
    {
        ShowEffect(position, rotation, scale, duration);
    }
}
