using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle
{

   //Add your puzzle here
   public static IEnumerable<Board> EasyPuzzles()
    {
        //easy puzzle #1
        yield return new Board(
      // Block to solve for:
      new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
        // Obstacle blocks:
        new Block(BlockOrientation.Orientation.Vertical, 0, 2, 3),
         new Block(BlockOrientation.Orientation.Vertical, 0, 3, 3),
          new Block(BlockOrientation.Orientation.Vertical, 1, 4, 3),
              new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2)

      );
        //easy puzzle #2
        yield return new Board(
       // Block to solve for:
       new Block(BlockOrientation.Orientation.Horizontal, 2, 1, 2),
         // Obstacle blocks:
         new Block(BlockOrientation.Orientation.Vertical, 0, 4, 3),
          new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 3),
          new Block(BlockOrientation.Orientation.Vertical, 2, 0, 2),
             new Block(BlockOrientation.Orientation.Vertical, 0, 3, 2),
              new Block(BlockOrientation.Orientation.Vertical, 2, 3, 2),
             new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2)

       );
        //easy puzzle #3
        yield return new Board(
      // Block to solve for:
      new Block(BlockOrientation.Orientation.Horizontal,2, 0, 2),
        // Obstacle blocks:
        new Block(BlockOrientation.Orientation.Vertical, 0, 2, 2),
          new Block(BlockOrientation.Orientation.Horizontal, 5, 0, 3),
          new Block(BlockOrientation.Orientation.Vertical, 2, 2, 3),
            new Block(BlockOrientation.Orientation.Vertical, 1, 5, 2),
             new Block(BlockOrientation.Orientation.Vertical, 3, 5, 2)

      );
        //easy puzzle #4
        yield return new Board
      (
      // Block to solve for:
      new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
       // Obstacle blocks:
       new Block(BlockOrientation.Orientation.Vertical, 0, 0, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 1, 2, 2),
      new Block(BlockOrientation.Orientation.Horizontal, 4, 4, 2),
        new Block(BlockOrientation.Orientation.Horizontal, 5, 3, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 4, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 5, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 3, 3)
          );

        //easy puzzle #5
        yield return new Board(
      // Block to solve for:
      new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
        // Obstacle blocks:
                 new Block(BlockOrientation.Orientation.Horizontal, 0, 0, 3),
                   new Block(BlockOrientation.Orientation.Vertical, 1, 2, 2),
                    new Block(BlockOrientation.Orientation.Vertical, 3, 0, 2),
                    new Block(BlockOrientation.Orientation.Horizontal, 3, 2, 2),
                     new Block(BlockOrientation.Orientation.Vertical, 1, 4, 3)
      );

    }

   public static IEnumerable<Board> MediumPuzzles()
   {
        //puzzle #1 (6)
        yield return new Board(
             // Block to solve for:
             new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
               // Obstacle blocks:
               new Block(BlockOrientation.Orientation.Vertical, 0, 2, 3),
                new Block(BlockOrientation.Orientation.Vertical, 0, 3, 3),
                 new Block(BlockOrientation.Orientation.Vertical, 1, 4, 2),
                  new Block(BlockOrientation.Orientation.Vertical, 3, 4, 2),
                   new Block(BlockOrientation.Orientation.Vertical, 3, 1, 2),
                    new Block(BlockOrientation.Orientation.Horizontal, 0, 4, 2),
                     new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2)

             );

        //Puzzle #2
        yield return new Board(
           // Block to solve for:
           new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
                 // Obstacle blocks:
                 new Block(BlockOrientation.Orientation.Horizontal, 0, 0, 3),
                   new Block(BlockOrientation.Orientation.Vertical, 1, 2, 3),
                    new Block(BlockOrientation.Orientation.Vertical, 3, 0, 2),
                    new Block(BlockOrientation.Orientation.Horizontal, 5, 0, 3),
                    new Block(BlockOrientation.Orientation.Vertical, 0, 5, 3),
                     new Block(BlockOrientation.Orientation.Horizontal, 3, 3, 2),
                     new Block(BlockOrientation.Orientation.Vertical, 4, 4, 2)

           );
        //puzzle #3
        yield return new Board
      (
      // Block to solve for:
      new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
       // Obstacle blocks:
       new Block(BlockOrientation.Orientation.Vertical, 0, 0, 2),
      new Block(BlockOrientation.Orientation.Vertical, 0, 1, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 0, 2, 2),
      new Block(BlockOrientation.Orientation.Horizontal, 0, 4, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 1, 4, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 3, 0, 3),
        new Block(BlockOrientation.Orientation.Horizontal, 5, 0, 2),
        new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2),
         new Block(BlockOrientation.Orientation.Vertical, 4, 4, 2),
          new Block(BlockOrientation.Orientation.Vertical, 4, 5, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 4, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 5, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 3, 3)

 );


        //puzzle #4
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 1, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 1, 3, 3),
            new Block(BlockOrientation.Orientation.Vertical, 1, 4, 3),
            new Block(BlockOrientation.Orientation.Horizontal, 3, 0, 2),
            new Block(BlockOrientation.Orientation.Vertical, 3, 2, 2),
            new Block(BlockOrientation.Orientation.Vertical, 4, 1, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2)
        );


        //Puzzle #2 (25)
        yield return new Board(
              // Block to solve for:
              new Block(BlockOrientation.Orientation.Horizontal, 2, 2, 2),
                // Obstacle blocks:
                new Block(BlockOrientation.Orientation.Vertical, 0, 1, 3),
                 new Block(BlockOrientation.Orientation.Vertical, 0, 4, 3),
                  new Block(BlockOrientation.Orientation.Horizontal, 5, 0, 3),
                  new Block(BlockOrientation.Orientation.Vertical, 3, 2, 2),
                   new Block(BlockOrientation.Orientation.Vertical, 4, 3, 2),
                   new Block(BlockOrientation.Orientation.Horizontal, 3, 3, 2),
                    new Block(BlockOrientation.Orientation.Vertical, 1, 5, 2),
                     new Block(BlockOrientation.Orientation.Vertical, 3, 5, 2)



              );
        //Puzzle 2
        yield return new Board
      (
      // Block to solve for:
      new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
       // Obstacle blocks:
       new Block(BlockOrientation.Orientation.Vertical, 0, 0, 2),
      new Block(BlockOrientation.Orientation.Vertical, 0, 1, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 0, 2, 2),
      new Block(BlockOrientation.Orientation.Horizontal, 0, 4, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 1, 4, 2),
       new Block(BlockOrientation.Orientation.Horizontal, 3, 0, 3),
        new Block(BlockOrientation.Orientation.Horizontal, 5, 0, 2),
        new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2),
         new Block(BlockOrientation.Orientation.Vertical, 4, 4, 2),
          new Block(BlockOrientation.Orientation.Vertical, 4, 5, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 4, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 5, 2),
          new Block(BlockOrientation.Orientation.Vertical, 2, 3, 3)

 );
        //puzzle #6
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 0, 3, 3),
            new Block(BlockOrientation.Orientation.Vertical, 1, 2, 2),
            new Block(BlockOrientation.Orientation.Vertical, 1, 5, 2),
            new Block(BlockOrientation.Orientation.Vertical, 3, 5, 2),
            new Block(BlockOrientation.Orientation.Vertical, 4, 1, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 5, 4, 2)
        );

        //puzzle #7
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 0, 0, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 0, 1, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 0, 3, 2),
            new Block(BlockOrientation.Orientation.Vertical, 1, 4, 3),
            new Block(BlockOrientation.Orientation.Vertical, 1, 5, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 3, 0, 3),
            new Block(BlockOrientation.Orientation.Vertical, 4, 2, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 5, 3, 3)
        );
        //#puzzle #3 (38)
        yield return new Board(
               // Block to solve for:
               new Block(BlockOrientation.Orientation.Horizontal, 2, 1, 2),
                 // Obstacle blocks:
                 new Block(BlockOrientation.Orientation.Vertical, 0, 4, 3),
                  new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 3),
                  new Block(BlockOrientation.Orientation.Vertical, 2, 0, 2),
                   new Block(BlockOrientation.Orientation.Horizontal, 3, 4, 2),
                   new Block(BlockOrientation.Orientation.Vertical, 4, 0, 2),
                      new Block(BlockOrientation.Orientation.Horizontal, 0, 0, 2),
                    new Block(BlockOrientation.Orientation.Vertical, 4, 1, 2),
                     new Block(BlockOrientation.Orientation.Vertical, 0, 3, 2),
                      new Block(BlockOrientation.Orientation.Vertical, 2, 3, 2),
                     new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2)



               );
        // puzzle 1 (801)
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 1, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 1, 3, 3),
             new Block(BlockOrientation.Orientation.Vertical, 1, 4, 3),
              new Block(BlockOrientation.Orientation.Vertical, 3, 2, 2),
               new Block(BlockOrientation.Orientation.Vertical, 4, 1, 2),
               new Block(BlockOrientation.Orientation.Horizontal, 3, 0, 2),
               new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2)


            );
        // puzzle 1 (803)
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 0, 3, 3),
             new Block(BlockOrientation.Orientation.Vertical, 1, 2, 2),
              new Block(BlockOrientation.Orientation.Vertical, 1, 5, 2),
               new Block(BlockOrientation.Orientation.Vertical, 3, 5, 2),
                new Block(BlockOrientation.Orientation.Vertical, 4, 1, 2),
                new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2),
                 new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2),
                 new Block(BlockOrientation.Orientation.Horizontal, 5, 4, 2)
            );

        // puzzle 1 (805)
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 2, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 1, 4, 3),
            new Block(BlockOrientation.Orientation.Vertical, 0, 1, 3),
            new Block(BlockOrientation.Orientation.Vertical, 3, 3, 2),
            new Block(BlockOrientation.Orientation.Vertical, 1, 5, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 5, 1, 3),
             new Block(BlockOrientation.Orientation.Horizontal, 3, 0, 2),
              new Block(BlockOrientation.Orientation.Horizontal, 1, 2, 2),
               new Block(BlockOrientation.Orientation.Horizontal, 4, 4, 2)
            );


        // puzzle 1 (820)
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 1, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 1, 3, 3),
              new Block(BlockOrientation.Orientation.Vertical, 3, 2, 2),
                new Block(BlockOrientation.Orientation.Vertical, 4, 1, 2),
                  new Block(BlockOrientation.Orientation.Vertical, 2, 4, 2),
                   new Block(BlockOrientation.Orientation.Horizontal, 0, 1, 2),
                      new Block(BlockOrientation.Orientation.Horizontal, 0, 3, 2),
                         new Block(BlockOrientation.Orientation.Horizontal, 4, 3, 2),
                            new Block(BlockOrientation.Orientation.Horizontal, 5, 2, 2),
                               new Block(BlockOrientation.Orientation.Horizontal, 5, 4, 2)


            );
        // puzzle 1 (809)
        yield return new Board(
            // Block to solve for:
            new Block(BlockOrientation.Orientation.Horizontal, 2, 0, 2),
            // Obstacle blocks:
            new Block(BlockOrientation.Orientation.Vertical, 0, 2, 3),
            new Block(BlockOrientation.Orientation.Vertical, 0, 5, 3),
            new Block(BlockOrientation.Orientation.Vertical, 0, 3, 2),
            new Block(BlockOrientation.Orientation.Vertical, 0, 4, 2),
            new Block(BlockOrientation.Orientation.Vertical, 2, 3, 2),
            new Block(BlockOrientation.Orientation.Vertical, 3, 1, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 0, 0, 2),
            new Block(BlockOrientation.Orientation.Horizontal, 4, 2, 2)
            );
    }

   public static IEnumerable<Board> HardPuzzles()
   {
       //puzzle
       yield return new Board(
           // Block to solve for:
           new Block(BlockOrientation.Orientation.Horizontal, 2, 1, 2)
           // Obstacle blocks:
           
           );

   }

    /// <summary>
    /// Breadth-first search implementation.
    /// </summary>
    /// <param name="initial"></param>
    /// <returns></returns>
    public static BoardSolution FindSolutionBFS(Board initial)
    {

        Queue<BoardMove> moves = new Queue<BoardMove>(1024);
        // Queue up the first board:
        moves.Enqueue(new BoardMove { Board = initial, MoveCount = 0, PreviousMove = null, KnownBoards = new HashSet<Board>() });

        HashSet<Board> knownBoards = new HashSet<Board>();

        // Process the queue until a solution is found:
        while (moves.Count != 0)
        {
            BoardMove move = moves.Dequeue();

            // Is this the winning move?
            if (move.Board.Blocks[0].Column >= Board.Width - 1)
            {
                // Build up the stack of previous moves leading to the initial move:
                Stack<Board> solutionMoves = new Stack<Board>(move.MoveCount);
                BoardMove tmp = move;
                while (tmp != null)
                {
                    solutionMoves.Push(tmp.Board);
                    tmp = tmp.PreviousMove;
                 
                }
                // Return the solution:
                return new BoardSolution { MoveCount = move.MoveCount, Moves = solutionMoves };
            }

            // Queue up the child legal moves that we haven't seen before on this thread:
#if UseExcept
                var remainingMoves = move.Board.GetLegalMoves().Except(knownBoards).ToList();
                knownBoards.UnionWith(remainingMoves);
#else
            var remainingMoves = move.Board.GetLegalMoves();
#endif

            foreach (var validBoard in remainingMoves)
            {
#if !UseExcept
                if (knownBoards.Contains(validBoard)) continue;
                knownBoards.Add(validBoard);
#endif
                moves.Enqueue(new BoardMove
                {
                    Board = validBoard,
                    MoveCount = move.MoveCount + 1,
                    PreviousMove = move
                });
            }
        }

        // No solution:
        return null;
    }

}
