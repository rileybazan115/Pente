using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
	public Image outlineImage;

	[HideInInspector]
	public Vector2Int boardPosition = Vector2Int.zero;
	[HideInInspector]
	public Board board = null;
	[HideInInspector]
	public RectTransform rectTransform = null;

	[HideInInspector]
	public int x, y;

	public void SetUp(Vector2Int newBoardPosition, Board newBoard)
	{
		boardPosition = newBoardPosition;
		board = newBoard;

		rectTransform = GetComponent<RectTransform>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		GameObject go = GameObject.Find("GameManager");
		go.GetComponent<GameManager>().OnCellClicked(this);
	}
}
