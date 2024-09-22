using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGame : MonoBehaviour
{
    public static BoardGame instance;
    private GameObject[,] board = new GameObject[3, 3];
    private char[,] dataBoard = new char[3, 3];
    private bool isXTurn = false;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject GameButton;
    [SerializeField] private Transform GameCanvas;
    private Vector2 StartPosition = new Vector2(-100, 100);
    private Vector2 ButtonSpacing = new Vector2(100, -100);
    [SerializeField] private GameObject WinTextPrefab;
    private GameObject InstantiatedText;
    [SerializeField] private AudioSource WinningClip;
    

    void Awake()
    {
        // Ensure there's only one instance
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SetBoardGame()
    {
        for(int row=0; row<3; row++)
        {
            for(int col=0; col<3; col++)
            {
                board[row, col] = Instantiate(GameButton, GameCanvas);
                RectTransform rectTransform = board[row, col].GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(StartPosition.x + (col * ButtonSpacing.x), StartPosition.y + (row * ButtonSpacing.y));
            }
        }

        for(int row=0; row<3; row++)
        {
            for(int col=0; col<3; col++)
            {
                dataBoard[row, col] = '.';
            }
        }
    }
    
    public void changeTurn()
    {
        isXTurn = !isXTurn;
    }

    public bool getIsXTurn()
    {
        return isXTurn;
    }

    void Start()
    {
        SetBoardGame();
    }

    public bool isThereAWin()
    {
        // diagonal
        for(int row=0;row<3; row++)
        {
            if(board[row, 0].GetComponent<ButtonBehavior>().GetCharacter() !='e' && board[row,0].GetComponent<ButtonBehavior>().GetCharacter() == board[row, 1].GetComponent<ButtonBehavior>().GetCharacter() && board[row, 0].GetComponent<ButtonBehavior>().GetCharacter() == board[row, 2].GetComponent<ButtonBehavior>().GetCharacter())
            {
                return true;
            }
        }
        //horizontal
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col].GetComponent<ButtonBehavior>().GetCharacter() !='e' && board[0, col].GetComponent<ButtonBehavior>().GetCharacter() == board[1, col].GetComponent<ButtonBehavior>().GetCharacter() && board[0, col].GetComponent<ButtonBehavior>().GetCharacter() == board[2, col].GetComponent<ButtonBehavior>().GetCharacter())
            {
                return true;
            }
        }
        //sideways
        // sideways
        if (board[1, 1].GetComponent<ButtonBehavior>().GetCharacter()!= 'e' && ((board[0, 0].GetComponent<ButtonBehavior>().GetCharacter() == board[1, 1].GetComponent<ButtonBehavior>().GetCharacter() && board[1, 1].GetComponent<ButtonBehavior>().GetCharacter() == board[2, 2].GetComponent<ButtonBehavior>().GetCharacter()) || (board[0, 2].GetComponent<ButtonBehavior>().GetCharacter() == board[1, 1].GetComponent<ButtonBehavior>().GetCharacter() && board[1, 1].GetComponent<ButtonBehavior>().GetCharacter() == board[2, 0].GetComponent<ButtonBehavior>().GetCharacter())))
        {
            return true;
        }

        return false;
    }
    public void turnOffAllButtons()
    {
        for(int row=0; row<3; row++)
        {
            for(int col=0; col<3; col++)
            {
                board[row, col].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void TriggerWinning()
    {
        turnOffAllButtons();
        InstantiatedText = Instantiate(WinTextPrefab, GameCanvas);
        RectTransform rectTransform = InstantiatedText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);

        if (getIsXTurn())
        {
            InstantiatedText.GetComponent<Text>().text = "X is the winner!";
        }
        else
        {
            InstantiatedText.GetComponent<Text>().text = "O is the winner!";
        }

        WinningClip.Play();
    }
}
