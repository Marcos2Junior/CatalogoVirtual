using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace LojaVirtualMae.API.Classes
{
    public class AcessoController : ControllerBase
    {
        protected readonly ILogger<ControllerBase> _logger;

        public AcessoController(ILogger<ControllerBase> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// </summary>
        /// <param name="metodo"></param>
        /// <param name="codigo">
        /// 0: iniciando metodo;
        /// 1: finalizando metodo com sucesso;
        /// 2: finalizando metodo com divergência
        /// 3: outros
        /// 4: IMapper
        /// </param>
        /// <param name="mensagem"></param>
        protected void NewLog(string metodo, int codigo, string mensagem = null)
        {
            string _mensagem = string.IsNullOrEmpty(mensagem) ? string.Empty : $", {mensagem}";
            switch (codigo)
            {
                case 0:
                    _logger.LogInformation($"Iniciando metodo {metodo}{_mensagem}");
                    break;
                case 1:
                    _logger.LogInformation($"Metodo {metodo} finalizado com sucesso{_mensagem}");
                    break;
                case 2:
                    _logger.LogWarning($"Metodo {metodo} finalizado com divergência{_mensagem}");
                    break;
                case 3:
                    _logger.LogInformation($"Metodo {metodo}{_mensagem}");
                    break;
                case 4:
                    _logger.LogInformation($"Metodo {metodo}, iniciando mapeamento{_mensagem}");
                    break;
            }
        }

        protected IActionResult ErrorException(Exception exception, string method, int? id = null)
        {
            string _id = id != null ? $", id: {id}" : string.Empty;

            _logger.LogError(exception, $"exception lancada metodo {method}{_id}");

            return StatusCode(500, "Ocorreu um erro interno com o tratamento dos dados.");
        }
    }
}
