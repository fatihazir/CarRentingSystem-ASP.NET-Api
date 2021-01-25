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
using CarRentingSystemApi.Models;

namespace CarRentingSystemApi.Controllers
{
    public class RentController : ApiController
    {

        [HttpGet]
        public string ConfirmRent(int id)
        {
            try
            {
                RentInfoBusiness repo  = new RentInfoBusiness();
                bool result = repo.Confirm(id);
                return result == true ? "Confirmed succesfuly!" : "Confirming Failed!";
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Confirm Reent failed. " + id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return "Confirming failed! Exception : " + ex.Message;
            }
        }

        [HttpGet]
        public string RejectRent(int id)
        {
            try
            {
                RentInfoBusiness repo = new RentInfoBusiness();
                bool result = repo.Reject(id);
                return result == true ? "Rejected succesfuly!" : "Rejecting Failed!";
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Confirm Reent failed. " + id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return "Rejecting failed! Exception : " + ex.Message;
            }
        }

        [HttpGet]
        public HttpResponseMessage RentExtraInfo(int id)
        {
            
            try
            {
                RentInfoBusiness repo = new RentInfoBusiness();
                var result = repo.Find(id);

                RentInfoModel model = new RentInfoModel()
                {
                    Customer = new Customers()
                    {
                        Address = result.Customers.Address,
                        BeginningDateOfDriverLicense = result.Customers.BeginningDateOfDriverLicense,
                        EndingDateOfDriverLicense = result.Customers.EndingDateOfDriverLicense,
                        CityOfBirth = result.Customers.CityOfBirth,
                        IdentificationNumber = result.Customers.IdentificationNumber,
                        Name = result.Customers.Name,
                        Surname = result.Customers.Surname
                    },
                    Vehicle = new Vehicles()
                    {
                        Brand = result.Vehicles.Brand,
                        ModelName = result.Vehicles.ModelName,
                        Plate = result.Vehicles.Plate
                    }
                };


                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Confirm Reent failed. " + id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return null;
            }
        }
    }
}
