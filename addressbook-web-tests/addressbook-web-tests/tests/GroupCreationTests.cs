using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData groop = new GroupData("aaa");
            groop.Header = "ddd";
            groop.Footer = "fff";

            app.Groups.Create(groop);
            // app.Auth.Logout();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData groop = new GroupData("");
            groop.Header = "";
            groop.Footer = "";

            app.Groups.Create(groop);
            // app.Auth.Logout();
        }
    }
}
