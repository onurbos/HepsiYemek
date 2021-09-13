using System.Threading.Tasks;
using HepsiYemek.Controllers;
using HepsiYemek.Entities;
using HepsiYemek.Extensions;
using HepsiYemek.Repositories;
using HepsiYemek.Tests;
using Microsoft.Extensions.Caching.Distributed;
using NUnit.Framework;

namespace Test.ControllerTest
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ProductControllerTest
    {
        StrictMock<IProductRepository> productRepository;
        StrictMock<IDistributedCache> distributedCache;
        StrictMock<ICacheExtensions> cacheExtensions;


        ProductController controller;
        string id = "613f11192a251e32f7406c64";

        [SetUp]
        public void Init()
        {
            SetUpController();
        }

        private void SetUpController()
        {
            productRepository = new StrictMock<IProductRepository>();
            distributedCache = new StrictMock<IDistributedCache>();
            cacheExtensions = new StrictMock<ICacheExtensions>();

            controller = new ProductController(productRepository.Object, distributedCache.Object, cacheExtensions.Object);
        }

        [TearDown]
        public void VerifyMocks()
        {
            productRepository.VerifyAll();
            distributedCache.VerifyAll();
            cacheExtensions.VerifyAll();
        }

        #region Delete

        [Test]
        public void Delete_BadRequest()
        {
            //Arrange
            productRepository.Setup(x => x.Get(string.Empty)).Returns((Product) null);

            //Act
            var result = controller.Delete(string.Empty);

            //Assert
            AssertHelpers.AssertResult(result, 400);
        }
        
        [Test]
        public void Delete_ReturnNoContent()
        {
            //Arrange
            productRepository.Setup(x => x.Get(id)).Returns((Product) null);

            //Act
            var result = controller.Delete(id);

            //Assert
            AssertHelpers.AssertResult(result, 204);
        }

        #endregion

        #region get

        [Test]
        public async Task Get_ReturnBadRequest()
        {
            // Arrange
            productRepository.Setup(x => x.GetByIdAsBsonDoc(string.Empty));

            //Act
            var result = await controller.GetAsync(string.Empty);

            //Assert
            AssertHelpers.AssertResult(result, 400);
        }

        #endregion
    }
}