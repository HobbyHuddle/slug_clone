using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Shared;

namespace Items
{
    [Serializable]
    public class CollectItemEvent : UnityEvent<Item> {}
    
    public class CollectibleItem : MonoBehaviour
    {
        public Item item;
        public CollectItemEvent onItemPickup;

        private SpriteRenderer spriteRenderer;
        private AudioSource sfx;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.image;
            sfx = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                onItemPickup.Invoke(item);
                
                StartCoroutine(Consume());
            }
        }

        IEnumerator Consume()
        {
            if (sfx.clip) sfx.Play();
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }
        
        public void PlaySfx()
        {
            if (sfx.clip) sfx.Play();
        }
    }
}