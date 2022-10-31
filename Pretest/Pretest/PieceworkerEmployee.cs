using System;

namespace Pretest
{
    public class PieceworkerEmployee : Employee
    {
        //  Constructor
        public PieceworkerEmployee(string firstName, string lastName, int empNum,
                                   int pieces, decimal priceperpiece)
                            : base(firstName, lastName, empNum)
        {
            Pieces = pieces;
            PricePerPiece = priceperpiece;
        }


        //  Getters and Setters
        int Pieces { get; set; }
        decimal PricePerPiece { get; set; }

        //  Override the existing displayText() method
        public override string displayText()
        {
            return base.displayText() +
                "\r\nPieces Made: " + Pieces.ToString() +
                "\r\nPrice/Piece: " + PricePerPiece.ToString("c");
        }
    }
}
