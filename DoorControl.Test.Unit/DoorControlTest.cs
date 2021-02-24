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
            var dummyState = Substitute.For<IDoorControlState>();
            _validation = new StubUserValidation();
            var dummyAlarm = Substitute.For<IAlarm>();
            var dummyDoor = Substitute.For<IDoor>();
            var dummyEntry = Substitute.For<IEntryNotification>();

            _UUT = new DoorControl(dummyState, dummyDoor, _validation, dummyEntry, dummyAlarm);
        }

        [Test]
        public void Test1()
        {
            _UUT.DoorOpened();
        }
    }
}