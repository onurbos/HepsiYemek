using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;


namespace HepsiYemek.Tests
{
    public static class AssertHelpers
    {
        internal static void AssertResult(IActionResult result, int code)
        {
            var res = result as StatusCodeResult;
            Assert.IsNotNull(res);
            Assert.AreEqual(code, res.StatusCode);
            Assert.IsInstanceOf<StatusCodeResult>(res);
        }
    }
}