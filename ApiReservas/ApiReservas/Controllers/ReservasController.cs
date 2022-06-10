﻿using ApiReservas.Models;
using ApiReservas.Models.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private IRepositoryReserva repository;
        public ReservasController(IRepositoryReserva repo) => repository = repo;


        //obtém todos os registros de reserva para o cliente em JSON
        [HttpGet]
        public IEnumerable<Reserva> Get() => repository.Reservas;

        //contém o segmento de roteamento 'id' como argumento. Em seguida, ele entrega a reserva para esse 'id' específico no formato JSON.
        [HttpGet("{id}")]
        public Reserva Get(int id) => repository[id];


        //O método Action HttpPost é usado para criar uma nova reserva. Ele recebe o objeto Reserva em seu argumento.
        //O atributo [FromBody] aplicado ao seu argumento garante que o conteúdo do body enviado pelo cliente seja decodificado
        //e colocado nesse argumento, usando o conceito de Model Binding.
        [HttpPost]
        public Reserva Post([FromBody] Reserva res) =>
        repository.AddReserva(new Reserva
        {
            Nome = res.Nome,
            InicioLocacao = res.InicioLocacao,
            FimLocacao = res.FimLocacao
        });


        //O método Action HttpPut é usado para atualizar um objeto reserva sendo chamado quando a requisição Http do tipo PUT for feita no URL
        [HttpPut]
        public Reserva Put([FromForm] Reserva res) => repository.UpdateReserva(res);


        //O método Action HttpPatch fará várias operações, como Adicionar, Remover, Atualizar, Copiar, etc., de um objeto Reserva enviado do cliente.
        //O cliente envia apenas um conjunto específico de propriedades de reserva no formato JSON.
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromForm] JsonPatchDocument<Reserva> patch)
        {
            Reserva res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }


        //O método Action HttpDelete exclui uma reserva do repositório.
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReserva(id);
    }
}
