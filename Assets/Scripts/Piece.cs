using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    [SerializeField] public GameObject White;
    [SerializeField] public GameObject Black;

    //public Color color = Color.clear;
    public enum ePieceType
    {
        White,
        Black,
        Empty
    }

    public ePieceType pieceType { get; set; } = ePieceType.Empty;
}
