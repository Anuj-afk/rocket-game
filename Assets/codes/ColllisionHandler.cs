using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColllisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem CrashParticles;
    [SerializeField] float delay;
    [SerializeField] AudioClip DieClip;
    [SerializeField] AudioClip NextLevelClip;
    AudioSource audioSource;
    bool isTransitioning = false;
    int level = 0;
    movement Movement;
    private void Start()
    {
        Movement = GetComponent<movement>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Freindly":
                Debug.Log("This is freindly");
                break;
            case "Finish":
                if (isTransitioning == false)
                {
                    level = 1;
                    StartCrash();
                    audioSource.Pause();
                    audioSource.PlayOneShot(NextLevelClip);
                    SuccessParticles.Play();
                }
                break;
            default:
                if (isTransitioning == false)
                {
                    level = 0;
                    StartCrash();
                    audioSource.Pause();
                    audioSource.PlayOneShot(DieClip);
                    CrashParticles.Play();
                }
                break;
        }
    }
    private void LevelLoader()
    {
        int Currentscene = SceneManager.GetActiveScene().buildIndex;
        if (Currentscene + level == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(Currentscene + level);
        }
    }
    private void StartCrash()
    {
        isTransitioning = true;
        Invoke("LevelLoader", delay);
        Movement.enabled = false;
    }
}
