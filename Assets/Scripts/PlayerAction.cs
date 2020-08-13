using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;
    public float speed = 125f;
    float dash;

    public bool canDash = false;
    private bool attacking = false;

    public LayerMask layerMask;
    private Rigidbody2D rb;
    private Animator anim;

    private float curTime;
    private float coolTime = 1f;
    public Transform pos;
    public Vector2 boxpos;


    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                canDash = true;
            }
        }

        if (attacking == false)
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                attacking = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxpos, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag != "Player")
                    {
                        Debug.Log(collider.tag);
                    }

                    if (collider.tag == "Animal")
                    {
                        collider.GetComponent<Animal>().TakeDamage(PlayerStat.instance.player_atk);
                    }
                    else if (collider.tag == "Plant")
                    {
                        collider.GetComponent<Plant>().TakeDamage(PlayerStat.instance.player_atk);
                    }
                    else if (collider.tag == "Rocks")
                    {
                        collider.GetComponent<Rocks>().TakeDamage(PlayerStat.instance.player_atk);
                    }
                }
                anim.SetTrigger("atk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
            if ((curTime <= 0.3f) && (curTime > 0) && (attacking == true))
            {
                rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                attacking = false;
            }
        }

        //animation
        if ((Input.GetAxisRaw("Horizontal") == 0) && (Input.GetAxisRaw("Vertical") == 0))
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxpos);
    }

    private void FixedUpdate()
    {
        if (canDash == true)
        {
            dash = speed * 1.5f;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime, Input.GetAxisRaw("Vertical") * Time.deltaTime) * dash;

            canDash = false;
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime, Input.GetAxisRaw("Vertical") * Time.deltaTime) * speed;
        }
    }
}
