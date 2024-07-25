namespace RabeenApi.Dtos.ContactMessage.Requests;

public record AddContactMessageRequest(string Name, string Email, string Subject, string Text);