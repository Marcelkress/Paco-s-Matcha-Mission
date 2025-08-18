using Sirenix.OdinInspector;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour
{
    [Title("Settings")] public float runSpeed = 2f;
    public float performAttackRadius = 1f;
    public float playerDetectRadius = 5;
    public float attackDamageRadius;
    public int attackDamage;
    public float edgeDetectRayLength = 0.5f;

    [Title("References")] public GameObject attackOrigin;
    public LayerMask playerLayer;
    public LayerMask collisionLayer;

    private Animator anim;
    private BoxCollider2D box;
    private Transform player;
    private bool canMove;
    private int faceDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        EnableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InRange(playerDetectRadius))
        {
            anim.SetBool("Running", false);
            return;
        }
        
        faceDir = (int)Mathf.Sign(player.position.x - transform.position.x);
        
        if (!IsAtEdge())
        {
            MoveAndAttack();
        }
        else
        {
            anim.SetBool("Running", false);   
        }

    }

    private void MoveAndAttack()
    {
        if (canMove)
        {
            Vector3 orgScale = transform.localScale;
            orgScale.x = Mathf.Abs(orgScale.x) * -faceDir;
            transform.localScale = orgScale;

            if (InRange(performAttackRadius))
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
                // Run towards player
                Vector2 velocity;
                velocity.x = runSpeed * faceDir * Time.deltaTime;
                velocity.y = 0;

                transform.Translate(velocity);
                anim.SetBool("Running", true);
            }
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Attack", false);
        }
    }

    // Called in animation timeline
    public void DealDamage()
    {
        Collider2D coll = Physics2D.OverlapCircle(attackOrigin.transform.position, attackDamageRadius, playerLayer);

        if (coll != null)
        {
            coll.GetComponent<IHealth>().TakeDamage(attackDamage, false);
        }
    }

    private bool IsAtEdge()
    {
        Vector2 origin;
        origin.x = faceDir == -1 ? box.bounds.min.x : box.bounds.max.x;
        origin.y = box.bounds.min.y;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, edgeDetectRayLength, collisionLayer);
        
        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    private bool InRange(float radius)
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (coll != null)
        {
            player = coll.transform;
            return true;
        }

        player = null;
        return false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, performAttackRadius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackOrigin.transform.position, attackDamageRadius);
        
        Vector2 origin;
        origin.x = box.bounds.min.x;
        origin.y = box.bounds.min.y;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin, Vector3.down);
        
        origin.x = box.bounds.max.x;
        Gizmos.DrawRay(origin, Vector3.down);
    }
}
