using MakFood.Authentication.Host.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    
    public class TestRequireLocalAttribute
    {
        [Fact]
        public async Task RequireLocal_EnteringWithWrongHostAddress_ShouldNotReturnNullAndAndStatusCodeShouldBe403Async()
        {
            //Arrange
            var attributeFilter = new RequireLocalAttribute();


            var httpContext = new DefaultHttpContext();
            httpContext.Request.Host = new HostString("111",5225);
            var actionContext = new ActionContext()
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };


            var executedContext = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), controller: null);
            var nextActionAfterSuccess = new ActionExecutionDelegate(() => Task.FromResult(executedContext));
            var contextBeforeExecution = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(),new Dictionary<string,object?>(),controller : null);


            //Act 
            await attributeFilter.OnActionExecutionAsync(contextBeforeExecution, nextActionAfterSuccess);


            //Assert
            Assert.NotNull(contextBeforeExecution.Result);
            var jsonResult = Assert.IsType<JsonResult>(contextBeforeExecution.Result);
            Assert.Equal(StatusCodes.Status403Forbidden, jsonResult.StatusCode);


        }
    }
}
