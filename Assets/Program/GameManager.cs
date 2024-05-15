using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;//�o������G�̃I�u�W�F�N�g
    [SerializeField]
    private List<GameObject> enemyPopPointList;//�G�̏o������ʒu
    private int popPointCount;//�G���o������ꏊ�̐�
    private int randomNumber;
    [SerializeField]
    private List<int> popPossibleNumber;//�o���o����ԍ�
    [SerializeField]
    private int concurrentPopNumber;//�����ɏo�����鐔
    [SerializeField]
    private float popInterval;//�o���Ԋu
    private float countPopInterval;//�o���Ԋu�J�E���g

    [SerializeField]
    private int limitTime;//��������
    private float countLimitTime;//�������ԃJ�E���g
    static public int gameScore;//�X�R�A
    private bool isGameOver;


    [SerializeField]
    private Text limitTimeText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject EndGamePanel;

    void Start()
    {
        isGameOver = false;
        countLimitTime = limitTime;
        EndGamePanel.SetActive(false);

        Transform parentTransform = transform.Find("EnemyRespawnPointList");
        popPointCount = parentTransform.transform.childCount;
        for (int i = 0; i < popPointCount; i++)
        {
            enemyPopPointList.Add(parentTransform.transform.GetChild(i).gameObject);
        }
    }
    void Update()
    {
        if (!isGameOver)
        {
            //UI����
            if (countLimitTime <= 0)
            {
                countLimitTime = 0;
                EndGamePanel.SetActive(true);
                isGameOver = true;
            }
            else countLimitTime -= Time.deltaTime;
            limitTimeText.text = Mathf.Ceil(countLimitTime).ToString();
            scoreText.text = gameScore.ToString();

            //�G�̏o������
            countPopInterval += Time.deltaTime;
            if (popInterval < countPopInterval)
            {
                for(int i = 0; i < popPointCount; i++)
                {
                    popPossibleNumber.Add(i);
                }
                //��x�ɏo������G�̐�(concurrentPopNumber)�����G�̃|�b�v�������J��Ԃ�
                for (int i = 0; i < concurrentPopNumber; i++)
                {
                    //���ѕς���
                    popPossibleNumber.Sort();
                    //�o���ꏊ�̗v�f�ԍ����o��
                    randomNumber = Random.Range(0, popPossibleNumber.Count);
                    //�v�f�ԍ��Ɋi�[����Ă���ԍ��̏ꏊ�ɏo��������
                    Instantiate(enemyObject, enemyPopPointList[popPossibleNumber[randomNumber]].transform.position, Quaternion.identity, transform.Find("EnemyList"));
                    //���ɏo�������v�f�ԍ�������
                    popPossibleNumber.RemoveAt(randomNumber);
                }
                popPossibleNumber.Clear();
                countPopInterval = 0;
            }
        }
    }
}
