using System;
using MeetTheFamily.Core.Models;
using MeetTheFamily.Core.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MeetTheFamily.UnitTests
{
    [TestClass]
    public class PersonUnitTest
    {
        Person _root;
        [TestInitialize]
        public void Init()
        {
            _root = Person.Create("Root", Gender.Male);
        }
        [TestCleanup]
        public void CleanUp()
        {
            _root = null;
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "Root" + ExceptionMessage.NO_CHILD_WITHOUT_SPOUSE)]
        public void Should_ThrowException_WhenAddingChildWithoutHavingSpouse()
        {
            //arrange

            //act
            _root.AddChild("child", Gender.Male);
        }

        [TestMethod]
        public void Should_Success_WhenAddingChildToFather()
        {
            //arrange
            AddSpouse();

            //act
            var child = _root.AddChild("child", Gender.Male);

            //assert
            AssertNewlyAddedChild(child);
        }

        [TestMethod]
        public void Should_Success_WhenAddingChildToMother()
        {
            //arrange
            AddSpouse();

            //act
            var child = _root.Spouse.AddChild("child", Gender.Male);

            //assert
            AssertNewlyAddedChild(child);
        }



        [TestMethod]
        [ExpectedException(typeof(Exception), ExceptionMessage.SAME_GENDER_FOR_SPOUSE)]
        public void Should_ThrowException_WhenSetingSpouseOfSameGender()
        {
            //act
            _root.Spouse = Person.Create("Spouse", Gender.Male);
        }

        [TestMethod]
        public void Should_Success_WhenSettingSpouseOfOppositeGender()
        {
            //arrange
            var spouse = Person.Create("Spouse", Gender.Female);

            //act
            _root.SetSpouse(spouse);

            //assert
            Assert.AreEqual(_root.Spouse, spouse);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "root " + ExceptionMessage.NO_CHILDREN)]
        public void Should_ThrowException_WhenThereIsNoChildre()
        {
            //act
            var children = _root.Childrens;
        }
        [TestMethod]
        public void Should_Return_Brothers()
        {
            //arrange
            AddSpouse();
            var firstChild = AddChildrenAndReturnFirstChild();

            //act
            var brothers = firstChild.Brothers;

            //assert
            Assert.AreEqual(2, brothers.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "child1" + ExceptionMessage.NO_BROTHER)]
        public void Should_ThrowException_WhenGettingBrothersCurrentIsOnlyMaleChild()
        {
            //arrange
            AddSpouse();
            var firstChild = _root.Spouse.AddChild("child1", Gender.Male);
            _root.Spouse.AddChild("child2", Gender.Female);

            //act
            var brothers = firstChild.Brothers;

            //assert

        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "child1" + ExceptionMessage.NO_SISTER)]
        public void Should_ThrowException_WhenGettingSistersCurrentIsOnlyFemailChild()
        {
            //arrange
            AddSpouse();
            _root.Spouse.AddChild("child1", Gender.Male);
            var firstChild = _root.Spouse.AddChild("child2", Gender.Female);

            //act
            var brothers = firstChild.Sisters;

            //assert

        }

        [TestMethod]
        public void Should_Return_Sisters()
        {
            //arrange
            AddSpouse();
            var firstChild = AddChildrenAndReturnFirstChild();

            //act
            var sisters = firstChild.Sisters;

            //assert
            Assert.AreEqual(2, sisters.Count);
        }

        [TestMethod]
        public void Should_Return_Sons()
        {
            //arrange
            AddChildrenAndReturnFirstChild();
            //act
            var sons = _root.Sons;

            //assert
            Assert.AreEqual(3, sons.Count);
        }
        [TestMethod]
        public void Should_Return_Daughter()
        {
            //arrange
            AddChildrenAndReturnFirstChild();
            //act
            var daugters = _root.Daughters;

            //assert
            Assert.AreEqual(2, daugters.Count);
        }

        [TestMethod]
        public void Should_Return_GrandDaughter()
        {
            //arrange
            AddChildrenAndReturnFirstChild();
            AddGrandChildrenAndReturnOneGrandChild();

            //act
            var grandDaughter = _root.GrandDaughters;
            //assert
            Assert.AreEqual(2, grandDaughter.Count);
        }

        [TestMethod]
        public void Should_Return_Cousins()
        {
            //arrange
            AddChildrenAndReturnFirstChild();
            var grandChild = AddGrandChildrenAndReturnOneGrandChild();

            //act
            var cousins = grandChild.Cousins;
            //assert
            Assert.AreEqual(2, cousins.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Grand daughter 2 " + ExceptionMessage.NO_COUSINS)]
        public void Should_ThrowException_WhenConsinsDoesNotExists()
        {
            //arrange
            AddChildrenAndReturnFirstChild();
            var grandChild = AddGrandSon();

            //act
            var cousins = grandChild.Cousins;

        }



        [TestMethod]
        [ExpectedException(typeof(Exception), "root " + ExceptionMessage.NO_GRANDDAUGHTER)]
        public void Should_ThrowException_WhenGrandDaughterIsNotThere()
        {
            //arrange
            AddChildrenAndReturnFirstChild();
            //act
            var grandDaughter = _root.GrandDaughters;
        }

        private Person AddChildrenAndReturnFirstChild()
        {
            AddSpouse();
            var firstChild = _root.AddChild("Male1", Gender.Male);
            _root.AddChild("Male2", Gender.Male);
            _root.AddChild("Male3", Gender.Male);
            _root.AddChild("Female1", Gender.Female);
            _root.AddChild("Female2", Gender.Female);
            return firstChild;
        }

        private void AssertNewlyAddedChild(Person child)
        {
            Assert.IsNotNull(child);
            Assert.AreEqual(child.Name, "child");
            Assert.AreEqual(child.Mother, _root.Spouse);
            Assert.AreEqual(child.Father, _root);
            Assert.AreEqual(1, _root.Childrens.Count);
            Assert.AreEqual(child, _root.Childrens[0]);
        }

        private void AddSpouse()
        {
            var spouse = Person.Create("Spouse", Gender.Female);
            _root.SetSpouse(spouse);
        }

        private Person AddGrandChildrenAndReturnOneGrandChild()
        {
            var daughter = _root.Daughters[0];
            daughter.Spouse = Person.Create("Spouse1", Gender.Male);
            daughter.AddChild("Grand son 1", Gender.Male);
            daughter.AddChild("Grand daughter 1", Gender.Female);
            return AddGrandSon();
        }

        private Person AddGrandSon()
        {
            var son = _root.Sons[0];
            son.Spouse = Person.Create("Spouse2", Gender.Female);
            return son.AddChild("Grand daughter 2", Gender.Female);
        }
    }
}