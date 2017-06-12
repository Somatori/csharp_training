using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsAnyGroupPresent())
            {
                GroupData groop = new GroupData("aaa");
                groop.Header = "ddd";
                groop.Footer = "fff";

                app.Groups.Create(groop);
            }

            app.Groups.Remove(1);
        }
    }
}
