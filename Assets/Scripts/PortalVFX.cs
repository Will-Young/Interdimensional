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
	[Range(0.1f, 100f)] public float spinMultiplier = 3;

	private Vector4 PE1Colour;
	private Vector4 PE2Colour;
	private Vector4 PE3Colour;

	[SerializeField] private float _hueShiftSpeed1 = 0.1f;
	[SerializeField] private float _hueShiftSpeed2 = 0.2f;
	[SerializeField] private float _hueShiftSpeed3 = 0.3f;
	[SerializeField, Range(0, 1)] private float _saturation = 1f;
	[SerializeField, Range(0, 1)] private float _value = 1f;

	enum colors { blue, red, green };

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

		ColourShift(PortalEffect1, _hueShiftSpeed1);
		ColourShift(PortalEffect2, _hueShiftSpeed2);
		ColourShift(PortalEffect3, _hueShiftSpeed3);
	}

	private void SpinPortal(GameObject portal, float spinSpeed)
	{
		Transform T = portal.GetComponent<Transform>();
		float newZ = T.rotation.z + spinSpeed;
		//T.SetLocalPositionAndRotation(T.position, new Quaternion(T.rotation.x, T.rotation.y, newZ, T.rotation.w));
		T.Rotate(T.forward * (spinSpeed * spinMultiplier));
	}

	private Color ShiftHueBy(Color color, float amount)
	{
		// convert from RGB to HSV
		Color.RGBToHSV(color, out float hue, out float sat, out float val);

		// shift hue by amount
		hue += amount;
		sat = _saturation;
		val = _value;

		// convert back to RGB and return the color
		return Color.HSVToRGB(hue, sat, val);
	}

	private void ColourShift(GameObject G, float hueShift)
	{
		SpriteRenderer s = G.GetComponent<SpriteRenderer>();
		float amountToShift = (hueShift / 3) * Time.deltaTime;
		Color newColor = ShiftHueBy(s.color, amountToShift);

		s.color = newColor;
	}

}
