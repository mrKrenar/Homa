using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetector : MonoBehaviour
{
    public UnityAction<int> onTriggerEnter;
    public bool ignoreTrigger;

    [SerializeField] string triggerTag = "Player";
    [SerializeField] int optionId;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger option: " + other.gameObject.name, other.gameObject);

        if (ignoreTrigger)
        {
            return;
        }

        if (string.IsNullOrEmpty(triggerTag) || other.CompareTag(triggerTag))
        {
            onTriggerEnter?.Invoke(optionId);
        }
    }
}
