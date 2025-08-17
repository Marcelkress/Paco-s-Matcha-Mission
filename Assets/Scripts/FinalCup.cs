using System;
using System.Collections;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class FinalCup : MonoBehaviour, IInteractable
{
    public GameObject matchaLeaf;
    public int maxLeafAmount = 10;
    public float timeBetweenLeafs = 0.5f;
    public string leafTag;
    public Animator anim;
    public Transform first, second;

    public void Interact(Transform cat)
    {
        StartCoroutine(BeginAnimation(cat));
        gameObject.GetComponentInChildren<InteractPrompt>().HidePrompt();
    }

    private IEnumerator BeginAnimation(Transform parent)
    {
        for (int i = 0; i < maxLeafAmount; i++)
        {
            GameObject leaf = Instantiate(matchaLeaf, parent.position, quaternion.identity);
            JumpInCup leafScript = leaf.GetComponent<JumpInCup>();
            
            leafScript.targets[0] = first;
            leafScript.targets[1] = second;
            
            yield return new WaitForSeconds(timeBetweenLeafs);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(leafTag))
        {
            anim.SetTrigger("Burst");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            
        }
    }
}
