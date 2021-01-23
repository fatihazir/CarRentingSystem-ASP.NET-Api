using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarRentingSystem.BusinessLogic.Concretes;
using CarRentingSystem.Commons.Concretes.Helpers;
using CarRentingSystem.Commons.Concretes.Logger;
using CarRentingSystem.DataAccess.Entity;

namespace CarRentingSystemApi.Controllers
{
    public class RentController : ApiController
    {

        //[HttpPost]
        //public string FindRentInfo(int id)
        //{
        //    try
        //    {
        //        RentBusiness repo = new RentInfos();
        //        bool result = repo.
        //        return result == true ? "Updated Succesfuly!" : "Update Failed!";
        //    }
            
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log(LogTarget.File,
        //            "Manager Post failed. " + entity.Id + "\n" + ExceptionHelper.ExceptionToString(ex));
        //        return "Updating failed! Exception : " + ex.Message;
        //    }
        //}
    }
}
