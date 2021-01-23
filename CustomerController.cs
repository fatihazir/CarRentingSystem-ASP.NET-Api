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
    public class CustomerController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage FindCustomer(int id)
        {
            try
            {
                CustomerBusiness repo = new CustomerBusiness();
                var result = repo.FindCustomerByIdentificatonNumber(id);

                Customers tempData = new Customers()
                {
                    DatetimeOfCreated = result.DatetimeOfCreated,
                    Name = result.Name,
                    Address = result.Address,
                    Surname = result.Surname,
                    CityOfBirth = result.CityOfBirth,
                    IdentificationNumber = result.IdentificationNumber,
                    BeginningDateOfDriverLicense = result.BeginningDateOfDriverLicense,
                    EndingDateOfDriverLicense = result.EndingDateOfDriverLicense,
                    Id = result.Id
                };

                return Request.CreateResponse(HttpStatusCode.OK, tempData);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Find Customer failed. " + id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return null;
            }
        }

        [HttpPost]
        public string InsertCustomer(Customers entity)
        {
            try
            {
                CustomerBusiness repo = new CustomerBusiness();
                bool result = repo.InsertCustomer(entity);
                return result == true ? "Inserted Succesfuly!" : "Insertation Failed!";
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File,
                    "Insert Customer failed. " + entity.Id + "\n" + ExceptionHelper.ExceptionToString(ex));
                return "Inserting failed! Exception : " + ex.Message;
            }
            
        }

    }
}
