using System.Collections;
using UnityEngine;

public class Disk : MonoBehaviour
{

	Rigidbody2D rb;
	[SerializeField] Rigidbody2D hookPlace;
	[SerializeField] private float _releaseTime = .15f;
	public float ReleaseTime => _releaseTime;
	[SerializeField] private float _maxDragDistance = 2f;
	public float MaxDragDistance => _maxDragDistance;
	[SerializeField] GameObject nextDisk;
	private bool isPressed = false, 
				isTouched = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate()
	{
		if (isPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Vector3.Distance(mousePos, hookPlace.position) > _maxDragDistance)
			{
				rb.position = getPosition(mousePos, hookPlace.position, _maxDragDistance);
			}
			else
			{
				rb.position = mousePos;
			}
		}
		
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
			
			if (isTouched)
			{
				if (Vector3.Distance(touchPos, hookPlace.position) > _maxDragDistance)
				{
					rb.position = getPosition(touchPos,hookPlace.position,_maxDragDistance);
					
				}
				else
				{
					rb.position = touchPos;
				}
			}
	
			if (isPhaseBegan(touch))
			{
				
				if(hit.collider != null && hit.collider.tag.Equals("Disk"))
				{
					isTouched = true;
					rb.isKinematic = true;
				}
			}

			else if (isPhaseEnded(touch)) 
			{
				if (hit.collider != null && hit.collider.tag.Equals("Disk"))
				{
					isTouched = false;
					rb.isKinematic = false;
					StartCoroutine(Release());
				}
			}

		}
	}

	private Vector2 getPosition(Vector2 actionPosition, Vector2 hookPlacePosition, float maxDragDistance)
	{
		return hookPlace.position + (actionPosition - hookPlacePosition).normalized * maxDragDistance;
	}
	private bool isPhaseEnded(Touch touch)
	{
		return touch.phase == TouchPhase.Ended;
	}
	private bool isPhaseBegan(Touch touch)
	{
		return touch.phase == TouchPhase.Began;
	}

#if UNITY_STANDALONE || UNITY_EDITOR
	void OnMouseDown()
	{
		isPressed = true;
		rb.isKinematic = true;
	}
	void OnMouseUp()
	{
		isPressed = false;
		rb.isKinematic = false;
		StartCoroutine(Release());
	}
#endif

	IEnumerator Release()
	{
		yield return new WaitForSeconds(_releaseTime);

		GetComponent<SpringJoint2D>().enabled = false;
		this.enabled = false;

		yield return new WaitForSeconds(2f);

		if (nextDisk != null)
		{
			nextDisk.SetActive(true);
		}

	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag.Equals("DeathLinePlayer"))
		{
			if (nextDisk != null)
			{
				nextDisk.SetActive(true);
			}
			gameObject.SetActive(false);

		}

	}

}
