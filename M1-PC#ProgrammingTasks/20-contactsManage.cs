using System;
using System.Collections.Generic;

class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }
}

class Program
{
    static void Main()
    {
        List<Contact> contacts = new List<Contact>();

        Console.Write("How many contacts? ");
        int count = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Contact contact = new Contact();

            Console.Write("Name: ");
            contact.Name = Console.ReadLine();

            Console.Write("Phone: ");
            contact.Phone = Console.ReadLine();

            contacts.Add(contact);
        }

        Console.WriteLine("\nContacts:");

        foreach (Contact contact in contacts)
        {
            Console.WriteLine($"{contact.Name} - {contact.Phone}");
        }
    }
}