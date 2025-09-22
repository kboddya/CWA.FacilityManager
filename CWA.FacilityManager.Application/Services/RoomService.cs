using AutoMapper;
using CWA.FacilityManager.Domain.Models;
using CWA.FacilityManager.Infrastructure.Contexts;
using CWA.FacilityManager.Infrastructure.Dbos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace CWA.FacilityManager.Application.Services;

public interface IRoomService
{
    Task<List<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(Guid id);
    Task<Room?> CreateRoomAsync(RoomDbo room);
    Task<Room?> UpdateRoomAsync(Guid id, RoomDbo room);
    Task<bool> DeleteRoomAsync(Guid id);
}

public class RoomService(ApplicationDbContext context) : IRoomService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapperFromRoomDboToRoom = new MapperConfiguration(cfg => cfg.CreateMap<RoomDbo, Room>(), new NullLoggerFactory()).CreateMapper();

    public async Task<List<Room>> GetAllRoomsAsync()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return rooms;
    }

    public async Task<Room?> GetRoomByIdAsync(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);
        return room;
    }

    public async Task<Room?> CreateRoomAsync(RoomDbo room)
    {
        if (room.Location == null || room.Location.Length == 0 || room.CoutOfSeats == 0) return null;
        
        var roomModel = _mapperFromRoomDboToRoom.Map<Room>(room);
        roomModel.Id = Guid.NewGuid();
        
        await _context.Rooms.AddAsync(roomModel);
        await _context.SaveChangesAsync();
        
        return roomModel;
    }

    public async Task<Room?> UpdateRoomAsync(Guid id, RoomDbo room)
    {
        var roomToUpdate = await _context.Rooms.FindAsync(id);
        if (roomToUpdate == null) return null;
        
        var roomModel = _mapperFromRoomDboToRoom.Map<Room>(room);
        roomModel.Id = id;

        _context.Update(room);
        await _context.SaveChangesAsync();
        return roomModel;
    }

    public async Task<bool> DeleteRoomAsync(Guid id)
    {
        var roomToDelete = await _context.Rooms.FindAsync(id);
        if (roomToDelete == null) return false;

        _context.Rooms.Remove(roomToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}