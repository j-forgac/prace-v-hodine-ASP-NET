namespace ConsoleApp1;

public class Spiral
{
    private int[,] intGrid;
    private int[] columnWidth; //stores the the highest digit count of number per column (used for padding when printing)
    private int x;
    private int y;
    private int num = 1;
    private int dir;// has value of 0-3, reference to move array
    private int[,] move =
    {
        { 1, 0 }, //0 -> left
        { 0, 1 }, //1 -> down
        { -1, 0 },//2 -> right
        { 0, -1 } //3 -> up
    };

    public Spiral(int size)
    {
        intGrid = new int[size, size];
        columnWidth = new int[size];
        while (num <= Math.Pow(size, 2)) // generate spiral
        {
            intGrid[y, x] = num;
            if (num.ToString().Length > columnWidth[x]) columnWidth[x] = num.ToString().Length; //update column width
            if (
                x + move[dir, 0] == size || y + move[dir, 1] == size || x + move[dir, 0] == -1 || y + move[dir, 1] == -1 //next position is out of border
                || intGrid[y + move[dir, 1], x + move[dir, 0]] != 0 //next position is occupied
            )
            {
                dir = (dir + 1) % 4; //change direction
            }
            
            x += move[dir, 0];
            y += move[dir, 1];
            num++;
        }
        
        for (int i = 0; i < size; i++) // print spiral
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write(intGrid[i, j].ToString().PadRight(columnWidth[j],' '));
            }
            Console.WriteLine();
        }
    }
}