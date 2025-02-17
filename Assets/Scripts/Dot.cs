﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;



    private FindMatches findMatches;
    private Board board;
    private GameObject otherDot;
    private Vector2 firstPosition;
    private Vector2 finalPosition;
    private Vector2 tempPosition;



    public float swipeAngle = 0;
    public float swipeResist = 1f;

    public bool isColumnBomb;
    public bool isRowBomb;
    public GameObject rowArrow;
    public GameObject columnArrow;


    // Start is called before the first frame update
    void Start()
    {
        isColumnBomb = false;
        isRowBomb = false;
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
       // targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRowBomb = true;
            GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
            arrow.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //FindMatches();
        if (isMatched)
        {
            Debug.Log(isMatched);
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1 )
        {
            //Move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            if(board.allDots[column, row]!= this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
             
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
           
        }

    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if(otherDot != null)
        {
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
                
            }
            otherDot = null;
        }
        
    }

    private void OnMouseDown()
    {
        if (board.currentState == GameState.move)
        {
            firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(firstPosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalPosition.y - firstPosition.y) > swipeResist || Mathf.Abs(finalPosition.x - firstPosition.x) > swipeResist)
        {


            swipeAngle = Mathf.Atan2(finalPosition.y - firstPosition.y, finalPosition.x - firstPosition.x) * 180 / Mathf.PI;
            Debug.Log(swipeAngle);
            MovePieces();
            board.currentState = GameState.wait;
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePieces()
    {
        if (swipeAngle> -45 && swipeAngle <= 45 && column < board.width-1)
        {//Right SWIPE
            otherDot = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135  && row < board.height-1)
        {//UP SWIPE
            otherDot = board.allDots[column , row+1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }

        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {//Left SWIPE
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row >0)
        {//DOWN SWIPE
            otherDot = board.allDots[column , row-1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());


    }

    void FindMatches(){
        if(column > 0 && column < board.width - 1){
            GameObject leftdot1 = board.allDots[column - 1, row];
            GameObject rightdot1 = board.allDots[column + 1, row];
            if (leftdot1 != null && rightdot1 != null)
            {
                if (this.gameObject.CompareTag(leftdot1.tag) && rightdot1.tag == this.gameObject.tag)
                {
                    leftdot1.GetComponent<Dot>().isMatched = true;
                    rightdot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;

                }
            }

        }

        if (row > 0 && row < board.width - 1)
        {
            GameObject upDot1 = board.allDots[column , row + 1];
            GameObject downdot1 = board.allDots[column , row - 1];
            if (upDot1 != null && downdot1 != null)
            {
                if (this.gameObject.CompareTag(upDot1.tag) && downdot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downdot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;

                }
            }

        }
    }
}
 