﻿namespace BookShelf.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime MembershipDate { get; set; }
}

