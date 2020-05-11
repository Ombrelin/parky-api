using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Models.DTOs;
using WebApplication.Repository.IRepository;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _parkRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository parkRepository, IMapper mapper)
        {
            _parkRepository = parkRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of national Parks
        /// </summary>
        /// <returns>List of all the national parks</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<NationalParkDTO>))]
        public IActionResult GetNationalParks()
        {
            return Ok(this._parkRepository.GetNationalParks().Select(park => _mapper.Map<NationalParkDTO>(park)));
        }

        /// <summary>
        /// Gets a National Park from its Id
        /// </summary>
        /// <param name="nationalParkId">Id of the National Park</param>
        /// <returns>The corresponding National Park</returns>
        [HttpGet("{nationalParkId:Guid}", Name = "GetNationalPark")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<NationalParkDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetNationalPark(Guid nationalParkId)
        {
            var park = this._parkRepository.GetNationalPark(nationalParkId);
            if (park == null)
            {
                return NotFound();
            }
            return Ok(this._mapper.Map<NationalParkDTO>(this._parkRepository.GetNationalPark(nationalParkId)));
        }

        /// <summary>
        /// Creates a National Park
        /// </summary>
        /// <param name="dto">The National Park to create</param>
        /// <returns>The created National Park</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(NationalParkDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDTO dto)
        {
            if (dto.Equals(null))
            {
                return BadRequest(ModelState);
            }

            if (_parkRepository.NationalParkExists(dto.Name))
            {
                ModelState.AddModelError("","National Park already Exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalPark = _mapper.Map<NationalPark>(dto);
            if (!this._parkRepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Error happened creating {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new {nationalParkId = nationalPark.Id}, nationalPark);
        }

        /// <summary>
        /// Updates a Nation Park
        /// </summary>
        /// <param name="nationalParkId">The Id of the National Park to update</param>
        /// <param name="dto">New data of the National Park to update</param>
        [HttpPatch("{nationalParkId:Guid}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(Guid nationalParkId, [FromBody] NationalParkDTO dto)
        {
            if (dto.Equals(null) || nationalParkId != dto.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalPark = _mapper.Map<NationalPark>(dto);
            if (!this._parkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Error happened updating {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a Nation Park
        /// </summary>
        /// <param name="nationalParkId">Id of the National Park to delete</param>
        [HttpDelete("{nationalParkId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(Guid nationalParkId)
        {
            if (!_parkRepository.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            var nationalPark = _parkRepository.GetNationalPark(nationalParkId);
            if (!_parkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Error happened deleting {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}