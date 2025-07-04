using Microsoft.AspNetCore.Mvc;

namespace PedidosDeLanches.API.Controllers
{
    [Route("api/imagem")]
    [ApiController]
    public class ImagemController : ControllerBase
    {
        [HttpPost("uploadImagem")]
        public async Task<IActionResult> UploadImagem([FromForm] InputImagem dto)
        {
            var imagem = dto.Imagem;

            if (imagem == null || imagem.Length == 0)
            {
                return BadRequest("Nenhuma imagem enviada.");
            }

            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImagensPerfis");

            if(!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            var caminhoRelativo = $"ImagensPerfis/{nomeArquivo}";

            return Ok(new { caminho = caminhoRelativo });
        }
    }
}
