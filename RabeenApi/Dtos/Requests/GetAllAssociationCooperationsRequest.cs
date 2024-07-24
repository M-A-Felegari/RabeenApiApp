namespace RabeenApi.Dtos.Requests;

public record GetAllAssociationCooperationsRequest(int AssociationId,
    int PageNumber, int PageLength);