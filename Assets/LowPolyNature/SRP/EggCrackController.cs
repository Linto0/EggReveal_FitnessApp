using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCrackController : MonoBehaviour
{
    public GameObject[] eggStages;         // Egg cracking stages (0 = initial, last = final)
    public GameObject character;           // Character to reveal
    public AudioSource audioSource;        // Audio source for SFX
    public AudioClip crackSound;           // Egg crack sound
    public AudioClip revealSound;          // Final reveal sound

    private int tapCount = 0;
    private bool hasRevealed = false;
    private bool isProcessing = false;

    void Start()
    {
        // Activate only the first egg stage, disable the rest
        for (int i = 0; i < eggStages.Length; i++)
            eggStages[i].SetActive(i == 0);

        if (character != null)
            character.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isProcessing && !hasRevealed)
        {
            HandleTap();
        }
    }

    void HandleTap()
    {
        isProcessing = true;

        // Play crack sound
        if (crackSound != null)
            audioSource.PlayOneShot(crackSound);

        // Show next egg stage if any
        if (tapCount + 1 < eggStages.Length)
        {
            eggStages[tapCount].SetActive(false);
            eggStages[tapCount + 1].SetActive(true);
            tapCount++;
            StartCoroutine(DelayNextTap(0.3f));
        }
        else
        {
            // Last tap: reveal character with sound
            StartCoroutine(RevealSequence());
        }
    }

    IEnumerator RevealSequence()
    {
        yield return new WaitForSeconds(0.3f);

        if (!hasRevealed)
        {
            hasRevealed = true;

            foreach (var egg in eggStages)
                egg.SetActive(false);

            if (character != null)
                character.SetActive(true);

            if (revealSound != null)
                audioSource.PlayOneShot(revealSound);
        }

        isProcessing = false;
    }

    IEnumerator DelayNextTap(float delay)
    {
        yield return new WaitForSeconds(delay);
        isProcessing = false;
    }
}

