using AutoMapper;
using Moq;
using NUnit.Framework;
using WebApplicationApi.Dtos.TicketDtos;
using WebApplicationApi.Entity;
using WebApplicationApi.Repositories;
using WebApplicationApi.Services;

namespace UnitTests;

[TestFixture]
public class TicketServiceTests
{
    private Mock<IRepository<Ticket>> _mockRepo;
    private Mock<IMapper> _mockMapper;
    private TicketService _ticketService;

    [SetUp] 
    public void Setup()
    {
        // Asılılıqları (Dependencies) saxtalaşdırırıq (Mocking)
        _mockRepo = new Mock<IRepository<Ticket>>();
        _mockMapper = new Mock<IMapper>();
        
        // Saxta asılılıqlarla servisimizi yaradırıq
        _ticketService = new TicketService(_mockRepo.Object, _mockMapper.Object);
    }

    [Test] 
    public async Task GetByIdAsync_WhenTicketExists_ReturnsTicketReturnDto()
    {
        // 1. ARRANGE (HAZIRLA) - Saxta məlumatları hazırlayırıq
        int ticketId = 1;
        var fakeTicketEntity = new Ticket { Id = ticketId, EventId = 2 };
        var fakeTicketDto = new TicketReturnDto { Id = ticketId };

        // Əgər Repository-nin GetByIdAsync metodu 1 id-si ilə çağırılarsa, fakeTicketEntity qaytar.
        _mockRepo.Setup(repo => repo.GetByIdAsync(ticketId))
                 .ReturnsAsync(fakeTicketEntity);

        // Əgər Mapper fakeTicketEntity-ni map etməyə çalışarsa, fakeTicketDto qaytar.
        _mockMapper.Setup(mapper => mapper.Map<TicketReturnDto>(fakeTicketEntity))
                   .Returns(fakeTicketDto);

        // 2. ACT (İCRA ET) - Test etmək istədiyimiz əsas metodu çağırırıq
        var result = await _ticketService.GetByIdAsync(ticketId);

        // 3. ASSERT (YOXLA) - Nəticənin gözləntilərimizə uyğun olub-olmadığını yoxlayırıq
        Assert.That(result, Is.Not.Null); // Nəticə null olmamalıdır
        Assert.That(result.Id, Is.EqualTo(ticketId)); // Qayıdan DTO-nun id-si 1 olmalıdır
    }
    
    
    [Test]
    public async Task GetByIdAsync_WhenTicketDoesNotExist_ReturnsNull()
    {
        // 1. ARRANGE - Olmayan bir ID və saxta davranış hazırlayırıq
        int invalidTicketId = 99; 
    
        // Əgər bazadan 99 nömrəli bilet istənilsə, yalandan null qaytar
        _mockRepo.Setup(repo => repo.GetByIdAsync(invalidTicketId))
            .ReturnsAsync((Ticket)null);

        // 2. ACT - Metodu icra edirik
        var result = await _ticketService.GetByIdAsync(invalidTicketId);

        // 3. ASSERT - Gözləyirik ki, nəticə mütləq null olsun
        Assert.That(result, Is.Null);
    }
}