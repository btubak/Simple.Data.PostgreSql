﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Data.PostgreSqlTest
{
  public class EnumTest
  {
    [SetUp]
    public void SetUp()
    {
      DatabaseUtility.CreateDatabase("Test");
    }

    [TearDown]
    public void TearDown()
    {
      DatabaseUtility.DestroyDatabase("Test");
    }

    [Test]
    public void ConvertsBetweenEnumAndInt()
    {
      var db = Database.Open();
      EnumTestClass actual = db.EnumTest.Insert(Flag: TestFlag.One);
      Assert.AreEqual(TestFlag.One, actual.Flag);

      actual.Flag = TestFlag.Three;

      db.EnumTest.Update(actual);

      actual = db.EnumTest.FindById(actual.Id);
      Assert.AreEqual(TestFlag.Three, actual.Flag);
    }
  }

  class EnumTestClass
  {
    public int Id { get; set; }
    public TestFlag Flag { get; set; }
  }

  enum TestFlag
  {
    None = 0,
    One = 1,
    Two = 2,
    Three = 3
  }
}
