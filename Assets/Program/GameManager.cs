using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;//出現する敵のオブジェクト
    [SerializeField]
    private List<GameObject> enemyPopPointList;//敵の出現する位置
    private int popPointCount;//敵が出現する場所の数
    private int randomNumber;
    [SerializeField]
    private List<int> popPossibleNumber;//出現出来る番号
    [SerializeField]
    private int concurrentPopNumber;//同時に出現する数
    [SerializeField]
    private float popInterval;//出現間隔
    private float countPopInterval;//出現間隔カウント

    [SerializeField]
    private int limitTime;//制限時間
    private float countLimitTime;//制限時間カウント
    static public int gameScore;//スコア
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
            //UI処理
            if (countLimitTime <= 0)
            {
                countLimitTime = 0;
                EndGamePanel.SetActive(true);
                isGameOver = true;
            }
            else countLimitTime -= Time.deltaTime;
            limitTimeText.text = Mathf.Ceil(countLimitTime).ToString();
            scoreText.text = gameScore.ToString();

            //敵の出現処理
            countPopInterval += Time.deltaTime;
            if (popInterval < countPopInterval)
            {
                for(int i = 0; i < popPointCount; i++)
                {
                    popPossibleNumber.Add(i);
                }
                //一度に出現する敵の数(concurrentPopNumber)だけ敵のポップ処理を繰り返す
                for (int i = 0; i < concurrentPopNumber; i++)
                {
                    //並び変える
                    popPossibleNumber.Sort();
                    //出現場所の要素番号を出す
                    randomNumber = Random.Range(0, popPossibleNumber.Count);
                    //要素番号に格納されている番号の場所に出現させる
                    Instantiate(enemyObject, enemyPopPointList[popPossibleNumber[randomNumber]].transform.position, Quaternion.identity, transform.Find("EnemyList"));
                    //既に出現した要素番号を消す
                    popPossibleNumber.RemoveAt(randomNumber);
                }
                popPossibleNumber.Clear();
                countPopInterval = 0;
            }
        }
    }
}
