using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarRentingSystem.BusinessLogic.Concretes;
using CarRentingSystem.DataAccess.Entity;

namespace CarRentingSystemApi.Controllers
{
    public class ManagerController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            ManagerBusiness managerBusiness = new ManagerBusiness();

            var tempManagers = managerBusiness.ListManagers().Select(
                i => new
                {
                    i.Name,
                    i.Address,
                    i.BeginningDateOfDriverLicense,
                    i.DatetimeOfCreated,
                    i.CityOfBirth,
                    i.PhotoURL,
                }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, tempManagers);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post(Companies entity)
        {
            try
            {
                CompanyBusiness companyBusiness = new CompanyBusiness();
                bool result = companyBusiness.InsertCompany(entity);
                return result == true ? "Added succesfuly!" : "Adding Failed!";
            }
            catch (Exception ex)
            {
                return "Adding failed! Exception : " + ex.Message;
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
