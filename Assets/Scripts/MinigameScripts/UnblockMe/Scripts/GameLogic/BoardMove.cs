using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardMove 
{
    public Board Board { get; set; }
    public int MoveCount { get; set; }
    public BoardMove PreviousMove { get; set; }
    public HashSet<Board> KnownBoards { get; set; }
}
