namespace EsturContacts.Services.Contacts.Models
{
    public class ContactInfo
    {
        public string GSM { get; set; }

        public string Email { get; set; }

        //I would prefer storing [ longitude, latitude ] as 2d array or maybe in case of more complexity i would even prefer using a Location Model class but im gonna use a simple string which contains only country name as location data 'cause i do not want to overkill by using a geolocation api for this assesment.
        public string Location { get; set; }
    }
}
