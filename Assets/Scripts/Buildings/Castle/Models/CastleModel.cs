using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CastleModel
{
    public ReactiveProperty<int> Health { get; private set; } = new ReactiveProperty<int>(1000);
}
