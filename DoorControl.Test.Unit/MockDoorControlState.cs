using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl.Test.Unit
{
    internal class MockDoorControlState : IDoorControlState
    {
        public DoorControlState State { get; set; }
    }
}
