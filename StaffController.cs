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
    public class StaffController : ApiController
    {


        [HttpPost]
        public Staffs GetEncKey(Staffs entity)
        {
            try
            {
                StaffBusiness staffBusiness = new StaffBusiness();

                var tempStaffs = staffBusiness.GetEncryptedKey(entity.Username);

                return tempStaffs;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Staff Get Enc failed. " + "\n" + ExceptionHelper.ExceptionToString(ex));
                return null;

            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                StaffBusiness staffBusiness = new StaffBusiness();

                var result = staffBusiness.Find(id);
                Staffs tempData = new Staffs()
                {
                    CompanyId = result.CompanyId,
                    Name = result.Name,
                    Surname = result.Surname,
                    Address = result.Address,
                    DatetimeOfCreated = result.DatetimeOfCreated,
                    Username = result.Username,
                    PhotoURL = result.PhotoURL,
                    CityOfBirth = result.CityOfBirth,
                    IdentificationNumber = result.IdentificationNumber,
                    BeginningDateOfDriverLicense = result.BeginningDateOfDriverLicense,
                    EndingDateOfDriverLicense = result.EndingDateOfDriverLicense,
                    Password = result.Password,
                    Id = result.Id
                };


                return Request.CreateResponse(HttpStatusCode.OK, tempData);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Staff Get failed. " + "\n" + ExceptionHelper.ExceptionToString(ex));
                return null;
            }
        }

     
       

    }
}
