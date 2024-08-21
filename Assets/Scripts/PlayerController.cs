using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerController : MonoBehaviour, ISavable
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    private Animator animator;
    public LayerMask interactableLayer; // interact character
    private Vector3 _moveVector;
    private bool isMoving;
    public RulerAttack rulerAttack;
    public SpriteRenderer spriteRenderer;
    private float moveX;
    Collider2D rulerCollider;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rulerCollider = rulerAttack.GetComponent<Collider2D>();
    }

    public void OnClickInteract()
    {
        StartCoroutine(Interact());
    }

    public void HandleUpdate()
    {

        moveX = animator.GetFloat("moveX");
        Move();
        animator.SetBool("isMoving", isMoving);
    }
    public IEnumerator Interact()
    {
            var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY")).normalized;
            var interacPos = transform.position + facingDir ;
            var collider = Physics2D.OverlapCircle(interacPos, 0.2f,interactableLayer);
            if(collider != null)
            {
                yield return collider.GetComponent<Interactable>()?.Interact(transform);
             }

      
    }
    private void Move()
    {

        _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveVector.y = _joystick.Vertical * _moveSpeed * Time.deltaTime;
        _moveVector = new Vector3(_moveVector.x, _moveVector.y, 0f).normalized;
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            isMoving = true;
            transform.position += _moveVector * _moveSpeed * Time.deltaTime;
            animator.SetFloat("moveX", _moveVector.x);
            animator.SetFloat("moveY", _moveVector.y);
            if(moveX > 0) {
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            else
            {
                gameObject.BroadcastMessage("IsFacingRight", false);

            }
        }

        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            isMoving = false;
        }

    }
    public void  Attack()
    {
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
            SomeAsyncTask();
            animator.SetTrigger("rulerAttack");
        }
        else
        {
            animator.SetTrigger("rulerAttack");
        }

    }
    public object CaptureState()
    {
        float[] position = new float[] { transform.position.x, transform.position.y };
        return position;
    }

    public void RestoreState(object state)
    {
        var position = (float[]) state;
        transform.position = new Vector3(position[0], position[1]);
    }
    async Task SomeAsyncTask()
    {
        await Task.Delay(1000);
        spriteRenderer.flipX = false;
    }

}




