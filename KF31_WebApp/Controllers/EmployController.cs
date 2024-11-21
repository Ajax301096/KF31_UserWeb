﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace KF31_WebApp.Controllers
{
    public class EmployController : Controller
    {
        private readonly KF31_LliM5_DataContext _context;
        public EmployController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

        //public IActionResult ItemList()
        //{
        //    var items = _context.Items.Select(i => i);
        //    return View(items);
        //}

        public IActionResult Employ(string id)
        {
            var employ = _context.Employs
                         .Select(e => new Employ
                         {
                             EmployID = e.EmployID,
                             Em_DisplayName = e.Em_DisplayName
                         })
                         .ToList(); 

            return View(employ); 
        }

      


        }
    }

