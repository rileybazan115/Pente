using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	public Board board;
	[SerializeField] Sprite whitePeace = null;
	[SerializeField] Sprite blackPeace = null;

	int turnCount = 0;
	int p1CapturedPieces = 0;
	int p2CapturedPieces = 0;

	List<List<int>> bord = new List<List<int>>()
	{
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
	};

	// Start is called before the first frame update
	void Start()
	{
		board.Create();

		int piecesCapturedThisTurn = CaptureChecker(bord);
		if (piecesCapturedThisTurn > 0)
		{
			if ((turnCount % 2) == 0)
			{
				p1CapturedPieces += piecesCapturedThisTurn;
			}
			else
			{
				p2CapturedPieces += piecesCapturedThisTurn;
			}
		}
		Debug.Log(p1CapturedPieces.ToString());
	}

	public void OnCellClicked(Cell cell)
	{
		Image image = cell.transform.GetChild(0).GetComponent<Image>();
		image.color = Color.white;
		image.sprite = turnCount % 2 == 0 ? whitePeace : blackPeace;

		bord[cell.x][cell.y] = (int)(turnCount % 2 == 0 ? Piece.ePieceType.White : Piece.ePieceType.Black);
		CaptureChecker(bord);
		TesseraChecker(bord);
		TriaChecker(bord);
		if (WinChecker(bord))
			Debug.Log("Player" + (turnCount%2) + " wins");

		turnCount++;
	}

	public bool WinChecker(List<List<int>> board)
	{
		int activePlayer = (turnCount % 2) + 1;

		//horizontal check
		for (int i = 0; i < 19; i++)
		{
			for (int j = 0; j < 15; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i][j + 1] && activePlayer == board[i][j + 2] && activePlayer == board[i][j + 3] && activePlayer == board[i][j + 4]) return true;
			}
		}

		//vertical check
		for (int i = 0; i < 15; i++)
		{
			for (int j = 0; j < 19; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i + 1][j] && activePlayer == board[i + 2][j] && activePlayer == board[i + 3][j] && activePlayer == board[i + 4][j]) return true;
			}
		}

		//bottom left to upper right diag check
		for (int i = 4; i < 19; i++)
		{
			for (int j = 0; j < 15; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i - 1][j + 1] && activePlayer == board[i - 2][j + 2] && activePlayer == board[i - 3][j + 3] && activePlayer == board[i - 4][j + 4]) return true;
			}
		}

		//upper left to bottom right diag check
		for (int i = 0; i < 15; i++)
		{
			for (int j = 0; j < 15; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i + 1][j + 1] && activePlayer == board[i + 2][j + 2] && activePlayer == board[i + 3][j + 3] && activePlayer == board[i + 4][j + 4]) return true;
			}
		}

		if (p1CapturedPieces >= 10 || p2CapturedPieces >= 10) return true;


		return false;
	}

	public int CaptureChecker(List<List<int>> board)
	{
		int activePlayer = (turnCount % 2) + 1;
		int inActivePlayer = ((turnCount + 1) % 2) + 1;

		int capturedPieces = 0;

		//horizontal check
		for (int i = 0; i < 19; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if (activePlayer == board[i][j] && inActivePlayer == board[i][j + 1] && inActivePlayer == board[i][j + 2] && activePlayer == board[i][j + 3]) capturedPieces += 2;
			}
		}

		//vertical check
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 19; j++)
			{
				if (activePlayer == board[i][j] && inActivePlayer == board[i + 1][j] && inActivePlayer == board[i + 2][j] && activePlayer == board[i + 3][j]) capturedPieces += 2;
			}
		}

		//bottom left to upper right diag check
		for (int i = 4; i < 19; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if (activePlayer == board[i][j] && inActivePlayer == board[i - 1][j + 1] && inActivePlayer == board[i - 2][j + 2] && activePlayer == board[i - 3][j + 3]) capturedPieces += 2;
			}
		}

		//upper left to bottom right diag check
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if (activePlayer == board[i][j] && inActivePlayer == board[i + 1][j + 1] && inActivePlayer == board[i + 2][j + 2] && activePlayer == board[i + 3][j + 3]) capturedPieces += 2;
			}
		}

		return capturedPieces;
	}

	public bool TesseraChecker(List<List<int>> board)
	{
		int activePlayer = (turnCount % 2) + 1;

		//horizontal check
		for (int i = 0; i < 19; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i][j + 1] && activePlayer == board[i][j + 2] && activePlayer == board[i][j + 3]) return true;
			}
		}

		//vertical check
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 19; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i + 1][j] && activePlayer == board[i + 2][j] && activePlayer == board[i + 3][j]) return true;
			}
		}

		//bottom left to upper right diag check
		for (int i = 3; i < 19; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i - 1][j + 1] && activePlayer == board[i - 2][j + 2] && activePlayer == board[i - 3][j + 3]) return true;
			}
		}

		//upper left to bottom right diag check
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i + 1][j + 1] && activePlayer == board[i + 2][j + 2] && activePlayer == board[i + 3][j + 3]) return true;
			}
		}

		if (p1CapturedPieces >= 10 || p2CapturedPieces >= 10) return true;


		return false;
	}

	public bool TriaChecker(List<List<int>> board)
	{
		int activePlayer = (turnCount % 2) + 1;

		//horizontal check
		for (int i = 0; i < 19; i++)
		{
			for (int j = 0; j < 17; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i][j + 1] && activePlayer == board[i][j + 2]) return true;
			}
		}

		//vertical check
		for (int i = 0; i < 17; i++)
		{
			for (int j = 0; j < 19; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i + 1][j] && activePlayer == board[i + 2][j]) return true;
			}
		}

		//bottom left to upper right diag check
		for (int i = 2; i < 19; i++)
		{
			for (int j = 0; j < 17; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i - 1][j + 1] && activePlayer == board[i - 2][j + 2]) return true;
			}
		}

		//upper left to bottom right diag check
		for (int i = 0; i < 17; i++)
		{
			for (int j = 0; j < 17; j++)
			{
				if (activePlayer == board[i][j] && activePlayer == board[i + 1][j + 1] && activePlayer == board[i + 2][j + 2]) return true;
			}
		}

		if (p1CapturedPieces >= 10 || p2CapturedPieces >= 10) return true;


		return false;
	}
}
