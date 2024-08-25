using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public struct Board 
{
    public Block[] _blocks;
    public int _highlightIndex;
    public const int Width = 6;
    public const int Height = 6;
    public const int HoleRow = 2;

    public Block[] Blocks { get { return _blocks; } }
    public int HighlightIndex { get { return _highlightIndex; } }

    /// <summary>
    /// Create a new board with a highlighted block.
    /// </summary>
    /// <param name="highlightIndex"></param>
    /// <param name="blocks"></param>
    public Board(int highlightIndex, params Block[] blocks)
    {
        _highlightIndex = highlightIndex;
        _blocks = blocks;

        testBlocks();
    }

    /// <summary>
    /// Create a new board.
    /// </summary>
    /// <param name="blocks"></param>
    public Board(params Block[] blocks)
    {
        _highlightIndex = -1;
        _blocks = blocks;

        testBlocks();
    }

    /// <summary>
    /// Test the block configuration to make sure it is legal.
    /// </summary>
    private void testBlocks()
    {
        //// First block is the solution block and it must be on the hole row:
        //Debug.Log(_blocks.Length >= 1);
        //Debug.Log(_blocks[0].Row == HoleRow);

        // Self-test the blocks:
        for (int i = 0; i < _blocks.Length; ++i)
            for (int j = 0; j < _blocks.Length; ++j)
            {
                if (i == j) continue;
                //Debug.Log(!_blocks[i].Intersects(_blocks[j]));
            }
    }

    /// <summary>
    /// Finds all legal moves from this board position.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Board> GetLegalMoves()
    {
        IEnumerable<Board> legalMoves = Enumerable.Empty<Board>();

        // Loop through each block to find legal moves:
        for (int i = 0; i < _blocks.Length; ++i)
        {
            int idx = i;
            Block curr = _blocks[idx];
            Block[] tmpBlocks = _blocks;
            Block[] testBlocks = _blocks.ExceptElement(idx);

            // Try all valid step-sizes of motions for this block:
            if (curr.Orientation == BlockOrientation.Orientation.Horizontal)
            {
                // Check moving left and right:
                var movesLeft = (
                        from t in Extensions.ReverseRange(curr.Column - 1, curr.Column)
                        select new Block(BlockOrientation.Orientation.Horizontal, curr.Row, t, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                // Block #0 is the solution block and can move one further to the right into the hole on row #2.
                var movesRight = (
                        from t in Enumerable.Range(curr.Column + 1, (idx == 0 ? Width + 1 : Width) - curr.Length - curr.Column)
                        select new Block(BlockOrientation.Orientation.Horizontal, curr.Row, t, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                // Don't evaluate edge cases of left or right:
                if (curr.Column == 0) movesLeft = Enumerable.Empty<Block>();
                if (curr.Column + curr.Length - 1 == Board.Width - 1) movesRight = Enumerable.Empty<Block>();

                // Concatenate the board builders:
                legalMoves = legalMoves.Concat(
                    movesRight.Concat(movesLeft)
                    .Select(x => new Board(idx, tmpBlocks.ReplaceElement(idx, x)))
                );
            }
            else
            {
                // Check moving up and down:
                var movesUp = (
                        from t in Extensions.ReverseRange(curr.Row - 1, curr.Row)
                        select new Block(BlockOrientation.Orientation.Vertical, t, curr.Column, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                var movesDown = (
                        from t in Enumerable.Range(curr.Row + 1, Height - curr.Length - curr.Row)
                        select new Block(BlockOrientation.Orientation.Vertical, t, curr.Column, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                // Don't evaluate edge cases of left or right:
                if (curr.Row == 0) movesUp = Enumerable.Empty<Block>();
                if (curr.Row + curr.Length - 1 == Board.Height - 1) movesDown = Enumerable.Empty<Block>();

                // Concatenate the board builders:
                legalMoves = legalMoves.Concat(
                    movesUp.Concat(movesDown)
                    .Select(x => new Board(idx, tmpBlocks.ReplaceElement(idx, x)))
                );
            }
        }

        return legalMoves;
    }


    public override bool Equals(object obj)
    {
        if (!(obj is Board)) return false;

        Board other = (Board)obj;

        if (other._blocks.Length != this._blocks.Length) return false;

        for (int i = 0; i < this._blocks.Length; ++i)
            if (!other._blocks[i].Equals(this._blocks[i]))
                return false;

        return true;
    }

    public override int GetHashCode()
    {
        return this._blocks.Aggregate(0, (i, b) => i ^ b.GetHashCode());
    }

    public struct Movement
    {
        public int Left;
        public int Right;
        public int Up;
        public int Down;
        public Movement(int _Left, int _Right,int _Up,int _Down)
        {
            Left = _Left;
            Right = _Right;
            Up = _Up;
            Down = _Down;
        }
    }


    //calculate left max movemnt and right movemnt of current block
     public Movement CalculateMovement(int index)
    {

        Block currblock =_blocks[index];
        Movement _movement = new Movement();

        Block[] testBlocks = _blocks.ExceptElement(index);

        if (currblock.Orientation == BlockOrientation.Orientation.Horizontal)
        {
            
            //Calculate Left Max Movement
            _movement.Left=     (from t in Extensions.ReverseRange(currblock.Column - 1, currblock.Column)
                        select new Block(BlockOrientation.Orientation.Horizontal, currblock.Row, t, currblock.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x))).Count();

            //Calculate Right Max Movement
             _movement.Right= (
                        from t in Enumerable.Range(currblock.Column + 1, (index == 0 ? Width + 1 : Width) - currblock.Length - currblock.Column)
                        select new Block(BlockOrientation.Orientation.Horizontal, currblock.Row, t, currblock.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x))).Count();
           
        }
        else
        {
            _movement.Right = 0;

            _movement.Up = 0;
          //Calculate  Up Max Movement
            _movement.Up=(
                        from t in Extensions.ReverseRange(currblock.Row - 1, currblock.Row)
                        select new Block(BlockOrientation.Orientation.Vertical, t, currblock.Column, currblock.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x))).Count();

            //Calculate  Down Max Movement
            _movement.Down = (
                        from t in Enumerable.Range(currblock.Row + 1, Height - currblock.Length - currblock.Row)
                        select new Block(BlockOrientation.Orientation.Vertical, t, currblock.Column, currblock.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x))).Count();
        }


        return _movement;
    }


    //change row & coulmn  value after each movement
     public Block AddNewValue(int index, int val)
     {

         Block tempBlock=_blocks[index];
         if (tempBlock.Orientation == BlockOrientation.Orientation.Horizontal)
         {
             tempBlock._col = val;
         }
         else
         {
             tempBlock._row = val;
         }

         _blocks[index] = tempBlock;
         return tempBlock;
     }

}
