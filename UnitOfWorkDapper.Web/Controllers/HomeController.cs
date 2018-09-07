using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnitOfWorkDapper.Core.Helper;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Services.Interfaces;
using UnitOfWorkDapper.Web.Models;
using UnitOfWorkDapper.Web.Test;

namespace UnitOfWorkDapper.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService productService;
        private IUserService userService;

        public HomeController(IProductService _productService)
        {
            productService = _productService;
        }

        public IActionResult Index()
        {
            //Product product = new Product();

            //product.Id = 1;
            //product.Name = "测试商品1";
            //product.Price = 1999;

            //productService.SaveAsync(product);

            PageCriteria pageCriteria = new PageCriteria();
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1");
            pageCriteria.ParameterList.Add(new ParameterDict() { ParamName = "number", ParamValue = DateTime.Now.ToString("yyyyMMdd") + "%" });
            pageCriteria.ParameterList.Add(new ParameterDict() { ParamName = "sex", ParamValue = 1 });
            pageCriteria.Condition = sb.ToString();
            pageCriteria.Fields = " id,username,nickname,grade ";
            pageCriteria.PageSize = 20;
            pageCriteria.PrimaryKey = " id";
            pageCriteria.Sort = " id desc";
            pageCriteria.TableName = "[dbo].[User]";

            string con = "";

            DapperHelper<SqlConnection> dapperHelper = new DapperHelper<SqlConnection>(con);

            var result = dapperHelper.GetPageListForSQL<UserModel>(pageCriteria);

            for (var i = 1; i <= result.TotalPageCount; i++)
            {
                pageCriteria.CurrentPage = i;

                PageDataView<UserModel> data;

                if (i != 1)
                {
                    data = dapperHelper.GetPageListForSQL<UserModel>(pageCriteria);
                }
                else
                {
                    data = result;
                }
                foreach (var item in data.Items)
                {
                    User user = new User();
                    user.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    user.IsDelete = 0;
                    user.NickName = item.NickName;
                    user.UserName = item.UserName;
                    user.RegisterTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    user.Grade = item.Grade;
                    user.Id = Util.Helpers.Id.ObjectId();

                    var infomodel = dapperHelper.Query<UserInfoModel>("SELECT  * FROM [dbo].[UserInfo] WHERE UserId=@userId", new { userId = item.ID }).FirstOrDefault();

                    UserInfo info = new UserInfo();
                    if (infomodel != null)
                    {
                        info.Id = Util.Helpers.Id.Guid();
                        info.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        info.IsDelete = 0;
                        info.Age = infomodel.Age;
                        info.Birthday = infomodel.Birthday.ToString("yyyy-MM-dd HH:mm:ss");
                        info.Email = infomodel.Email;
                        info.UserId = user.Id;
                        info.QQ = infomodel.QQ;
                        info.PhoneNumber = infomodel.PhoneNumber;
                    }

                    var address = dapperHelper.Query<UserAddressModel>("SELECT  * FROM [dbo].[UserAddress] WHERE UserId=@userId", new { userId = item.ID });

                    var newAddressList = new List<UserAddress>();

                    foreach (var item1 in address)
                    {
                        UserAddress addressData = new UserAddress();

                        addressData.Id = Util.Helpers.Id.Guid();
                        addressData.IsDefault = 0;
                        addressData.IsDelete = 0;
                        addressData.PhoneNumber = item1.PhoneNumber;
                        addressData.Province = item1.Province;
                        addressData.RecName = item1.Name;
                        addressData.Regin = item1.Region;
                        addressData.Street = item1.Street;
                        addressData.UserId = user.Id;

                        newAddressList.Add(addressData);
                    }

                    //userService.ImportUser();
                }
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}