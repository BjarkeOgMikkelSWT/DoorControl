using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl
{
    class DoorControl
    {
        private readonly IDoorControlState _state;
        private readonly IDoor _door;
        private readonly IEntryNotification _entryNotify;
        private readonly IAlarm _Alarm;
        private readonly IUserValidation _UserValidation;





        public DoorControl(IDoorControlState state, IDoor door,  IUserValidation vali, IEntryNotification entryNotify, IAlarm alarm)
        {
            _state = state;
            _door = door;
            _UserValidation = vali;
            _entryNotify = entryNotify;
            _Alarm = alarm;
        }

        public void RequestEntry(int id) 
        {
            if(_UserValidation.ValidateEntryRequest(id))
            {
                
            }
        }

        public void DoorOpened()
        {

        }
    }
}
