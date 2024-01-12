using Entities.Entities;
using Newtonsoft.Json;

namespace TestProject
{
    [TestClass]
    public class MessageTest
    {
        private const string ENDPOINT = "https://localhost:7006/api/message/";

        [TestMethod]
        public void AddTest()
        {
            Helper helper = new Helper();

            var data = new
            {
                id = 0,
                title = "test message " + DateTime.Now.ToString(),
                active = true,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                deletedAt = DBNull.Value,
                UserId = Guid.NewGuid()
            };

            var result = helper.execApiPost(true, ENDPOINT, "Add", data).Result;

            if (result != null)
            {
                var listMessage = JsonConvert.DeserializeObject<Message[]>(result).ToList();

                Assert.IsTrue(listMessage.Any());
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void ListTest()
        {
            Helper helper = new Helper();

            var result = helper.execApiPost(true, ENDPOINT, "List").Result;

            if (result != null)
            {
                var listaMessage = JsonConvert.DeserializeObject<Message[]>(result).ToList();

                Assert.IsTrue(listaMessage.Any());
            }
            else
                Assert.Fail();
        }
    }
}
