﻿public class CallDetails
{
    public string Advisor { get; set; }
    public string Phone { get; set; }
    public string MemberId { get; set; }
    public string ReservationId { get; set; }
    public string Ssn { get; set; }

    public override string ToString()
    {
        return $"Call to {Advisor} from {Phone}, Member: {MemberId}, Reservation: {ReservationId}, SSN: {Ssn}";
    }
}
