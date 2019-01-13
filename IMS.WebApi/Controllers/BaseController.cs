using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using IMS.Repository;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : class
    {
        IBaseService<T> BaseService;
        public BaseController(IBaseService<T> baseService)
        {
            BaseService = baseService;
        }

        // GET: api/Base
        [HttpGet]
        public async Task<IEnumerable<T>> Get()
        {
            return await BaseService.Query();
        }

        // GET: api/Base/5
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int id)
        {
            var entity = await BaseService.QueryByID(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/Base/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, T entity)
        {
            PropertyInfo pi = entity.GetType().GetProperties().FirstOrDefault(p => p.IsDefined(typeof(KeyAttribute)));
            int entityId =(int)pi.GetValue(entity);
            if (entityId != id)
            {
                return BadRequest();
            }
            else
            {          
                var e = await BaseService.QueryByID(id);
                if (e == null)
                {
                    int eid=await BaseService.Add(entity);
                    return CreatedAtAction("Get", new { id = eid }, entity);
                }
                else
                {
                    await BaseService.Update(entity);
                }
            }
            
            return NoContent();
        }
        // PATCH: api/Base
        [HttpPatch]
        public async Task<ActionResult<T>> Patch(T entity)
        {
            PropertyInfo pi = entity.GetType().GetProperties().FirstOrDefault(p => p.IsDefined(typeof(KeyAttribute)));
            int entityId = (int)pi.GetValue(entity);
            var e = await BaseService.QueryByID(entityId);
            if (e == null)
            {
                return NotFound();
            }
            await BaseService.Update(entity);
            return CreatedAtAction("Get", new { id = entityId }, entity);
        }
        // POST: api/Base
        [HttpPost]
        public async Task<ActionResult<T>> Add(T entity)
        {
            int entityId=await BaseService.Add(entity);       
            return CreatedAtAction("Get", new { id = entityId }, entity);
        }

        // DELETE: api/Base/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var entity = await Task.Run(() => BaseService.QueryByID(id));
            if (entity == null)
            {
                return NotFound();
            }
            return await Task.Run(() => BaseService.Delete(entity));

        }




        //// GET: api/Base
        //[HttpGet]
        //public async Task<IEnumerable<T>> Get()
        //{
        //    return await BaseService.FindList();
        //}

        //// GET: api/Base/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<T>> Get(int id)
        //{
        //    var entity = await BaseService.Find(id);
        //    if (entity == null)
        //    {
        //        return NotFound();
        //    }
        //    return entity;
        //}

        //// PUT: api/Base/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, T entity)
        //{
        //    string entityId = entity.GetType().GetProperty("Id").GetValue(entity).ToString();
        //    if (entityId != id.ToString())
        //    {
        //        return BadRequest();
        //    }
        //    if (string.IsNullOrEmpty(entityId) || entityId == id.ToString())
        //    {
        //        var u = await BaseService.Find(id);
        //        if (u == null)
        //        {
        //            entity.GetType().GetProperty("Id").SetValue(entity, 0);
        //            await BaseService.Add(entity);
        //            return CreatedAtAction("Get", new { id = entity.GetType().GetProperty("Id").GetValue(entity).ToString() }, entity);
        //        }
        //        else
        //        {
        //            BaseService.Copy(u, entity);
        //            await BaseService.Update(entity);
        //        }
        //    }

        //    return NoContent();
        //}
        //// PATCH: api/Base
        //[HttpPatch]
        //public async Task<ActionResult<T>> Patch(T entity)
        //{
        //    var u = await BaseService.Find(entity);
        //    if (u == null)
        //    {
        //        return NotFound();
        //    }
        //    BaseService.Copy(u, entity);
        //    await BaseService.Update(u);
        //    return CreatedAtAction("Get", new { id = entity.GetType().GetProperty("Id").GetValue(entity).ToString() }, entity);
        //}
        //// POST: api/Base
        //[HttpPost]
        //public async Task<ActionResult<T>> Add(T entity)
        //{
        //    entity.GetType().GetProperty("Id").SetValue(entity, 0);
        //    await BaseService.Add(entity);
        //    return CreatedAtAction("Get", new { id = entity.GetType().GetProperty("Id").GetValue(entity).ToString() }, entity);
        //}

        //// DELETE: api/Base/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> Delete(int id)
        //{
        //    var entity = await Task.Run(() => BaseService.Find(id));
        //    if (entity == null)
        //    {
        //        return NotFound();
        //    }
        //    return await Task.Run(() => BaseService.Delete(entity));

        //}
    }
}
