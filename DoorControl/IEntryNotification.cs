using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl
{
    interface IEntryNotification
    {
        void NotifyEntryGranted();
        void NotifyEntryDenied();
    }
}
