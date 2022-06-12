using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DeathReason { sadness, hunger }

public class MainCharacterController : MonoSingleton<MainCharacterController>
{
    [SerializeField] float collectMoneyDistance = 1;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float groundWidth = 4.5f;

    public CollectablesStack collectablesStack;

    Vector3 updatedPosition;
    float lastXPos, currentXPos, deltaX;

    public bool GameStarted { get; set; }

    bool died, won;

    private void Awake()
    {
        if (collectablesStack == null)
        {
            collectablesStack = GetComponent<CollectablesStack>();
        }
    }

    void Start()
    {
        updatedPosition = transform.position;

        StartCoroutine(CheckCollectables());

        IEnumerator CheckCollectables()
        {
            var checkDelay = new WaitForSeconds(.1f);
            Collider[] colliders;

            while (!died && !won)
            {
                colliders = Physics.OverlapSphere(transform.position, collectMoneyDistance);

                foreach (var item in colliders)
                {
                    if (item.CompareTag("Money"))
                    {
                        collectablesStack.AddToStack(item.gameObject);

                        Destroy(item);
                    }
                    else if (item.CompareTag("Finish"))
                    {
                        Won();
                    }
                }
                yield return checkDelay;
            }
        }
    }

    void Won()
    {
        won = true;

        CameraController.Instance.ignoreFollowing = true;

        transform.DOMove(new Vector3(0, 0, transform.position.z + 3), 1)
            .OnComplete(() =>
            {
                transform.DORotate(Vector3.up * 180, .5f)
                .OnComplete(() =>
                {
                    collectablesStack.DropStack();

                    CharacterAnimationController.Instance.SetAnimation(CharacterAnimationType.randomDance);

                    UiManager.Instance.SetWonReason();

                    StartCoroutine(EnableWonCanvasAfter(2));
                });
            });

        IEnumerator EnableWonCanvasAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            UiManager.Instance.EnableCanvas(2);
        }
    }

    public void Die(DeathReason reason)
    {
        died = true;

        CameraController.Instance.PlayerDied();

        UiManager.Instance.SetDeathReason(reason);

        collectablesStack.DropStack();

        transform.DOMove(new Vector3(0, 0, transform.position.z + 5), 1f).OnComplete(
            () =>
            {
                CharacterAnimationController.Instance.SetAnimation(CharacterAnimationType.die);
            });
    }

    private void Update()
    {
        if (died || won)
        {
            return;
        }

        //when stop touching, make sure: deltaX = 0 and character stops moving on X
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lastXPos = currentXPos = 0;
        }

        //prevent character from "jumping" on first click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastXPos = currentXPos = TouchPositionFromScreenCenter01();
        }

        //calculate deltaX
        if (Input.GetKey(KeyCode.Mouse0))
        {
            lastXPos = currentXPos;
            currentXPos = TouchPositionFromScreenCenter01();
            deltaX = (currentXPos - lastXPos) / Screen.width;

            deltaX *= groundWidth;

            //cache movement X calculations
            updatedPosition.x += deltaX;

            //clamp x position to allowed values
            if (updatedPosition.x > (groundWidth - 2) / 2)
            {
                updatedPosition.x = (groundWidth - 2) / 2;
            }
            else if (updatedPosition.x < (-groundWidth + 2) / 2)
            {
                updatedPosition.x = (-groundWidth + 2) / 2;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameStarted || died || won)
        {
            return;
        }

        //apply movement calculations
        updatedPosition.z += Time.fixedDeltaTime * moveSpeed;
        transform.position = updatedPosition;
    }

    float TouchPositionFromScreenCenter01()
    {
        return Input.mousePosition.x - (Screen.width / 2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collectMoneyDistance);
    }
}
