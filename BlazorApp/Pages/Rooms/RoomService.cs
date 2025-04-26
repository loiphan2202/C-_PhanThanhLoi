using System.Net.Http.Json;
using Clients.Models;

namespace BlazorApp.Services
{
    public class RoomService
    {
        private readonly HttpClient _http;

        public RoomService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _http.GetFromJsonAsync<List<Room>>("api/rooms");
        }

        public async Task<Room> GetRoomAsync(int id)
        {
            return await _http.GetFromJsonAsync<Room>($"api/rooms/{id}");
        }

        public async Task CreateRoomAsync(RoomDto dto)
        {
            await _http.PostAsJsonAsync("api/rooms/create", dto);
        }

        public async Task UpdateRoomAsync(int id, RoomDto dto)
        {
            await _http.PutAsJsonAsync($"api/rooms/{id}", dto);
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _http.DeleteAsync($"api/rooms/{id}");
        }
    }
}
