using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Business.Services;
using HDA.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityRoles.AdminName)]
    public class ReservationController : ControllerBase
    {
        private readonly IResasZeroService _resasZeroService;
        private readonly IReservationService _reservationService;
        public ReservationController(IResasZeroService resasZeroService, IReservationService reservationService)
        {
            _resasZeroService = resasZeroService;
            _reservationService = reservationService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllReservations()
        {
            var list = await _reservationService.GetAllReservations();
            return Ok(list);
        }

        [HttpGet("Public")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllReservationsPubliques()
        {
            var list = await _reservationService.GetAllReservationsPubliques();
            return Ok(list);
        }

        [HttpGet("Zero")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllReservationsZero()
        {
            var list = await _resasZeroService.GetAllReservations();
            return Ok(list);
        }

        [HttpGet("Zero/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReservationZeroById(long id)
        {
            var dto = await _resasZeroService.GetReservationById(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReservationById(long id)
        {
            var dto = await _reservationService.GetReservationById(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddReservation(ReservationDto dto)
        {
            var result = await _reservationService.AddReservation(dto);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateReservation(ReservationDto dto)
        {
            var result = await _reservationService.UpdateReservation(dto);
            if (result is null) return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            var b = await _reservationService.DeleteReservation(id);
            if (!b) return NotFound();
            return NoContent();
        }

    }
}