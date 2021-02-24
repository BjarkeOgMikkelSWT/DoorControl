using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl
{
    public interface IUserValidation
    {
        bool ValidateEntryRequest(int id);
    }
}
