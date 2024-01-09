using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    //순찰지점 저장
    public List<Transform> wayPoints;
    //다음 순찰 지점의 배열
    public int nextIdx = 0;
    private NavMeshAgent agent;
    private Transform enemyTr;
    // 순찰 속도 정의 함수
    private readonly float patrollSpeed = 20.0f;
    private readonly float TraceSpeed = 40.0f;

    //회전속도를 조절하는 변수
    private float damping = 1.0f;

    //순찰여부 판단하는 변수
    private bool _patrolling;

    public bool patrolling
    {
        get { return _patrolling;}
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrollSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }
    //추적 대상의 위치 저장변수
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = TraceSpeed;
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }
    public float speed
    {
        get { return agent.velocity.magnitude; }
    }



    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.autoBraking = false;
        agent.speed = patrollSpeed;

        var group = GameObject.Find("WayPointGroup");

        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);

            nextIdx = Random.Range(0, wayPoints.Count);
        }
        MoveWayPoint();

    }
    private void MoveWayPoint()
    {
        // 최단 거리 경로계산이 끝나지 않았다면 다음을 수행 x
        if (agent.isPathStale) return;
        // 다음 목적지를 wayPoints 배열에서 추출한 위치로 다음목적지를 지정
        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        //바로 정지하기위해 속도를 0으로 설정
        agent.velocity = Vector3.zero;
        //순찰 정지
        _patrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.isStopped == false)
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);

            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

        if (!_patrolling) return;
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f
            && agent.remainingDistance <= 0.5f)
        {
            //nextIdx = ++nextIdx % wayPoints.Count;
            nextIdx = Random.Range(0, wayPoints.Count);

            //다음목적지 이동명령 수행
            MoveWayPoint();
        }
        
    }
}
