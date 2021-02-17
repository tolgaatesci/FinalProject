﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming convention
        //IoC Container - Inversion of Control - yani bellekte referansları new'leyerek oluşturur sonra da ihtiyacı olan fonksiyonda kullanılabilir, konfigürasyon yani
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")] //AOP bir metodun önünde, bir metodun sonunda çalışan kod parçacıklarıdır.
        public IActionResult GetAll()
        {
            //Swagger
            //Depandancy chain - bağımlılık zinciri
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAll();
            if(result.Success)
            {
                return Ok(result); //postman için kullanıyoruz 
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
