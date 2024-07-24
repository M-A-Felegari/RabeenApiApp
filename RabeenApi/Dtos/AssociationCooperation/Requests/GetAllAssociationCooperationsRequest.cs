namespace RabeenApi.Dtos.AssociationCooperation.Requests;

public record GetAllAssociationCooperationsRequest(int AssociationId,
    int PageNumber, int PageLength);