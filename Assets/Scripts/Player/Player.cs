using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float speed = 3f;

    bool jumped = false;
    bool gameStarted = false;

    void Awake()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (GameManager.Instance.State != GameState.Running)
            return;

        if (!gameStarted && Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            rb.isKinematic = false;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }


        if (!jumped && Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            jumped = true;
        }
    }

    void FixedUpdate()
    {
        if (jumped)
        {
            jumped = false;
            rb.velocity = new Vector2(speed, jumpForce);
        }
    }

    void LateUpdate()
    {
        cam.transform.position = new Vector3(transform.position.x + 1f, cam.transform.position.y, cam.transform.position.z);
    }
}
