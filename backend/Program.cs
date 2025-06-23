using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/contacts", async (ContactsDbContext db) =>
    await db.Contacts.ToListAsync());

app.MapGet("/api/contacts/{id}", async (Guid id, ContactsDbContext db) =>
    await db.Contacts.FindAsync(id)
        is Contact contact
        ? Results.Ok(contact)
        : Results.NotFound());

app.MapPost("/api/contacts", async (Contact contact, ContactsDbContext db) =>
{
    contact.Id = Guid.NewGuid();
    contact.CreatedAt = DateTime.UtcNow;
    contact.UpdatedAt = DateTime.UtcNow;
    db.Contacts.Add(contact);
    await db.SaveChangesAsync();
    return Results.Created($"/api/contacts/{contact.Id}", contact);
});

app.MapPut("/api/contacts/{id}", async (Guid id, Contact input, ContactsDbContext db) =>
{
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null) return Results.NotFound();

    contact.FullName = input.FullName;
    contact.PhoneNumber = input.PhoneNumber;
    contact.Company = input.Company;
    contact.DateOfBirth = input.DateOfBirth;
    contact.Address = input.Address;
    contact.UpdatedAt = DateTime.UtcNow;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/contacts/{id}", async (Guid id, ContactsDbContext db) =>
{
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null) return Results.NotFound();
    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

