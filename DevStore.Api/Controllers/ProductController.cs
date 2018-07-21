using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DevStore.Domain;
using DevStore.Infra;

namespace DevStore.Api.Controllers
{
    [RoutePrefix("api/v1/public")]
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class ProductController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        [Route("products")]
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            var products = db.Products.Include(x => x.Category).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        // GET: api/Product
        //public IEnumerable<Product> GetProducts()
        //{
        //    return db.Products.ToList();

        //}

        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            var categories = this.db.Categories.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        [Route("category/{categoryId}/products")]
        public HttpResponseMessage GetProductsByCategory(int categoryId) {
            var result = db.Products.Where(x => x.CategoryId == categoryId);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Route("products")]
        [HttpPost]
        public HttpResponseMessage Product(Product product)
        {
            if(product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Products.Add(product);

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,"Falha ao incluir o produto");
            }

        }

        [Route("products")]
        [HttpPut]
        [HttpPatch]
        public HttpResponseMessage UpdateProduct(Product product)
        {
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Product>(product).State = EntityState.Modified;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto");
            }

        }

        [Route("products/{productId}")]
        [HttpDelete]
        public HttpResponseMessage DeteleProduct(int productId)
        {
            if (productId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            

            try
            {
                var product = db.Products.Find(productId);

                db.Products.Remove(product);

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Produto excluido");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto");
            }

        }
    }
}