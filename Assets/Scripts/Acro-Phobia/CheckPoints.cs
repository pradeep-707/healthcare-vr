using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class CheckPoints : MonoBehaviour, IPointerClickHandler
{
    public GameObject player;
    Transform playerBody;
    [SerializeField] private Transform pos;

    void Start()
    {
        playerBody = player.GetComponent<Transform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("inside OnPointerClick");
        player.GetComponent<NavMeshAgent>().enabled = false;
        playerBody.transform.position = pos.position;
        player.GetComponent<NavMeshAgent>().enabled = true;
    }
}
