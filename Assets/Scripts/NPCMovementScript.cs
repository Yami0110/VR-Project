using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMovementScript : MonoBehaviour
{
    public enum MovementType { Loop, Path }
    public MovementType movementType;

    public List<Transform> pathPoints;
    public float speed = 2f;
    public Transform sensor;
    public float detectionRange = 10f;
    public Transform player;

    private int index = 0;
    private int direction = 1;
    private bool chasingPlayer = false;
    private bool isStunned = false;
    private Vector3 startPosition;

    private Animator animator; // Animator komponent

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>(); // Načítame Animator z GameObjectu
    }

    void Update()
    {
        if (isStunned) return; // Ak je ogromený (stun), nič nerob

        if (chasingPlayer)
        {
            if (player != null)
            {
                // Nastavíme cieľ tak, aby mal rovnaké Y ako Robber (nedvíhame sa do výšky)
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                Vector3 toPlayer = targetPosition - transform.position;

                if (toPlayer != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(toPlayer.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }

                if (Vector3.Distance(transform.position, targetPosition) > detectionRange)
                {
                    transform.position = pathPoints[0].position;
                    chasingPlayer = false;
                    index = 0;
                    direction = 1;
                }
            }
        }
        else
        {
            if (pathPoints.Count < 2) return;

            Vector3 targetPos = pathPoints[index].position;
            Vector3 moveDir = (targetPos - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (moveDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }

            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                switch (movementType)
                {
                    case MovementType.Loop:
                        index = (index + 1) % pathPoints.Count;
                        break;

                    case MovementType.Path:
                        index += direction;
                        if (index >= pathPoints.Count)
                        {
                            index = pathPoints.Count - 2;
                            direction = -1;
                        }
                        else if (index < 0)
                        {
                            index = 1;
                            direction = 1;
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 directionToPlayer = (other.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, other.transform.position);

            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer))
            {
                if (hit.transform.root == other.transform.root && other.CompareTag("Player"))
                {
                    player = other.transform;
                    chasingPlayer = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            if (!isStunned)
            {
                StartCoroutine(StunCoroutine());
            }
        }
        else if (collision.collider.CompareTag("Player"))
        {
            if (!isStunned)
            {
                StartCoroutine(PunchCoroutine());

                // Zobraz Game Over menu
                VRMenuController menu = FindObjectOfType<VRMenuController>();
                if (menu != null)
                {
                    menu.ShowGameOver();
                }
            }
        }
    }

    private IEnumerator StunCoroutine()
    {
        isStunned = true;
        
        if (animator != null)
        {
            animator.SetTrigger("Stun"); // Trigger pre Stun animáciu
        }

        yield return new WaitForSeconds(2f); // 3 sekundy čakania

        if (animator != null)
        {
            animator.SetTrigger("Run"); // Späť na Run animáciu
        }

        yield return new WaitForSeconds(0.5f);

        isStunned = false;
    }

    private IEnumerator PunchCoroutine()
    {
        isStunned = true; // Krátko stopneme pohyb počas úderu

        if (animator != null)
        {
            animator.SetTrigger("Punch"); // Trigger pre Punch animáciu
        }

        yield return new WaitForSeconds(1f); // napr. 1 sekunda na prehratie Punch animácie

        isStunned = false; // Opäť povolíme pohyb
    }
}
