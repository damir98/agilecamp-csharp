using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace ClassLibrary1
{
    class FirstTests
    {
        [Test]
        public void FirstSuccessTest()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void FirstFailTest()
        {
            Assert.True(false);
        }

        [Test]
        public void Test1()
        {
            Assert.IsEmpty("");
        }

        [Test]
        public void Test2()
        {
            Assert.Greater(1, 2);
        }
    }

    public class IntTests
    {
        [Test]
        public void ParseCorrect_ReturnInt()
        {
            int result;
            var b = int.TryParse("123", out result);

            Assert.AreEqual(result, 123);

            Assert.AreEqual(int.TryParse("fff", out result), false);
        }

        [Test]
        public void ExceptionTests()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                string f = null;
                int a = f.Length;
            });
        }

        [Test]
        public void RegexTest()
        {
            var matches = Regex.Matches("1 2 3", @"\d+");

            Assert.AreEqual("1", matches[0].Value);
            Assert.AreEqual("2", matches[1].Value);
            Assert.AreEqual("3", matches[2].Value);
        }

        [Test]
        public void WrongTest()
        {
            int a = new Random().Next(0, 100);
            Assert.Less(a, 50);
        }

    }


    public interface IDataBaseContext
    {
        IQueryable<T> Query<T>();
    }

    public class User
    {
        public int Id { get; set; }
    }

    public class MemberShipService
    {
        private readonly IDataBaseContext _dataBaseContext;

        public MemberShipService(IDataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public User FindContextUser(int userId)
        {
            var user = _dataBaseContext.Query<User>().SingleOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException();

            return user;
        }
    }

    public class MembershipTests
    {
        private static readonly Mock<IDataBaseContext> DataBaseContext = new Mock<IDataBaseContext>();

        [Test]
        public void IfUserNotFound_ThrowException()
        {
            DataBaseContext.Setup(x => x.Query<User>()).Returns(new List<User>().AsQueryable());
            var memberShipService  = new MemberShipService(DataBaseContext.Object);
            Assert.Throws<ArgumentException>(() =>
            {
                memberShipService.FindContextUser(1);
            });

            DataBaseContext.Verify(x=>x.Query<User>(), Times.AtLeast(1));
        }

        [Test]
        public void IfUserFound_ReturnFoundUser()
        {
            DataBaseContext.Setup(x => x.Query<User>()).Returns(new List<User>() {new User() {Id = 1} }.AsQueryable());
            var memberShipService = new MemberShipService(DataBaseContext.Object);
            var user = memberShipService.FindContextUser(1);
            Assert.AreEqual(1, user.Id);
        }
    }

    class Habit
    {
        private const string _prefix = "habit: ";
        public Habit(string habitName)
        {
            if (string.IsNullOrEmpty(habitName))
                throw new ArgumentNullException();
            if(habitName.Length < 2)
                throw new ArgumentException();
            Name = _prefix + habitName;
        } 

        public string Name { get; set; }
    }

    class HabitTests
    {
        private string validName = "ValidName";

        [Test]
        public void CantCreateUnnamedHabitTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Habit(habitName : null));
        }

        [Test]
        public void IfHabitNameLessThanTwo()
        {
            Assert.Throws<ArgumentException>(() => new Habit(habitName: "2"));
        }

        [Test]
        public void CanCreateHabit()
        {
            Assert.DoesNotThrow(() => new Habit(validName));
        }

        [Test]
        public void HabitHasCorrectName()
        {
            var prefix = "habit: ";
            var habit = new Habit(validName);
            Assert.True(habit.Name.StartsWith(prefix));
        }
    }
}
