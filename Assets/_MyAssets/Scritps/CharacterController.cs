using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoSingleton<CharacterController>
{
    [SerializeField] float collectMoneyDistance = 1;

    void Start()
    {
        StartCoroutine(CheckCollectables());
    }

    IEnumerator CheckCollectables()
    {
        var checkDelay = new WaitForSeconds(.1f);
        Collider[] colliders;

        while (true)
        {
            colliders = Physics.OverlapSphere(transform.position, collectMoneyDistance);

            foreach (var item in colliders)
            {
                if (item.CompareTag("Money"))
                {
                    Debug.Log("money found", item.gameObject);
                }
            }
            yield return checkDelay;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collectMoneyDistance);
    }
}
