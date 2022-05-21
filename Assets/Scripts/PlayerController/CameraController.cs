using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController player;

    public float turnSpeed = 10.0f;

    public float height = 5f;
    public float distance = 4f;
    Vector3 offset;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        offset = new Vector3(0, height, distance);
    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;

        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }
}
