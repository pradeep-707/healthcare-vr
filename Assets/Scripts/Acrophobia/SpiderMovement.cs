using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SpiderMovement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private NavMeshAgent spider;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip[] walkingSounds;
    [SerializeField] private AudioClip[] hurtSounds;
    private AudioSource audioSource;
    private Rigidbody rigidBody;
    private const float forceMagnitude = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(spiderSounds());
    }

    // Update is called once per frame
    void Update()
    {
        spider.SetDestination(player.transform.position);
    }

    IEnumerator spiderSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            playWalkingSound();
        }
    }

    void playWalkingSound()
    {
        audioSource.clip = walkingSounds[Random.Range(0, walkingSounds.Length)];
        audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject camera = GameObject.Find("Main Camera");
        Vector3 distance = (transform.position - camera.transform.position);
        Vector3 direction = distance;
        direction.y = 0;
        direction.Normalize();
        rigidBody.AddForce(direction * forceMagnitude, ForceMode.Impulse);
        playHurtSound();
    }

    void playHurtSound()
    {
        audioSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
        audioSource.Play();
    }
}
