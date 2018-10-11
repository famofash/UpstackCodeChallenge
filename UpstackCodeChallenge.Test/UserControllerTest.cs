
using Xunit;
using Moq;
using UpstackCodeChallenge.Contracts;
using UpstackCodeChallenge.Controllers;
using UpstackCodeChallenge.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using UpstackCodeChallenge.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpstackCodeChallenge.UnitofWork;

namespace UpstackCodeChallenge.Test
{

    public class UserControllerTest
    {
        private UserController _userController;
       
      
         private Mock<IUnitOfWork> _unitofWorkMoq = new Mock<IUnitOfWork>();
        static SendGridModel appSettings = new SendGridModel()
        {
            SMTPHost = "smtp.gmail.com",
            SMTPUser = "zonamailbox2@gmail.com",
            SMTPPassword = "Aa123456!",
            SMTPPort = "587"
        };
        IOptions<SendGridModel> ioptions = Options.Create(appSettings);
       // private Mock<IOptions<SendGridModel>> ioptions = new Mock<IOptions<SendGridModel>>();
        public UserControllerTest()
        {
            _userController = new UserController(_unitofWorkMoq.Object, ioptions);
        }
        [Fact]
        public async System.Threading.Tasks.Task Post_ShouldCreateUserAsync()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserBindingModel>();
            });

            var user = new UserBindingModel
            {
                Email = "famofash@gmail.com",
                Username = "famofash",
                ClientURL = "/User/Activate?token=B7BDBF93BA5AE5E2A7DBB926EE8ACD2A",
                IsVerified = false,
                Token = "B7BDBF93BA5AE5E2A7DBB926EE8ACD2A"
            };
            var options = new DbContextOptionsBuilder<UpstackDbContext>()
              .UseInMemoryDatabase(databaseName: "Post_ShouldCreateUserAsync")
               .Options;
            using (var context = new UpstackDbContext(options))
            {
               IUnitOfWork _unitofWork = new UnitOfWork(context);
                _userController = new UserController(_unitofWork, ioptions);
                var result = await _userController.Post(user);
                _unitofWork.Save();
               

                var createdResult =  result as OkResult;
                
                Assert.Equal(200, createdResult.StatusCode);
            }
              
        }
        [Theory]
        [InlineData("B7BDBF93BA5AE5E2A7DBB926EE8ACD")]
        public void Update_ShouldVerifyToken(string token)
        {
         
            var options = new DbContextOptionsBuilder<UpstackDbContext>()
              .UseInMemoryDatabase(databaseName: "Update_ShouldVerifyCustomer")
               .Options;
            using (var context = new UpstackDbContext(options))
            {
                IUnitOfWork _unitofWork = new UnitOfWork(context);
                _userController = new UserController(_unitofWork, ioptions);
                var result =  _userController.Put(token);
                _unitofWork.Save();


                var createdResult = result as NotFoundResult;

                Assert.Equal(404, createdResult.StatusCode);
            }
        }

    }
}
