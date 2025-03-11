using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints; // 이동할 경로 지점 배열
    public float speed = 2f;
    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length == 0) return;

        // 현재 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // 목표 지점에 도달하면 다음 지점으로 변경
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // 플레이어를 플랫폼의 자식으로 설정하여 함께 움직이게 함
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // 플랫폼을 벗어나면 부모 관계 해제
        }
    }
}
