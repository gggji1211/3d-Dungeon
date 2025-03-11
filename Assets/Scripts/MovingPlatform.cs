using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints; // �̵��� ��� ���� �迭
    public float speed = 2f;
    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length == 0) return;

        // ���� ��ǥ �������� �̵�
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // ��ǥ ������ �����ϸ� ���� �������� ����
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // �÷��̾ �÷����� �ڽ����� �����Ͽ� �Բ� �����̰� ��
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // �÷����� ����� �θ� ���� ����
        }
    }
}
