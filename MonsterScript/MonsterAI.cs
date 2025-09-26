using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRange = 15f;   // How close player needs to be
    public float attackRange = 2f;       // Distance to trigger attack
    public float attackCooldown = 1.5f;  // Delay between attacks

    private Transform player;            // Player target
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Automatically find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("MonsterAI: No GameObject with 'Player' tag found in scene!");
        }
    }

    void Update()
    {
        if (player == null) return; // Do nothing if no player found

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Stop moving
            agent.isStopped = true;
            animator.SetBool("isRunning", false);

            // Attack if cooldown is ready
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
        else if (distance <= detectionRange)
        {
            // Chase player
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isRunning", true);
        }
        else
        {
            // Idle
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
        }
    }
}
