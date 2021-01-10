using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Moving : MonoBehaviour
{
    private GameObject target;

    private Vector3 direction;

    private float move_scale;



    private Animator monster_animator;

    private float distance;

    // private bool get_hit = false;

    private int get_hit_times = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        monster_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");

    }


    void FixedUpdate()
    {
        transform.LookAt(target.transform);
        direction = target.transform.position - transform.position;
        move_scale = 0.05f / direction.magnitude;
        transform.Translate(direction.x * move_scale, 0, direction.z * move_scale);

        distance = direction.magnitude;
        monster_animator.SetFloat("Attack_distance", distance);

    }

    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.CompareTag("bullet"))
        {
            get_hit_times += 1;

            monster_animator.SetBool("Get_hit", true);
            monster_animator.SetInteger("Get_hit_times", get_hit_times);

            if (get_hit_times >= 5)
            {
                Destroy(gameObject, 1.95f);
            }

            Invoke("change_get_hit", 0.5f);

        }
    }


    void change_get_hit()
    {
        monster_animator.SetBool("Get_hit", false);
    }
}
