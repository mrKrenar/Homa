using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoSingleton<CharacterAnimationController>
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void SetAnimation(CharacterAnimationType animationType)
    {
        switch (animationType)
        {
            case CharacterAnimationType.idle:
                animator.SetBool("bo_running", false);
                break;
            case CharacterAnimationType.running:
                animator.SetBool("bo_running", true);
                break;
            case CharacterAnimationType.jumpUp:
                animator.SetTrigger("tr_jumpUp");
                break;
            case CharacterAnimationType.jumpDown:
                animator.SetTrigger("tr_jumpDown");
                break;
            case CharacterAnimationType.randomDance:
                animator.SetBool("bo_running", false);
                animator.SetInteger("int_randomDance", Random.Range(0, 3));
                break;
            case CharacterAnimationType.die:
                animator.SetTrigger("tr_die");
                break;
        }
    }

    //Don't delete. This method is triggered from an event in die animation
    public void Died()
    {
        MainCharacterUI.Instance.CharacterDied();
    }
}

public enum CharacterAnimationType { idle, running, jumpUp, jumpDown, randomDance, die }
