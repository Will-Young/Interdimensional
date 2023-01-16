using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CatSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _catSprites_Blue;
    [SerializeField] private Sprite[] _catSprites_White;
    [SerializeField] private Sprite[] _catSprites_Spotty;
    [SerializeField] private Sprite[] _catSprites_Basketball;

    private Sprite[][] _chooseSprite;

    public SpriteRenderer[] catSprites;

    /*
    private enum Skin { blue, white, spotty, basketball };
    private Skin catSkin;
    */

	// Start is called before the first frame update
	void Start()
    {
        _chooseSprite = new Sprite[][] { _catSprites_Blue, _catSprites_White, _catSprites_Spotty };


		int skinRandom = Random.RandomRange(0, 2);



        for ( int i = 0; i < catSprites.Length; i++ )
        {
            catSprites[i].sprite = _chooseSprite[skinRandom][i];
            print("changeing " + catSprites[i].sprite + " to " + _chooseSprite[skinRandom][i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeSkin();
        }
    }

    void ChangeSkin()
    {
		_chooseSprite = new Sprite[][] { _catSprites_Blue, _catSprites_White, _catSprites_Spotty };


		int skinRandom = Random.Range(0, 3);



		for (int i = 0; i < catSprites.Length; i++)
		{
			catSprites[i].sprite = _chooseSprite[skinRandom][i];
			print("changeing " + catSprites[i].sprite + " to " + _chooseSprite[skinRandom][i]);
		}
	}
}
