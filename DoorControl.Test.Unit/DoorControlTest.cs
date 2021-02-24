using System;
using NSubstitute;
using NUnit.Framework;

namespace DoorControl.Test.Unit
{
    [TestFixture]
    public class DoorControlTest
    {
        private IDoorControlState _state;
        private IDoor _door;
        private IEntryNotification _entry;
        private IAlarm _alarm;
        private IUserValidation _validation;

        private DoorControl _UUT;

        [SetUp]
        public void Setup()
        {
            _state = new StubDoorControlState();
            _validation = new StubUserValidation();
            _alarm = Substitute.For<IAlarm>();
            _door = Substitute.For<IDoor>();
            _entry = Substitute.For<IEntryNotification>();

            _UUT = new DoorControl(_state, _door, _validation, _entry, _alarm);
        }

        [Test]
        public void DoorControlConstructor_StateIsDoorClosed()
        {
            Assert.That(_state.State, Is.EqualTo(DoorControlState.DoorClosed));
        }

        [TestCase(42, true)]
        [TestCase(43, false)]
        [TestCase(-1, false)]
        [TestCase(0, false)]

        public void RequestEntryOpensDoorTest_StateDoorClosed(int id, bool doorOpened)
        {
            _UUT.RequestEntry(id);

            if (doorOpened)
            {
                _door.Received(1).Open();
            }
            else
            {
                _door.DidNotReceive().Open();
            }
        }

        [TestCase(42, true)]
        [TestCase(43, false)]
        [TestCase(-1, false)]
        [TestCase(0, false)]

        public void RequestEntryEntryCallTest_StateDoorClosed(int id, bool granted)
        {
            _UUT.RequestEntry(id);

            if (granted)
            {
                _entry.Received(1).NotifyEntryGranted();
                _entry.DidNotReceive().NotifyEntryDenied();
            }
            else
            {
                _entry.Received(1).NotifyEntryDenied();
                _entry.DidNotReceive().NotifyEntryGranted();
            }
        }

        [TestCase(42, DoorControlState.DoorOpening)]
        [TestCase(43, DoorControlState.DoorClosed)]
        [TestCase(-1, DoorControlState.DoorClosed)]
        [TestCase(0, DoorControlState.DoorClosed)]

        public void RequestEntryEntryStateChanged_StateDoorClosed(int id, DoorControlState state)
        {
            _UUT.RequestEntry(id);

            Assert.That(_state.State, Is.EqualTo(state));
        }

        [TestCase( 42, DoorControlState.DoorOpening)]
        [TestCase(-1, DoorControlState.DoorOpening)]
        [TestCase(0, DoorControlState.DoorOpening)]
        [TestCase(90, DoorControlState.DoorOpening)]

        [TestCase(42, DoorControlState.DoorBreached)]
        [TestCase(-1, DoorControlState.DoorBreached)]
        [TestCase(0, DoorControlState.DoorBreached)]
        [TestCase(90, DoorControlState.DoorBreached)]

        [TestCase(42, DoorControlState.DoorClosing)]
        [TestCase(-1, DoorControlState.DoorClosing)]
        [TestCase(0, DoorControlState.DoorClosing)]
        [TestCase(90, DoorControlState.DoorClosing)]
        public void RequestEntryThrowsException_StateDoorNotClosed(int id, DoorControlState state)
        {
            _state.State = state;
            Assert.Throws<ArgumentException>(() =>_UUT.RequestEntry(id));
        }
        
        [TestCase(DoorControlState.DoorBreached)]
        [TestCase(DoorControlState.DoorClosing)]
        
        public void DoorOpenedThrowsArgumentException_StateDoorNotClosedOrOpening(DoorControlState state)
        {
            _state.State = state;
            Assert.Throws<ArgumentException>(() => _UUT.DoorOpened());
        }

        [TestCase(DoorControlState.DoorClosed)]
        [TestCase(DoorControlState.DoorOpening)]
        public void DoorOpenedValidState_DoorCloseCalled(DoorControlState state)
        {
            _state.State = state;
            _UUT.DoorOpened();

            _door.Received(1).Close();
        }

        [TestCase(DoorControlState.DoorClosed, true)]
        [TestCase(DoorControlState.DoorOpening, false)]
        public void DoorOpenedValidState_RaiseAlarmCalled(DoorControlState state, bool RaiseAlarmExpected)
        {
            _state.State = state;
            _UUT.DoorOpened();

            if (RaiseAlarmExpected)
            {
                _alarm.Received(1).RaiseAlarm();
            }
            else
            {
                _alarm.DidNotReceive().RaiseAlarm();
            }

        }

        [TestCase(DoorControlState.DoorClosed, DoorControlState.DoorBreached)]
        [TestCase(DoorControlState.DoorOpening, DoorControlState.DoorClosing)]
        public void DoorOpenedValidState_CorrectStateChange(DoorControlState initialState, DoorControlState expectedState)
        {
            _state.State = initialState;
            _UUT.DoorOpened();

           Assert.That(_state.State, Is.EqualTo(expectedState));

        }

        [Test]
        public void DoorClosedStateDoorClosing_StateChangedToClosed()
        {
            _state.State = DoorControlState.DoorClosing;
            _UUT.DoorClosed();

            Assert.That(_state.State, Is.EqualTo(DoorControlState.DoorClosed));

        }

        [TestCase(DoorControlState.DoorBreached)]
        [TestCase(DoorControlState.DoorOpening)]
        [TestCase(DoorControlState.DoorClosed)]

        public void DoorClosedThrowsArgumentException_StateDoorNotClosing(DoorControlState state)
        {
            _state.State = state;
            Assert.Throws<ArgumentException>(() => _UUT.DoorClosed());
        }


    }
}