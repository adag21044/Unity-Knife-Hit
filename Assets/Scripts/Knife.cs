using UnityEngine;
using UnityEngine.UIElements;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;
    private bool isActive = true;
    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isActive)
        {
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameController.Instance.gameUI.DecrementDisplayedKnifeCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isActive) return;

        isActive = false;

        if(other.collider.CompareTag("Log"))
        {
            GetComponent<ParticleSystem>().Play();
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(other.transform);
            
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);

            // Accessing GameController singleton directly
            GameController.Instance.OnSuccessfulKnifeHit();
        }
        else
        if(other.collider.CompareTag("Knife"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -2);
            GameController.Instance.StartGameOverSequence(false);
        }
    }
}
