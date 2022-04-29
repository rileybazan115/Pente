using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Board board;
	[SerializeField] Sprite whitePeace;
	[SerializeField] Sprite blackPeace;

	// Start is called before the first frame update
	void Start()
	{
		board.Create();
	}

	int turnCount = 0;

	public void OnCellClicked(Cell cell)
	{
		Image image = cell.transform.GetChild(0).GetComponent<Image>();
		image.color = Color.white;
		if (turnCount % 2 == 0)
			image.sprite = whitePeace;
		else
			image.sprite = blackPeace;

		turnCount++;

		Debug.Log(cell);
	}
}
