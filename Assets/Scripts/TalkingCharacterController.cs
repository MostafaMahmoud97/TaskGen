using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingCharacterController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator CinimaCameraAnimator;
    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private GameObject CinimaCamera;
    [SerializeField] private GameObject TalkingCharacter;
    [SerializeField] private AudioSource CharacterAudioSource;
    [SerializeField] private AudioClip[] ClipList;
    [SerializeField] private Material[] changedMaterial;
    [SerializeField] private GameObject InterBoard;

    bool FirstTimeCheck = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerX"))
        {
            if (!FirstTimeCheck)
            {
                CinimaCamera.SetActive(true);
                Player.SetActive(false);
                StartCoroutine(StartAction());
                FirstTimeCheck = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerX"))
        {
            FirstTimeCheck = false;
        }
    }


    IEnumerator StartAction()
    {
        CharacterAnimator.SetBool("is_talk", true);
        yield return new WaitForSeconds(1f);
        CharacterAudioSource.clip = ClipList[0];
        CharacterAudioSource.Play();
        yield return new WaitForSeconds(2f);
        CharacterAudioSource.clip = ClipList[1];
        CharacterAudioSource.Play();
        yield return new WaitForSeconds(4f);
        CharacterAnimator.SetBool("is_talk", false);
        CharacterAudioSource.Stop();
        CharacterAudioSource.clip = ClipList[2];
        CharacterAudioSource.Play();
        yield return new WaitForSeconds(3f);
        CinimaCameraAnimator.SetBool("Move", true);
    }

    public void ExitBoard()
    {
        StartCoroutine(ExitBoardAnime());
    }

    IEnumerator ExitBoardAnime()
    {
        CinimaCameraAnimator.SetBool("Move", false);
        yield return new WaitForSeconds(1f);
        InterBoard.GetComponent<MeshRenderer>().material = changedMaterial[2];
        Player.SetActive(true);
        CinimaCamera.SetActive(false);
    }

    public void changeMaterial(int index)
    {
        InterBoard.GetComponent<MeshRenderer>().material = changedMaterial[index];
    }
}
