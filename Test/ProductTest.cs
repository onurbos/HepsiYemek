using FakeItEasy;
using HepsiYemek.Controllers;
using HepsiYemek.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HepsiYemek.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ProductTest
    {

        StrictMock<IProductRepository> productRepository;
        StrictMock<IDistributedCache> distributedCache;

        ProductController controller;

        [SetUp]
        public void Init()
        {
            SetUpController();
        }

        public void SetUpController()
        {
            productRepository = new StrictMock<IProductRepository>();
            distributedCache = new StrictMock<IDistributedCache>();

            controller = new ProductController(productRepository.Object, distributedCache.Object);
        }

        [TearDown]
        public void VerifyMocks()
        {
            productRepository.VerifyAll();
            distributedCache.VerifyAll();
        }


        [Test]
        public async Task Delete_ReturnOk()
        {
            //Arrange

            productRepository.Setup(x => x.Delete(""));

            //Act
             var result = await controller.DeleteAsync("");

            //Assert
             AssertHelpers.AssertOkResult(result);
        }
    }
}
