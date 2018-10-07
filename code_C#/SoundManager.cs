using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager S;

    public GameObject Collect;
    private AudioSource CollectSound;

    public GameObject Death;
    private AudioSource DeathSound;

    public GameObject FirstUnlock;
    private AudioSource FirstUnlockSound;

    public GameObject GameMusic;
    private AudioSource GameMusicSound;

    public GameObject Hissing;
    private AudioSource HissingSound;

    public GameObject Jump;
    private AudioSource JumpSound;

    public GameObject SecondUnlock;
    private AudioSource SecondUnlockSound;

    public GameObject Shoot;
    private AudioSource ShootSound;

    public GameObject Transform;
    private AudioSource TransformSound;

    // Use this for initialization
    void Start () {
		S = this;

        CollectSound = Collect.GetComponent<AudioSource>();
        DeathSound = Death.GetComponent<AudioSource>();
        FirstUnlockSound = FirstUnlock.GetComponent<AudioSource>();
        GameMusicSound = GameMusic.GetComponent<AudioSource> ();
        HissingSound = Hissing.GetComponent<AudioSource>();
        JumpSound = Jump.GetComponent<AudioSource>();
        SecondUnlockSound = SecondUnlock.GetComponent<AudioSource>();
        ShootSound = Shoot.GetComponent<AudioSource>();
        TransformSound = Transform.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    //void Update () {

    //}

    public void PlayCollectSound()
    {
        CollectSound.Play();
    }

    public void PlayDeathSound()
    {
        DeathSound.Play();
    }

    public void PlayFirstUnlockSound()
    {
        FirstUnlockSound.Play();
    }

    public void PlayGameMusic()
    {
        GameMusicSound.Play();
    }

    public void StopGameMusic()
    {
        GameMusicSound.Stop();
    }

    public void PlayHissingSound()
    {
        HissingSound.Play();
    }

    public void PlayJumpSound()
    {
        JumpSound.Play();
    }

    public void PlaySecondUnlockSound()
    {
        SecondUnlockSound.Play();
    }

    public void PlayShootSound() {
        ShootSound.Play();
    }

    public void PlayTransformSound()
    {
        TransformSound.Play();
    }

}
