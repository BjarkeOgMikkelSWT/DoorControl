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
            _state = Substitute.For<IDoorControlState>();
            _validation = new StubUserValidation();
            _alarm = Substitute.For<IAlarm>();
            _door = Substitute.For<IDoor>();
            _entry = Substitute.For<IEntryNotification>();

            _UUT = new DoorControl(_state, _door, _validation, _entry, _alarm);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
            
        }
    }
}