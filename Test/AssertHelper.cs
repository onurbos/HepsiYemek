using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;


namespace HepsiYemek.Tests
{
    public class AssertHelpers
    {
        internal static void AssertOkResult(IActionResult result)
        {
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOf<OkResult>(okResult);
        }

        internal static void AssertOkObjectResult(IActionResult result)
        {
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.IsInstanceOf<OkObjectResult>(okObjectResult);
        }

        internal static void AssertNotFoundResult(IActionResult result)
        {
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.IsInstanceOf<NotFoundResult>(notFoundResult);
        }

        internal static void AssertNotFoundObjectResult(IActionResult result)
        {
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.IsInstanceOf<NotFoundObjectResult>(notFoundResult);
        }

        internal static void AssertBadRequestResult(IActionResult result)
        {
            var notFoundResult = result as BadRequestResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(400, notFoundResult.StatusCode);
            Assert.IsInstanceOf<BadRequestResult>(notFoundResult);
        }

        internal static void AssertBadRequestObjectResult(IActionResult result)
        {
            var notFoundResult = result as BadRequestObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(400, notFoundResult.StatusCode);
            Assert.IsInstanceOf<BadRequestObjectResult>(notFoundResult);
        }

        internal static void AssertStatusCodeResult(IActionResult result, int statusCode)
        {
            var statusCodeResult = result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(statusCode, statusCodeResult.StatusCode);
            Assert.IsInstanceOf<StatusCodeResult>(statusCodeResult);
        }

        internal static void AssertForbidResult(IActionResult result)
        {
            var forbidResult = result as ForbidResult;
            Assert.IsNotNull(forbidResult);
            Assert.IsInstanceOf<ForbidResult>(forbidResult);
        }
    }
}
