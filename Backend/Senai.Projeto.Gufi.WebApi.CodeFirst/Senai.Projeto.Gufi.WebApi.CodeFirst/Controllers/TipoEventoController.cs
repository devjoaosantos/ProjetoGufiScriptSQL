﻿using Microsoft.AspNetCore.Mvc;
using Senai.Projeto.Gufi.WebApi.CodeFirst.Domains;
using Senai.Projeto.Gufi.WebApi.CodeFirst.Interfaces;
using Senai.Projeto.Gufi.WebApi.CodeFirst.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Projeto.Gufi.WebApi.CodeFirst.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private ITipoEventoRepository _tipoEventoRepository;

        public TipoEventoController()
        {
            _tipoEventoRepository = new TipoEventoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tipoEventoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return StatusCode(200, _tipoEventoRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Post (TipoEvento novoTipoEvento)
        {
            _tipoEventoRepository.Cadastrar(novoTipoEvento);

            return StatusCode(201);
        }

        [HttpPut]
        public IActionResult Put (int id, TipoEvento tipoEventoAtualizado)
        {
            TipoEvento tipoEventoBuscado =_tipoEventoRepository.BuscarPorId(id);

            if(tipoEventoBuscado != null)
            {
                try
                {
                    _tipoEventoRepository.Atualizar(id, tipoEventoAtualizado);
                    return StatusCode(200);
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }
            return StatusCode(404);
        }

        [HttpDelete]
        public IActionResult Delete (int id)
        {
            TipoEvento tipoEventoBuscado = _tipoEventoRepository.BuscarPorId(id);

            if(tipoEventoBuscado == null)
            {
                return NotFound();
            }
            _tipoEventoRepository.Deletar(id);
            return StatusCode(200);
        }
    }
}
