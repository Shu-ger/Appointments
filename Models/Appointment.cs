using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static QueueManagementSystem1.StatusEnum;

public class Appointment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? CustomerName { get; set; }
    public DateTime? Date { get; set; }
    public AppointmentStatus? Status { get; set; }  // "Scheduled", "Completed", "Cancelled"
}
