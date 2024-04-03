using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private BoolPublisherSO onObjectSelected;
    [SerializeField] private VoidPublisherSO onObjectInteracted;
    [SerializeField] private Sprite interactedSprite;
    [SerializeField] private string interactSoundName;

    private Collider2D col;
    private bool isInteractable;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            onObjectInteracted.RaiseEvent();
            AudioManager.Instance.PlaySound(interactSoundName);
            col.enabled = false;
            spriteRenderer.sprite = interactedSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onObjectSelected.RaiseEvent(true);
            isInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onObjectSelected.RaiseEvent(false);
            isInteractable = false;
        }
    }
}
