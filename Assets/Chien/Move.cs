using UnityEngine;

public class Move
{
    public Move(MoveBase @base)
    {
        Base = @base;
        PP = @base.PP;
    }

    public MoveBase Base { get; set; }
    public int PP { get; set; }

}
