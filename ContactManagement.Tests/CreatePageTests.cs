using ContactManagement.Data;
using ContactManagement.Models;
using ContactManagement.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Xml.Linq;
using Xunit;

namespace ContactManagement.Tests
{
    public class CreatePageTests
    {        
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
                
        private CreateModel CreatePageModel(ApplicationDbContext context)
        {
            
            var httpContext = new DefaultHttpContext();
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new PageActionDescriptor(), modelState);
            var modelMetadataProvider = new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            return new CreateModel(context)
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        [Fact] 
        public async Task OnPostAsync_WhenNameIsTooShort_ReturnsPageWithModelError()
        {                     
            await using var context = GetInMemoryDbContext();
                        
            var pageModel = CreatePageModel(context);
                        
            pageModel.Contact = new Contact
            {               
                Phone = "123456789",
                Email = "test@test.com"
            };
                        
            pageModel.ModelState.AddModelError("Contact.Name", "O nome deve ter entre 5 e 100 caracteres.");

            var result = await pageModel.OnPostAsync();
            
            Assert.False(pageModel.ModelState.IsValid);
                        
            Assert.IsType<PageResult>(result);
                        
            Assert.Equal(0, await context.Contacts.CountAsync());
        }
    }
}