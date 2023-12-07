using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomLayoutGenerator
{
    public abstract void ResetToDefaultState();
    public abstract GameObject BuildRoom(string args);
}
