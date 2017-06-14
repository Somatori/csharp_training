using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (!app.Groups.IsAnyGroupPresent())
            {
                GroupData groop = new GroupData("aaa");
                groop.Header = "ddd";
                groop.Footer = "fff";

                app.Groups.Create(groop);
            }

            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;

            app.Groups.Modify(0, newData);
        }
    }
}
