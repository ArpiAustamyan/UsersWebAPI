using backend.Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace backend.Controllers
{
    // http://localhost:50617/

    [RoutePrefix("api/Users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        UserManager userManager = new UserManager();
        
        public IHttpActionResult Get()
        {
            try
            {
                var users = userManager.Get();
               
                return Ok(users);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Post(Model model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userManager.Add(model) > 0)
                        return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }
    }
}
