using CRUD_API.Models;
using CRUD_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public RoomsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms()
        {
            var rooms = context.Rooms
                .Include(r => r.PaymentMethod)
                .OrderByDescending(r => r.RoomId)
                .ToList();
            return Ok(rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public ActionResult<Room> GetRoom(int id)
        {
            var room = context.Rooms
                .Include(r => r.PaymentMethod)
                .FirstOrDefault(r => r.RoomId == id);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        // GET: api/Rooms/search
        [HttpGet("search")]
        public ActionResult<IEnumerable<Room>> SearchRooms(string? query)
        {
            if (string.IsNullOrEmpty(query))
                return GetRooms();

            var rooms = context.Rooms
                .Include(r => r.PaymentMethod)
                .Where(r => r.RoomId.ToString().Contains(query) ||
                            r.TenantName.Contains(query) ||
                            r.PhoneNumber.Contains(query))
                .ToList();

            return Ok(rooms);
        }

        // POST: api/Rooms/Create
        [HttpPost]
        public ActionResult<Room> CreateRoom(RoomDto roomDto)
        {
            var paymentMethod = context.PaymentMethods.Find(roomDto.PaymentMethodId);
            if (paymentMethod == null)
            {
                ModelState.AddModelError("PaymentMethodId", "Payment method does not exist.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (roomDto.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be in the past");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var room = new Room
            {
                TenantName = roomDto.TenantName,
                PhoneNumber = roomDto.PhoneNumber,
                StartDate = roomDto.StartDate,
                PaymentMethodId = roomDto.PaymentMethodId
            };

            context.Rooms.Add(room);
            context.SaveChanges();

            // Load payment method for return
            context.Entry(room).Reference(r => r.PaymentMethod).Load();

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, RoomDto roomDto)
        {
            var room = context.Rooms.Find(id);
            if (room == null)
                return NotFound();

            var paymentMethod = context.PaymentMethods.Find(roomDto.PaymentMethodId);
            if (paymentMethod == null)
            {
                ModelState.AddModelError("PaymentMethodId", "Payment method does not exist.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            if (roomDto.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("StartDate", "Start date cannot be in the past");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            room.TenantName = roomDto.TenantName;
            room.PhoneNumber = roomDto.PhoneNumber;
            room.StartDate = roomDto.StartDate;
            room.PaymentMethodId = roomDto.PaymentMethodId;

            context.SaveChanges();

            context.Entry(room).Reference(r => r.PaymentMethod).Load();

            return Ok(room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = context.Rooms.Find(id);
            if (room == null)
                return NotFound();

            context.Rooms.Remove(room);
            context.SaveChanges();
            return Ok();
        }

        // DELETE: api/Rooms
        [HttpDelete]
        public IActionResult DeleteMultipleRooms([FromBody] int[] ids)
        {
            var roomsToDelete = context.Rooms.Where(r => ids.Contains(r.RoomId)).ToList();
            if (!roomsToDelete.Any())
                return NotFound();

            context.Rooms.RemoveRange(roomsToDelete);
            context.SaveChanges();
            return Ok();
        }
    }
}