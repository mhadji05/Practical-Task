using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

	public float speed = 6f;

	Vector3 movement;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	public float posX;
	public float posZ;

	public GeneralManager genMan;

	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		playerRigidbody = GetComponent<Rigidbody>();
	}

	float h;
	float v;
	void FixedUpdate()
	{
		//float h = Input.GetAxisRaw("Horizontal");
		//float v = Input.GetAxisRaw("Vertical");
		
		if (h == 0)
        {
			v = Input.GetAxisRaw("Vertical");
		}
		if (v == 0)
        {
			h = Input.GetAxisRaw("Horizontal");
		}

		Move(h, v);
		//Turning();

		if (transform.position.x < 0)
        {
			posX = Mathf.Round((transform.position.x) * 1f) / 1f + 0.5f;
		}
        else
        {
			posX = Mathf.Round((transform.position.x) * 1f) / 1f - 0.5f;
		}

		if (transform.position.z < 0)
		{
			posZ = Mathf.Round((transform.position.z) * 1f) / 1f + 0.5f;
		}
		else
		{
			posZ = Mathf.Round((transform.position.z) * 1f) / 1f - 0.5f;
		}

		genMan.flagPlayerPosition(posX, posZ);
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0;

			var rotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(rotation);
		}
	}

}