using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    int click_quantity;
    bool canClick;

    [Header("Referencias")]
    [SerializeField] Animator animator;
    [SerializeField] Transform spawnClawsPoint;
    [SerializeField] GameObject claws;
    [SerializeField] GameObject fireClaws;
    [SerializeField] Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Player>();
        click_quantity = 0;
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (p.attackType == Player.AttackType.Melee || p.attackType == Player.AttackType.Fire)){StartCombo();}
    }

    void StartCombo(){
        if(canClick){
            click_quantity++;
        }

        if(click_quantity == 1){
            animator.SetInteger("Attack",1);
        }
    }

    public void VerifyCombo(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && click_quantity == 1){
            animator.SetInteger("Attack",0);
            canClick = true;
            click_quantity = 0;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && click_quantity >= 2){
            animator.SetInteger("Attack",2);
            canClick = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && click_quantity == 2){
            animator.SetInteger("Attack",0);
            canClick = true;
            click_quantity = 0;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && click_quantity >= 3){
            animator.SetInteger("Attack",3);
            canClick = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")){
            animator.SetInteger("Attack",0);
            canClick = true;
            click_quantity = 0;
        }
    }

    public void SpawnClaws(float deg){
        // Instantiate(claws, spawnClawsPoint.position, Quaternion.Euler(spawnClawsPoint.rotation.x, transform.rotation.y * Mathf.Rad2Deg, deg));
        float yAngle = transform.rotation.eulerAngles.y;
        if(p.attackType == Player.AttackType.Melee){
            Instantiate(claws, spawnClawsPoint.position, Quaternion.Euler(spawnClawsPoint.rotation.x, yAngle, deg));
        }
        else{
            Instantiate(fireClaws, spawnClawsPoint.position, Quaternion.Euler(spawnClawsPoint.rotation.x, yAngle, deg));
        }
        

    }
}
