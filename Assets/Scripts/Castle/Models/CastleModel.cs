using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Castle
{
    public class CastleModel
    {
        public ReactiveProperty<int> Health = new(50);
    }
}
