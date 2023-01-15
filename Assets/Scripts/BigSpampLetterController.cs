using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpampLetterController : MonoBehaviour
{

	public Transform pipeIn;
	public Transform pipeOut;
	public Transform underStamp;

	public float letterSizeInPipe = 0.5f;

	public GameObject letterPrefab;
	private GameObject _letterComingIn;
	private GameObject[] _letterGoingOut;

	// Start is called before the first frame update
	void Start()
    {
		_letterGoingOut = GameObject.FindGameObjectsWithTag("LetterOut");
	}

    // Update is called once per frame
    void Update()
    {
		if (_letterComingIn == null)
		{ SpawnLetter(); }

		MoveLetters();
		//DestroyLetters();
		if (Input.GetKeyDown(KeyCode.Space))
			{
			Stamp();
		}
    }

	void SpawnLetter()
	{
		_letterComingIn = Instantiate(letterPrefab, pipeIn.transform.position, Quaternion.identity);
		_letterComingIn.transform.localScale = Vector3.one * letterSizeInPipe;
	}

	void MoveLetters()
	{
		if (Vector3.Distance(_letterComingIn.transform.position, underStamp.position) > 0.1f)
		{
			_letterComingIn.transform.position = Vector3.Lerp(_letterComingIn.transform.position, underStamp.position, 0.1f);
			_letterComingIn.transform.localScale = Vector3.Lerp(_letterComingIn.transform.localScale, Vector3.one, 0.1f);
		}
		if (_letterGoingOut != null)
		{
			for (int i = 0; i < _letterGoingOut.Length; i++)
			{
				print("LettergoingOut is this length: " + _letterGoingOut.Length);
				if (Vector3.Distance(_letterGoingOut[i].transform.position, pipeOut.position) > 0.1f)
				{
					_letterGoingOut[i].transform.position = Vector3.Lerp(_letterGoingOut[i].transform.position, pipeOut.position, 0.1f);
					_letterGoingOut[i].transform.localScale = Vector3.Lerp(_letterGoingOut[i].transform.localScale, Vector3.one * letterSizeInPipe, 0.1f);
				}
				else
				{
					Destroy(_letterGoingOut[i]);
					_letterGoingOut = GameObject.FindGameObjectsWithTag("LetterOut");
				}
				
			}
		}
	}

	public void Stamp()
	{
		if (Vector3.Distance(_letterComingIn.transform.position, underStamp.position) > 0.1f)
		{
			_letterComingIn.transform.position = Vector3.Lerp(_letterComingIn.transform.position, underStamp.position, 1f);
		}
		_letterComingIn.transform.transform.GetChild(0).gameObject.SetActive(true);
		_letterComingIn.tag = "LetterOut";
		_letterGoingOut = GameObject.FindGameObjectsWithTag("LetterOut");
		_letterComingIn = null;
	}

	/*private void DestroyLetters()
	{
		foreach (GameObject letter in _letterGoingOut)
		{
			if (Vector3.Distance(letter.transform.position, pipeOut.position) < 0.1f)
			{
				Destroy(letter.gameObject);
				_letterGoingOut = GameObject.FindGameObjectsWithTag("LetterOut");
			}
		}
	}*/
}
