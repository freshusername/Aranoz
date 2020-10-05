using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using HotelsBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Controllers
{
    public class AccountController : Controller

    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IAuthenticationManager authenticationManager, IMapper mapper)
        {
            _authenticationManager = authenticationManager;
            _mapper = mapper;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _mapper.Map<RegisterViewModel, UserDTO>(model);
            var result = await _authenticationManager.Register(user);
            if (result.Succedeed)
                return RedirectToAction(result.Message, "Index", "Home");
            else
                ModelState.AddModelError(result.Property, result.Message);


            return View(model);

        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginModel = _mapper.Map<LoginViewModel, UserDTO>(model);
            var identity = await _authenticationManager.Login(loginModel);

            if (!identity.Succedeed)
            {
               ModelState.AddModelError(identity.Property , identity.Message );
                return View(model);
            }

            return RedirectToAction("Index" , "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationManager.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
