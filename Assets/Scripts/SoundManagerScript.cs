using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip skeletonAttack, volstaggAttack, skeletonDeath, volstaggDeath, volstaggHit, pickUpKey;
    static AudioSource audioSrc;
    // Use this for initialization
    void Start()
    {
        skeletonAttack = Resources.Load<AudioClip>("sword-slash-attack");
        volstaggAttack = Resources.Load<AudioClip>("volAttack-slash");
        skeletonDeath = Resources.Load<AudioClip>("skeleton-bones");
        volstaggDeath = Resources.Load<AudioClip>("volstagg-death");
        volstaggHit = Resources.Load<AudioClip>("volstagg-grunt");
        pickUpKey = Resources.Load<AudioClip>("pickupkey"); 

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "sword-slash-attack":
                audioSrc.PlayOneShot(skeletonAttack);
                break;
            case "volAttack-slash":
                audioSrc.PlayOneShot(volstaggAttack);
                break;
            case "skeleton-bones":
                audioSrc.PlayOneShot(skeletonDeath);
                break;
            case "volstagg-death":
                audioSrc.PlayOneShot(volstaggDeath);
                break;
            case "volstagg-grunt":
                audioSrc.PlayDelayed(0.8f);
                audioSrc.PlayOneShot(volstaggHit);
                break;
            case "pickupkey":
                audioSrc.PlayOneShot(pickUpKey);
                break;
        }
    }
}