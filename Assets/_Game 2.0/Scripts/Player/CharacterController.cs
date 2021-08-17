using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float groundedRaycastDistance = 0.5f;
    [SerializeField] float groundOffset = 0.2f;
    [SerializeField] float speed = 5f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashDistance = 4f;
    [SerializeField] float timeBetweenDash = 1f;
    [SerializeField] GameObject minionArt = default;
    [SerializeField] LayerMask groundRaycastMask = default;

    public bool IsGrounded { get; private set; }
    public bool IsOnDash { get; private set; }


    private Rigidbody rb;
    private Vector3 movementInput;
    private Vector3 groundPosition;
    private Vector3 gravity;
    private Vector3 currentVelocity;
    private Vector3 movementVelocity;

    private Vector3 dashVelocity;
    private Vector3 dashDirection;
    private float currentDashTime;
    private float dashSpeed;
    private float currentTimeBetweenDash;

    private bool canDash = true;

    private Stunable stunStatus;

    [SerializeField] ParticleSystem dashefect; //Particulas
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stunStatus = GetComponent<Stunable>();
    }

    private void Update()
    {
        if (stunStatus.IsStunned)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(h, 0f, v);
        movementVelocity = movementInput.normalized * speed;

        if (Input.GetMouseButtonDown(1) && !IsOnDash && canDash)
        {
            if (movementInput.x != 0 || movementInput.z != 0)
            {
                dashSpeed = dashDistance / dashTime * 2f;
                Dash(movementInput.normalized);
            }
        }
    }

    private void FixedUpdate()
    {
        Turning();
        CheckGround();
        CalculateGravityVelocity();

        DashDesaccelerate();

        ApplyVelocities();

        TimerForDash();
    }

    private void TimerForDash()
    {
        if (!IsOnDash && !canDash)
        {
            currentTimeBetweenDash -= Time.fixedDeltaTime;

            if (currentTimeBetweenDash <= 0)
            {
                canDash = true;
            }
        }
    }

    private void CheckGround()
    {
        RaycastHit hit;

        Debug.DrawRay(rb.position, -transform.up * groundedRaycastDistance, Color.red);
        if (Physics.Raycast(rb.position, -transform.up, out hit, groundedRaycastDistance))
        {
            groundPosition = hit.point;

            bool willGround = false;

            if (rb.position.y - groundPosition.y < groundOffset)
            {
                willGround = true;
            }

            if (!IsGrounded && willGround)
            {
                gravity.y = 0;
            }

            IsGrounded = willGround;
        }
        else
        {
            IsGrounded = false;
        }

    }

    private void Dash(Vector3 newDirection)
    {
        if (IsGrounded)
        {
            dashDirection = newDirection;
            currentDashTime = dashTime;
            canDash = false;
            currentTimeBetweenDash = timeBetweenDash;
            dashefect.Play(dashefect);                  //Play particulas
        }
    }

    private void CalculateGravityVelocity()
    {
        if (!IsGrounded)
        {
            gravity.y += Physics.gravity.y * Time.fixedDeltaTime;
        }

    }

    private void ApplyVelocities()
    {
        if (IsOnDash)
        {
            currentVelocity = gravity + dashVelocity;
        }
        else
        {
            currentVelocity = movementVelocity + gravity;
        }

        rb.velocity = currentVelocity;

    }

    private void DashDesaccelerate()
    {
        //currentDashTime -= Time.fixedDeltaTime;

        if (currentDashTime > 0f)
        {
            IsOnDash = true;
            currentDashTime -= Time.fixedDeltaTime;
            dashVelocity = (dashDirection * dashSpeed) * (currentDashTime / dashTime);
        }
        else
        {
            dashVelocity = Vector3.zero;
            IsOnDash = false;
        }
    }

    private void Turning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        origin = ray.origin;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundRaycastMask))
        {
            hitPoint = hit.point;
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            Vector3 lookPosition = hit.point;
            lookPosition.y = minionArt.transform.position.y;
            minionArt.transform.LookAt(lookPosition);
        }
    }

    Vector3 origin;
    Vector3 hitPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(origin, hitPoint);

        Gizmos.DrawSphere(hitPoint, 0.2f);
    }
}
