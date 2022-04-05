using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lead.Data;
using Lead.Models;
using Lead.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lead.Controller
{

    [ApiController] //Explicitando o uso para Api
    [Route(template: "v1")]
    public class LeadController : ControllerBase
    {
        //Requisito all
        [HttpGet]
        [Route(template: "LeadsGet")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
        )
        {
            var leads = await context
                .MeusLeads
                .AsNoTracking()
                .ToListAsync();
            return Ok(leads);
        }
        //Requisito by Id
        [HttpGet]
        [Route(template: "LeadsGet/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var leads = await context
                .MeusLeads
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return leads == null ? NotFound() : Ok(leads);
        }

        //Verbo Post Inserir novos leads (Cread/Cadastrar)
        [HttpPost(template: "LeadsPost")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody]  CreateLeadViewModel model
        )
        {
            if (!ModelState.IsValid) //Validando se os campos estão preenchidos
                return BadRequest();

            var leads = new Leads
            {
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone
            };

            try
            {
                await context.MeusLeads.AddAsync(leads);
                await context.SaveChangesAsync();
                return Created("v1/LeadsPost/{leads.Id}", leads);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}