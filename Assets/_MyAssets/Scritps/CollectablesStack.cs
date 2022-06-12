using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesStack : MonoBehaviour
{
    [SerializeField] Transform stackHolder;

    float lastElementYPostion;
    float distanceBetweenElements = 0.03f;

    List<GameObject> stack = new List<GameObject>();

    public void AddToStack(GameObject go)
    {
        stack.Add(go);
        go.transform.parent = stackHolder;
        go.transform.position = stackHolder.transform.position + Vector3.up * lastElementYPostion;

        lastElementYPostion += distanceBetweenElements;
    }

    public void ThrowFromStack()
    {
        if (stack.Count > 0)
        {
            var element = stack[stack.Count - 1];
            element.AddComponent<Rigidbody>().AddForce(Random.Range(-3f, 3f), Random.Range(5f, 7f), 0, ForceMode.VelocityChange);
            Destroy(element, 5f);
            stack.RemoveAt(stack.Count - 1);
        }
    }

    public void DropStack()
    {
        foreach (var item in stack)
        {
            item.AddComponent<BoxCollider>();
            item.AddComponent<Rigidbody>().AddForce(Random.Range(-.5f, .5f), Random.Range(2f, 3f), Random.Range(4f, 5f), ForceMode.VelocityChange);
        }
    }
}
