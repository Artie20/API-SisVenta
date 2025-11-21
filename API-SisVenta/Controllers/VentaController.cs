using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class VentaController : ControllerBase
{
    private readonly DBVENTAbakContext _context;

    public VentaController(DBVENTAbakContext context)
    {
        _context = context;
    }

    // ✅ GET POR ID
    // api/Venta/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var venta = await _context.Venta
            .Where(v => v.IdVenta == id)
            .Select(v => new VentaDto
            {
                idVenta = v.IdVenta,
                numeroVenta = v.NumeroVenta,
                idTipoDocumentoVenta = v.IdTipoDocumentoVenta,
                idUsuario = v.IdUsuario,
                subTotal = v.SubTotal,
                impuestoTotal = v.ImpuestoTotal,
                total = v.Total,
                fechaRegistro = v.FechaRegistro,
                idCliente = v.IdCliente,
                descuento = v.Descuento
            })
            .FirstOrDefaultAsync();

        if (venta == null)
            return NotFound($"No existe venta con id {id}");

        return Ok(venta);
    }

    // ✅ POST CREAR VENTA
    // api/Venta
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VentaDto dto)
    {
        var venta = new VentaEntity
        {
            NumeroVenta = dto.numeroVenta,
            IdTipoDocumentoVenta = dto.idTipoDocumentoVenta,
            IdUsuario = dto.idUsuario,
            SubTotal = dto.subTotal,
            ImpuestoTotal = dto.impuestoTotal,
            Total = dto.total,
            FechaRegistro = DateTime.Now,
            IdCliente = dto.idCliente,
            Descuento = dto.descuento
        };

        _context.Venta.Add(venta);
        await _context.SaveChangesAsync();

        dto.idVenta = venta.IdVenta;

        return CreatedAtAction(nameof(Get), new { id = venta.IdVenta }, dto);
    }
}


