using UnityEngine;

public class Soldier : MonoBehaviour
{
    public int speed = 10;
    public float turn = 20.5f;

    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    public Rigidbody rigPick;


    private void Update()
    {
        Turn();
        Run();
        Attack();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "寶箱"&& ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigPick;
        }

        if (other.name == "區域" && ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊"))
        {
            GameObject.Find("寶箱").GetComponent<HingeJoint>().connectedBody = null;
        }
    }

    private void Run()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊")) return;

        float v = Input.GetAxis("Vertical"); // W S
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);

        ani.SetBool("走路開關", v != 0);

    }

    private void Turn()
    {
        float h = Input.GetAxis("Horizontal"); // A D
        tran.Rotate(0, turn * h * Time.deltaTime , 0);

    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("攻擊觸發器");
        }
    }
}
