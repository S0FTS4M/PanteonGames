﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    IEnumerable Move(int x, int y);
}
