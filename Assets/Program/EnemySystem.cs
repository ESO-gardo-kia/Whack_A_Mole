using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool isDeath;
    [SerializeField]
    private float latencyAfterPop;//èoåªå„ÇÃë“ã@éûä‘
    private float countLatencyAfterPop;
    void Start()
    {
        isDeath = false;
    }
    void Update()
    {
        countLatencyAfterPop += Time.deltaTime;
        if(latencyAfterPop < countLatencyAfterPop)
        {
            animator.SetTrigger("isDeath");
        }
    }
    private void OnMouseDown()
    {
        if (!isDeath)
        {
            animator.SetTrigger("isDeath");
            GameManager.gameScore++;
            isDeath = true;
        }
    }
    public void DeathMethod()
    {
        Destroy(gameObject);
    }
}
