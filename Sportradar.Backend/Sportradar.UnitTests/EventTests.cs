using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Sportradar.Core.Application.DTOs;
using Sportradar.Core.Application.Services;
using Sportradar.Core.Domain;
using Sportradar.Core.Domain.RepositoryContracts;
using Sportradar.Core.Entities;

namespace Sportradar.UnitTests;

public class EventTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IEventRepository> _eventRepo;
    private readonly Mock<ILocationRepository> _locationRepo;
    private readonly Mock<IPlayerRepository> _playerRepo;

    private readonly EventService _sut;

    public EventTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));

        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _eventRepo = _fixture.Freeze<Mock<IEventRepository>>();
        _locationRepo = _fixture.Freeze<Mock<ILocationRepository>>();
        _playerRepo = _fixture.Freeze<Mock<IPlayerRepository>>();

        _sut = _fixture.Create<EventService>();
    }

    #region Add Participant
    [Fact]
    public async Task AddParticipant_NotFreeForAll_Throws()
    {
        var ev = _fixture.Create<TeamEvent>();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.AddParticipant(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async Task AddParticipant_PlayerNotFound_Throws()
    {
        var ev = _fixture.Create<FreeForAllEvent>();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        _playerRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Player?)null);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.AddParticipant(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async Task AddParticipant_Success_AddsParticipant()
    {
        var player = _fixture.Create<Player>();
        var ev = _fixture.Build<FreeForAllEvent>()
            .With(x => x.Participants, new List<Player>())
            .Create();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        _playerRepo.Setup(x => x.GetByIdAsync(player.Id))
            .ReturnsAsync(player);

        await _sut.AddParticipant(Guid.NewGuid(), player.Id);

        Assert.Contains(player, ev.Participants);
        _eventRepo.Verify(x => x.UpdateAsync(ev), Times.Once);
    }
    #endregion

    #region Remove Participant
    [Fact]
    public async Task RemoveParticipant_NotFreeForAll_Throws()
    {
        var ev = _fixture.Create<TeamEvent>();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.RemoveParticipant(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async Task RemoveParticipant_NotInEvent_Throws()
    {
        var ev = _fixture.Create<FreeForAllEvent>();
        var player = _fixture.Create<Player>();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        _playerRepo.Setup(x => x.GetByIdAsync(player.Id))
            .ReturnsAsync(player);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.RemoveParticipant(Guid.NewGuid(), player.Id));
    }
    [Fact]
    public async Task RemoveParticipant_Success_Removes()
    {
        var player = _fixture.Create<Player>();

        var ev = _fixture.Build<FreeForAllEvent>()
            .With(x => x.Participants, new List<Player> { player })
            .Create();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        _playerRepo.Setup(x => x.GetByIdAsync(player.Id))
            .ReturnsAsync(player);

        await _sut.RemoveParticipant(Guid.NewGuid(), player.Id);

        Assert.DoesNotContain(player, ev.Participants);
    }
    #endregion

    #region Create Event
    [Fact]
    public async Task CreateEvent_InvalidDates_Throws()
    {
        var request = _fixture.Build<CreateTeamEventRequest>()
            .With(x => x.StartTime, DateTime.UtcNow)
            .With(x => x.EndTime, DateTime.UtcNow.AddHours(-1))
            .Create();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.CreateEvent(request));
    }

    [Fact]
    public async Task CreateEvent_NoLocation_Throws()
    {
        var request = _fixture.Build<CreateTeamEventRequest>()
            .With(x => x.LocationId, (Guid?)null)
            .With(x => x.NewLocation, (LocationDTO?)null)
            .Create();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.CreateEvent(request));
    }
    [Fact]
    public async Task CreateEvent_NewLocation_CreatesLocation()
    {
        var request = _fixture.Build<CreateTeamEventRequest>()
            .With(x => x.StartTime, DateTime.UtcNow)
            .With(x => x.EndTime, DateTime.UtcNow.AddHours(1))
            .With(x => x.LocationId, (Guid?)null)
            .With(x => x.NewLocation, _fixture.Create<LocationDTO>())
            .Create();

        await _sut.CreateEvent(request);

        _locationRepo.Verify(x => x.CreateAsync(It.IsAny<Location>()), Times.Once);
        _eventRepo.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once);
    }
    #endregion

    #region Delete Event
    [Fact]
    public async Task DeleteEvent_EventNotFound_Throws()
    {
        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Event?)null);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.DeleteEvent(Guid.NewGuid()));
    }
    [Fact]
    public async Task DeleteEvent_Success_CallsDelete()
    {
        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_fixture.Create<TeamEvent>());

        await _sut.DeleteEvent(Guid.NewGuid());

        _eventRepo.Verify(x => x.DeleteAsync(It.IsAny<Guid>()), Times.Once);
    }
    #endregion

    #region Update Event
    [Fact]
    public async Task UpdateEvent_EventNotFound_Throws()
    {
        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Event?)null);

        var request = _fixture.Create<UpdateEventRequest>();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.UpdateEvent(request));
    }
    [Fact]
    public async Task UpdateEvent_InvalidDates_Throws()
    {
        var ev = _fixture.Create<TeamEvent>();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        var request = _fixture.Build<UpdateTeamEventRequest>()
            .With(x => x.StartTime, DateTime.UtcNow.AddHours(2))
            .With(x => x.EndTime, DateTime.UtcNow)
            .Create();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _sut.UpdateEvent(request));
    }
    [Fact]
    public async Task UpdateEvent_TeamEvent_UpdatesTeams()
    {
        var ev = _fixture.Create<TeamEvent>();

        _eventRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(ev);

        var request = _fixture.Build<UpdateTeamEventRequest>()
            .With(x => x.StartTime, DateTime.UtcNow)
            .With(x => x.EndTime, DateTime.UtcNow.AddHours(1))
            .With(x => x.HomeTeamId, Guid.NewGuid())
            .Create();

        await _sut.UpdateEvent(request);

        Assert.Equal(request.HomeTeamId!.Value, ev.HomeTeamId);
    }
    #endregion

    [Fact]
    public async Task GetEventsByCity_ReturnsMappedResults()
    {
        var events = _fixture.CreateMany<TeamEvent>(3).Cast<Event>().ToList();

        _eventRepo.Setup(x => x.GetByCityAsync(It.IsAny<string>()))
            .ReturnsAsync(events);

        var result = await _sut.GetEventsByCity("Warsaw");

        Assert.Equal(3, events.Count);
    }
}
