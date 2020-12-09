namespace AdventOfCode_2020
{
    public enum OperationName
    {
        NOP,
        ACC,
        JMP
    }

    public record Operation(string Name, int Operands);

    public record Operand(int Value);

    public record Instruction(OperationName Operation, Operand Operand);
}