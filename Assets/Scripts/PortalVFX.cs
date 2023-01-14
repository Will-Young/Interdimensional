using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVFX : MonoBehaviour
{
	[SerializeField] private GameObject PortalBase;
	[SerializeField] private GameObject PortalEffect1;
	[SerializeField] private GameObject PortalEffect2;
	[SerializeField] private GameObject PortalEffect3;

	public float Portal1_SpinSpeed = 0.3f;
	public float Portal2_SpinSpeed = 0.4f;
	public float Portal3_SpinSpeed = 0.5f;
	[Range(0.1f, 10f)] public float spinMultiplier = 3;
	
	// Start is called before the first frame update
	void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		SpinPortal(PortalEffect1, Portal1_SpinSpeed);
		SpinPortal(PortalEffect2, Portal2_SpinSpeed);
		SpinPortal(PortalEffect3, Portal3_SpinSpeed);

		Portal1_SpinSpeed = Mathf.PingPong(Time.time / spinMultiplier, 0.3f) + 0.1f;
		Portal2_SpinSpeed = Mathf.PingPong(Time.time / spinMultiplier, 0.4f) + 0.1f;
		Portal3_SpinSpeed = Mathf.PingPong(Time.time / spinMultiplier, 0.5f) + 0.1f;

		PortalBase.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
	}

	private void SpinPortal(GameObject portal, float spinSpeed)
	{
		Transform T = portal.GetComponent<Transform>();
		float newZ = T.rotation.z + spinSpeed;
		//T.SetLocalPositionAndRotation(T.position, new Quaternion(T.rotation.x, T.rotation.y, newZ, T.rotation.w));
		T.Rotate(T.forward * spinSpeed);
	}
}
