namespace AdventOfCode_2020
{
    public record BoardingPass(int Row, int Column)
    {
        public int Id => Row * 8 + Column;

        public void Deconstruct(out int Row, out int Column, out int Id)
        {
            Row = this.Row;
            Column = this.Column;
            Id = this.Id;
        }
    }
}
