using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectablesStack : MonoBehaviour
{
    [SerializeField] Transform stackHolder;

    float lastElementYPostion;
    float distanceBetweenElements = 0.05f;

    List<GameObject> stack = new List<GameObject>();

    public void AddToStack(GameObject go)
    {
        stack.Add(go);
        go.transform.parent = stackHolder;

        Vector3 targetPosition = stackHolder.transform.localPosition + Vector3.up * lastElementYPostion;

        go.transform.DOLocalMove(stackHolder.transform.localPosition + Vector3.up * lastElementYPostion, .25f);

        lastElementYPostion += distanceBetweenElements;
    }

    public void ThrowFromStack()
    {
        if (stack.Count > 0)
        {
            var element = stack[stack.Count - 1];
            element.AddComponent<Rigidbody>().AddForce(Random.Range(-3f, 3f), Random.Range(5f, 7f), 0, ForceMode.VelocityChange);
            element.transform.parent = null;
            Destroy(element, 5f);
            stack.RemoveAt(stack.Count - 1);

            lastElementYPostion -= distanceBetweenElements;
        }
    }

    public void DropStack()
    {
        lastElementYPostion -= distanceBetweenElements * stack.Count;

        foreach (var item in stack)
        {
            item.AddComponent<BoxCollider>();
            item.AddComponent<Rigidbody>().AddForce(item.transform.forward + new Vector3 (Random.Range(-.5f, .5f), Random.Range(2f, 3f), Random.Range(4f, 5f)), ForceMode.VelocityChange);
            item.transform.parent = null;
        }

        stack.Clear();
    }
}
