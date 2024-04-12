using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using APISensor.Models;
using APISensor.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

    namespace APISensor.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class DadosController : ControllerBase
        {
            private readonly ILogger<DadosController> _logger;
            private readonly AppDbContext _context;

            public DadosController(ILogger<DadosController> logger, AppDbContext context)
            {
                _logger = logger;
                _context = context;
            }

            // GET: api/<DadosController>
            [HttpGet]
            public IActionResult Get()
            {
                try
                {
                    var dados = _context.Dados.ToList();
                    return Ok(dados);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar dados do banco de dados: {ex.Message}");
                }
            }

            // GET api/<DadosController>/5
            [HttpGet("{equipmentId}")]
            public IActionResult Get(string equipmentId)
            {
                try
                {
                    var dados = _context.Dados.FirstOrDefault(d => d.EquipmentId == equipmentId);
                    if (dados == null)
                        return NotFound("Dados não encontrados para o equipamento especificado.");

                    return Ok(dados);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar dados do banco de dados: {ex.Message}");
                }
            }

            // POST api/<DadosController>
            [HttpPost]
            public IActionResult Post([FromBody] Dados dados)
            {
                try
                {
                    if (dados == null)
                        return BadRequest("Dados inválidos.");

                    _context.Dados.Add(dados);
                    _context.SaveChanges();

                    return Ok("Dados salvos com sucesso!");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao salvar os dados: {ex.Message}");
                }
            }

            // POST api/<DadosController>/csv
            [HttpPost("csv")]
            public IActionResult UploadCsv(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Arquivo CSV não foi enviado.");
                }

                try
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
                    {
                        var records = csv.GetRecords<CSVDados>().ToList();

                        foreach (var record in records)
                        {
                            
                            if (string.IsNullOrEmpty(record.EquipmentId) || record.Timestamp == null)
                            {
                                continue;
                            }

                            _context.Dados.Add(new Dados
                            {
                                EquipmentId = record.EquipmentId,
                                Timestamp = record.Timestamp,
                                Value = record.Value
                            });
                        }

                        _context.SaveChanges();

                        return Ok("Os dados do CSV foram salvos com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar o arquivo CSV: {ex.Message}");
                }
            }

        // GET api/<DadosController>/average/{horas}
        [HttpGet("average/{horas}")]
        public IActionResult GetAverage(int horas)
        {
            try
            {
                var cutoff = DateTime.Now.AddHours(-horas);
                var averages = _context.Dados
                    .Where(d => d.Timestamp >= cutoff)
                    .GroupBy(d => d.EquipmentId)
                    .Select(g => new {
                        EquipmentId = g.Key,
                        AverageValue = g.Average(x => x.Value)
                    }).ToList();

                return Ok(averages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao calcular a média: {ex.Message}");
            }
        }

    }
}