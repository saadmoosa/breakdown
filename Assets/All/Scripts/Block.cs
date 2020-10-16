using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockVFX;
    [SerializeReference] Sprite[] hitSprites;

    //cached reference
    Level level;
    GameSession gameStatus;

    //state variables
    [SerializeReference] int timesHit; //serialized for debugging purposes

    public void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    {
        //instantiate cached references
        level = FindObjectOfType<Level>();


        //only count blocks that can be broken
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }

    //if another object collides with breakable block, destroy it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;

        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite(timesHit);
        }
    }

    private void ShowNextHitSprite(int timesHit)
    {
        gameStatus.addPoints();
        if(hitSprites[timesHit -1] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
        }
        else
        {
            Debug.LogError("Block sprite missing from array : " + gameObject.ToString());
        }
    }

    //play sound and vfx, add game points and remove block from counter when destroying block
    private void DestroyBlock()
    {
        PlayBreakSounds();
        TriggerVFX();
        Destroy(gameObject, 0.1f);
        level.BlockBroken();
        gameStatus.addPoints();
    }

    private void PlayBreakSounds()
    {
        //Vector3 brickPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 1);
    }

    private void TriggerVFX()
    {
        GameObject sparkles = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
