using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.PATROL;

    private Transform playerTr;
    private Transform enemyTr;
    //Animator 컴포넌트 저장 변수
    private Animator animator;



    public float attackDist = 5.0f;
    public float traceDist = 10.0f;

    public bool isDie = false;

    private WaitForSeconds ws;
    // 이동을 제어하는 변수
    private MoveAgent moveAgent;

    private EnemyFire enemyFire;

    //애니메이터 컨트롤러에 정의한 파라미터의 해시값 추출
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    private readonly int hashOffset = Animator.StringToHash("Offset");
    private readonly int hashWalkSpeed = Animator.StringToHash("WalkSpeed");


    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            playerTr = player.GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        animator = GetComponent<Animator>();
        enemyFire = GetComponent<EnemyFire>();

        ws = new WaitForSeconds(0.3f);

        animator.SetFloat(hashOffset, Random.Range(0.0f, 1.0f));
        animator.SetFloat(hashWalkSpeed, Random.Range(1.0f, 1.2f));
    }

    private void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        //캐릭터 사망까지 무한 루프
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.PATROL:
                    enemyFire.isFire = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;

                case State.TRACE:
                    enemyFire.isFire = false;
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;

                case State.ATTACK:
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    if(enemyFire.isFire == false)
                    {
                        enemyFire.isFire = true;
                    }
                    break;

                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;
                    moveAgent.Stop();
                    animator.SetTrigger(hashDie);
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;

            }
        }

    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;

            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if(dist <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(hashSpeed, moveAgent.speed);
        
    }
}
