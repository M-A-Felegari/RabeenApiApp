﻿namespace RabeenApi.Dtos.AssociationCooperation.Requests;

public record UpdateCooperationRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime FinishDate,
    IFormFile? Image
);