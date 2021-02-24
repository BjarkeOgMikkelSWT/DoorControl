using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl.Test.Unit
{
    internal class StubDoorControlState : IDoorControlState
    {
        public DoorControlState State { get; set; }
    }
}
