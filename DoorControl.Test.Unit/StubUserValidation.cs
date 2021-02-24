using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl.Test.Unit
{
    internal class StubUserValidation : IUserValidation
    {
        private readonly int _targetID;

        internal StubUserValidation(int targetID = 42)
        {
            _targetID = targetID;
        }
        
        public bool ValidateEntryRequest(int id)
        {
            return _targetID == id;
        }
    }
}
