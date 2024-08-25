using UnityEngine;
using System.Collections;
using System.Linq;
public struct Block  
{
    public int _row;
    public int _col;
    public int _length;
    BlockOrientation.Orientation _orientation;

    public int Row { get { return _row; } }
    public int Column { get { return _col; } }
    public int Length { get { return _length; } }
    public BlockOrientation.Orientation Orientation { get { return _orientation; } }

    public Block(BlockOrientation.Orientation orientation, int row, int col, int length)
    {

        _orientation = orientation;
        _row = row;
        _col = col;
        _length = length;
    }

    static bool IsInRange(int x, int start, int end)
    {
        return x >= start && x <= end;
    }

    public bool Intersects(Block other)
    {
        if (this.Orientation == BlockOrientation.Orientation.Horizontal)
        {
            // Horizontal this:
            if (other.Orientation == BlockOrientation.Orientation.Horizontal)
            {
                // Horizontal this, Horizontal other:

                if (this.Row != other.Row) return false;

                if (IsInRange(this.Column, other.Column, other.Column + other.Length - 1)) return true;
                if (IsInRange(this.Column + this.Length - 1, other.Column, other.Column + other.Length - 1)) return true;
                if (IsInRange(other.Column, this.Column, this.Column + this.Length - 1)) return true;
                if (IsInRange(other.Column + other.Length - 1, this.Column, this.Column + this.Length - 1)) return true;

                return false;
            }
            else
            {
                // Horizontal this, Vertical other:

                if (!IsInRange(this.Row, other.Row, other.Row + other.Length - 1)) return false;
                if (IsInRange(other.Column, this.Column, this.Column + this.Length - 1)) return true;

                return false;
            }
        }
        else
        {
            // Vertical this:
            if (other.Orientation == BlockOrientation.Orientation.Horizontal)
            {
                // Vertical this, Horizontal other:

                if (!IsInRange(other.Row, this.Row, this.Row + this.Length - 1)) return false;
                if (IsInRange(this.Column, other.Column, other.Column + other.Length - 1)) return true;

                return false;
            }
            else
            {
                // Vertical this, Vertical other:

                if (this.Column != other.Column) return false;

                if (IsInRange(this.Row, other.Row, other.Row + other.Length - 1)) return true;
                if (IsInRange(this.Row + this.Length - 1, other.Row, other.Row + other.Length - 1)) return true;
                if (IsInRange(other.Row, this.Row, this.Row + this.Length - 1)) return true;
                if (IsInRange(other.Row + other.Length - 1, this.Row, this.Row + this.Length - 1)) return true;

                return false;
            }
        }
    }

   

   

	
}
