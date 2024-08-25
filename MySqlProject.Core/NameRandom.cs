using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Core
{
    public class NameRandom
    {
        public static string ChineseName()
        {
            var faker = new Faker("zh_CN");
            var bogusName = faker.Name.FullName();
            var nameList = bogusName.Split(' ');
            return nameList[1] + nameList[0];

        }
    }
}
