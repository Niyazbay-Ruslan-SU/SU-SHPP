using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Подсистема: Бронирование номеров (RoomBookingSystem)
public class RoomBookingSystem
{
    public void BookRoom() => Console.WriteLine("Номер забронирован.");
    public void CheckAvailability() => Console.WriteLine("Доступность номера проверена.");
    public void CancelBooking() => Console.WriteLine("Бронирование номера отменено.");
}

// Подсистема: Ресторан (RestaurantSystem)
public class RestaurantSystem
{
    public void BookTable() => Console.WriteLine("Столик в ресторане забронирован.");
    public void OrderFood() => Console.WriteLine("Еда заказана.");
}

// Подсистема: Управление мероприятиями (EventManagementSystem)
public class EventManagementSystem
{
    public void BookConferenceHall() => Console.WriteLine("Конференц-зал забронирован.");
    public void OrderEquipment() => Console.WriteLine("Оборудование для мероприятия заказано.");
}

// Подсистема: Уборка (CleaningService)
public class CleaningService
{
    public void ScheduleCleaning() => Console.WriteLine("Уборка запланирована.");
    public void PerformCleaning() => Console.WriteLine("Уборка выполнена.");
}

// Фасад для управления отелем (HotelFacade)
public class HotelFacade
{
    private readonly RoomBookingSystem _roomBooking;
    private readonly RestaurantSystem _restaurant;
    private readonly EventManagementSystem _eventManagement;
    private readonly CleaningService _cleaningService;

    public HotelFacade(RoomBookingSystem roomBooking, RestaurantSystem restaurant, EventManagementSystem eventManagement, CleaningService cleaningService)
    {
        _roomBooking = roomBooking;
        _restaurant = restaurant;
        _eventManagement = eventManagement;
        _cleaningService = cleaningService;
    }

    // Сценарий: Бронирование номера с услугами ресторана и уборки
    public void BookRoomWithServices()
    {
        _roomBooking.BookRoom();
        _restaurant.OrderFood();
        _cleaningService.ScheduleCleaning();
        Console.WriteLine("Номер забронирован с услугами ресторана и уборки.");
    }

    // Сценарий: Организация мероприятия
    public void OrganizeEvent()
    {
        _eventManagement.BookConferenceHall();
        _eventManagement.OrderEquipment();
        _roomBooking.BookRoom();
        Console.WriteLine("Мероприятие организовано, номера забронированы.");
    }

    // Сценарий: Бронирование стола в ресторане и заказ такси
    public void BookTableWithTaxi()
    {
        _restaurant.BookTable();
        Console.WriteLine("Такси вызвано для гостей.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Инициализация подсистем
        var roomBooking = new RoomBookingSystem();
        var restaurant = new RestaurantSystem();
        var eventManagement = new EventManagementSystem();
        var cleaningService = new CleaningService();

        // Создание фасада для управления отелем
        var hotelFacade = new HotelFacade(roomBooking, restaurant, eventManagement, cleaningService);

        // Вызов сценариев через фасад
        hotelFacade.BookRoomWithServices();
        hotelFacade.OrganizeEvent();
        hotelFacade.BookTableWithTaxi();
    }
}

