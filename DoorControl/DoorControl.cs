using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControl
{
    public class DoorControl
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

            _state.State = DoorControlState.DoorClosed;
        }

        public void RequestEntry(int id) 
        {
            if(_state.State != DoorControlState.DoorClosed)
            {
                throw new ArgumentException("Invalid state to call in");
            }

            if (_UserValidation.ValidateEntryRequest(id))
            {
                _door.Open();
                _entryNotify.NotifyEntryGranted();
                _state.State = DoorControlState.DoorOpening;

            }
            else
            {
                _entryNotify.NotifyEntryDenied();
            }
        }

        public void DoorOpened()
        {
            switch(_state.State)
            {
                case DoorControlState.DoorClosed:
                    _door.Close();
                    _Alarm.RaiseAlarm();
                    _state.State = DoorControlState.DoorBreached;
                    break;
                
                case DoorControlState.DoorOpening:
                
                    _door.Close();
                    _state.State = DoorControlState.DoorClosing;
                    break;
                
                default:
                
                    throw new ArgumentException("Invalid state");
                
            }
        }

        public void DoorClosed()
        {
            switch (_state.State)
            {
                case DoorControlState.DoorClosing:
                    _state.State = DoorControlState.DoorClosed;
                    break;

                default:

                    throw new ArgumentException("Invalid state");

            }
        }
    }
}
